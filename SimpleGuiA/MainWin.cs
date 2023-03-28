using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Introspect.Python.Runtime;

namespace TestWindowsForms
{
    public partial class MainWin : Form
    {
        private string _svtPythonPath;
        private string _formFactorName;

        public MainWin()
        {
            InitializeComponent();
            Svt.SetPythonPath("C:\\Introspect\\IntrospectESP_22.2.b7\\PythonEnv");
            Svt.SetSvtPythonPath("C:\\Introspect\\IntrospectESP_22.2.b7\\SvtPython");

            TextBoxStreamWriter txtBoxWriter
                            = new TextBoxStreamWriter(logTxtBox);
            Svt.RedirectPythonOutput(txtBoxWriter);
            Svt.InitPythonEngine();
        }

        private void LogMsg(string msg)
        {
            logTxtBox.AppendText(msg + Environment.NewLine);
        }

        private void FormFactorBtnClicked(object sender, EventArgs e)
        {
            using (var dialog = new FormFactorDialog())
            {
                dialog.ShowDialog(this);
                if (dialog.DialogResult == DialogResult.OK)
                {
                    var formFactorName = dialog.getFormFactorName();
                    if (formFactorName.Length > 0)
                    {
                        Application.DoEvents(); // update screen (next line takes a while)
                        dynamic iesp = Svt.InitFormFactor(formFactorName, _svtPythonPath);
                        _formFactorName = formFactorName;
                        formFactorTxtBox.Text = formFactorName;
                        formFactorBtn.Enabled = false;
                        svtPythonPathBtn.Enabled = false;
                    }
                }
            }
        }

        private void SvtPythonPathBtnClicked(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog
            {
                Description = "Select the 'Python' sub-folder of your IntrospectESP install"
            })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string folderPath = dialog.SelectedPath;
                    _svtPythonPath = folderPath;
                    svtPythonPathTxtBox.Text = folderPath;
                }
            }
        }

        // Set the folderPath for serialProtocolDll (only need to do this when running on my Mac)
        // since Windows doesn't like accessing DLLs on a "foreign" disk (e.g. the F drive from my Mac)
        private void SetupSerProtoDllPath()
        {
            const string serProtoDllFolderPath = "C:\\introspectDocs\\SvtPython\\dftm\\comm";
            using (Svt.GetGIL())
            {
                dynamic serProtocolApi = Svt.ImportModule("dftm.comm.local.serProtocolApi");
                dynamic AnySerProtocol = serProtocolApi.AnySerProtocol;
                AnySerProtocol.serProtoDllFolderPath = serProtoDllFolderPath;
            }
        }

        private void ReadDjFromPreviousResultFile()
        {
            const string bertScanResultFolder = null; // Add the path to your Bert Scan results folder here.

            if (!Directory.Exists(bertScanResultFolder))
            {
                LogMsg("No such folder: " + bertScanResultFolder);
            }
            dynamic svtContext = Svt.CreateComponentContext();
            using (Svt.GetGIL())
            {
                dynamic svtNamesDict = svtContext.getNamesDict();
                LogMsg("reading data from folder: " + bertScanResultFolder);
                dynamic SvtBertScan = svtNamesDict.get("SvtBertScan");

                dynamic bertScanResult = SvtBertScan.readResultFiles(bertScanResultFolder);
                // Get the DJ for channel 2:
                const ushort channel = 2;
                dynamic analysis = bertScanResult.getAnalysis(channel);
                float dj1 = analysis.get("jitter").get("dj");
                LogMsg("dj1: " + dj1);
            }
        }

        private void RunBtnClicked(object sender, EventArgs e)
        {
            if (_formFactorName == null)
            {
                throw new Exception("No form factor specified");
            }

            //ReadDjFromPreviousResultFile();

            var channelsStr = channelsTxtBox.Text.Trim();
            var channelStrs = channelsStr.Split(',');
            var numChannels = channelStrs.Length;
            if (numChannels == 0)
            {
                return;
            }
            var channels = new ushort[numChannels];
            try
            {
                for (int i = 0; i < numChannels; i++)
                {
                    var channelStr = channelStrs[i];
                    channels[i] = Convert.ToUInt16(channelStr);
                }
            }
            catch (Exception)
            {
                return;
            }

            string patternsStr = patternTxtBox.Text;
            string[] patternStrs = patternsStr.Split(',');
            if (patternStrs.Length == 0)
            {
                return;
            }

            dynamic svtContext = Svt.CreateComponentContext();
            //setupSerProtoDllPath(); // needed on Mac

            using (Svt.GetGIL())
            {
                dynamic dftSvt = Svt.ImportModule("dftm.svt");

                dynamic globalClockConfig = svtContext.createComponent("SvtGlobalClockConfig");
                globalClockConfig.dataRate = 5000;

                dynamic txChannelList1 = svtContext.createComponent("SvtTxChannelList");

                dynamic rxChannelList1 = svtContext.createComponent("SvtRxChannelList");
                rxChannelList1.channels = channels;
                rxChannelList1.expectedPatterns = patternStrs;

                txChannelList1.channels = channels;
                txChannelList1.patterns = patternStrs;

                // setting the voltages
                txChannelList1.voltageSwings = new List<int> { 800 };

                dynamic patternSync1 = svtContext.createComponent("SvtPatternSync");
                patternSync1.errorIfSyncFails = false; // to be able to continue even if sync on a channel fails

                dynamic bertScan1 = svtContext.createComponent("SvtBertScan");
                bertScan1.rxChannelList = rxChannelList1;
                bertScan1.patternSync = patternSync1;

                bool connected = Svt.Connect();
                if (!connected)
                {
                    dftSvt.printMsg("Failed to connect to Introspect hardware");  // use dftSvt.printMsg instead of logMsg so that messages are synchronized
                    return;
                }

                globalClockConfig.setup();
                // Start the TX pattern and call the BertScan ‘run’ method:
                txChannelList1.setup();
                dynamic bertScanResult = bertScan1.run();

                if (Svt.IsNotNone(bertScanResult))
                {
                    // Get the TJ for each channel
                    foreach (var channel in channels)
                    {
                        dynamic tjValue = bertScanResult.getAnalysisValue(channel, "TJ");
                        if (tjValue != null && Svt.IsNotNone(tjValue))
                        {
                            float tj = tjValue;
                            dftSvt.printMsg("channel " + channel + " tj: " + tj);
                        }
                        else
                        {
                            dftSvt.printMsg("tj is None");
                        }
                    }
                }
                else
                {
                    dftSvt.printMsg("bertScanResult is None");
                }
            }
            Svt.Disconnect();
            Svt.RemoveComponentContext(svtContext);
        }
    }

    public class TextBoxStreamWriter : TextWriter
    {
        private readonly TextBox _textBox;

        // ctor
        public TextBoxStreamWriter(TextBox textBox)
        {
            _textBox = textBox;
        }

        // TODO: isn't this inefficient? Should we supply a method to write a string?
        public override void Write(string value)
        {
            base.Write(value);
            MethodInvoker action = delegate
            {
                _textBox.AppendText(value);
            };
            _textBox.BeginInvoke(action);
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
