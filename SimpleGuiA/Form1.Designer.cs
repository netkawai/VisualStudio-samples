namespace TestWindowsForms
{
    partial class MainWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logTxtBox = new System.Windows.Forms.TextBox();
            this.runBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.channelsTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.patternTxtBox = new System.Windows.Forms.TextBox();
            this.formFactorBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.formFactorTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.svtPythonPathTxtBox = new System.Windows.Forms.TextBox();
            this.svtPythonPathBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logTxtBox
            // 
            this.logTxtBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logTxtBox.Location = new System.Drawing.Point(0, 162);
            this.logTxtBox.Multiline = true;
            this.logTxtBox.Name = "logTxtBox";
            this.logTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTxtBox.Size = new System.Drawing.Size(481, 175);
            this.logTxtBox.TabIndex = 0;
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(199, 133);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(75, 23);
            this.runBtn.TabIndex = 3;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.RunBtnClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Channels:";
            // 
            // channelsTxtBox
            // 
            this.channelsTxtBox.Location = new System.Drawing.Point(92, 62);
            this.channelsTxtBox.Name = "channelsTxtBox";
            this.channelsTxtBox.Size = new System.Drawing.Size(245, 20);
            this.channelsTxtBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Patterns:";
            // 
            // patternTxtBox
            // 
            this.patternTxtBox.Location = new System.Drawing.Point(92, 88);
            this.patternTxtBox.Name = "patternTxtBox";
            this.patternTxtBox.Size = new System.Drawing.Size(245, 20);
            this.patternTxtBox.TabIndex = 7;
            // 
            // formFactorBtn
            // 
            this.formFactorBtn.Location = new System.Drawing.Point(343, 34);
            this.formFactorBtn.Name = "formFactorBtn";
            this.formFactorBtn.Size = new System.Drawing.Size(119, 23);
            this.formFactorBtn.TabIndex = 8;
            this.formFactorBtn.Text = "Set Form Factor...";
            this.formFactorBtn.UseVisualStyleBackColor = true;
            this.formFactorBtn.Click += new System.EventHandler(this.FormFactorBtnClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Form Factor:";
            // 
            // formFactorTxtBox
            // 
            this.formFactorTxtBox.Location = new System.Drawing.Point(92, 36);
            this.formFactorTxtBox.Name = "formFactorTxtBox";
            this.formFactorTxtBox.ReadOnly = true;
            this.formFactorTxtBox.Size = new System.Drawing.Size(245, 20);
            this.formFactorTxtBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "SvtPythonPath:";
            // 
            // svtPythonPathTxtBox
            // 
            this.svtPythonPathTxtBox.Location = new System.Drawing.Point(92, 9);
            this.svtPythonPathTxtBox.Name = "svtPythonPathTxtBox";
            this.svtPythonPathTxtBox.ReadOnly = true;
            this.svtPythonPathTxtBox.Size = new System.Drawing.Size(245, 20);
            this.svtPythonPathTxtBox.TabIndex = 12;
            // 
            // svtPythonPathBtn
            // 
            this.svtPythonPathBtn.Location = new System.Drawing.Point(343, 7);
            this.svtPythonPathBtn.Name = "svtPythonPathBtn";
            this.svtPythonPathBtn.Size = new System.Drawing.Size(119, 23);
            this.svtPythonPathBtn.TabIndex = 13;
            this.svtPythonPathBtn.Text = "Set SvtPythonPath...";
            this.svtPythonPathBtn.UseVisualStyleBackColor = true;
            this.svtPythonPathBtn.Click += new System.EventHandler(this.SvtPythonPathBtnClicked);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 337);
            this.Controls.Add(this.svtPythonPathBtn);
            this.Controls.Add(this.svtPythonPathTxtBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.formFactorTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.formFactorBtn);
            this.Controls.Add(this.patternTxtBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.channelsTxtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.logTxtBox);
            this.Name = "MainWin";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logTxtBox;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox channelsTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox patternTxtBox;
        private System.Windows.Forms.Button formFactorBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox formFactorTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox svtPythonPathTxtBox;
        private System.Windows.Forms.Button svtPythonPathBtn;
    }
}

