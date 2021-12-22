#define USE_CONCURRENTBAG 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace MultiDownloadSample
{
    public partial class Form1 : Form
    {

        private IEnumerable<string> urlList;
        private CancellationTokenSource downlodCts;
        private Task multiDownloadTask;
        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateProgressUI(Tuple<string, double> val)
        { 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            urlList = new string[]{ "http://localhost/help/aegis_designer_custom.png", 
                "http://localhost/17763.737.190906-2324.rs5_release_svc_refresh_SERVER_EVAL_x64FRE_en-us_1.iso"};
        }



        private async void btnStart_Click(object sender, EventArgs e)
        {
            downlodCts = new CancellationTokenSource();
            var progressIndicator = new Progress<Tuple<string,double>>(UpdateProgressUI);
            var results = new List<Tuple<string,  Exception>>();
            var downloadPath = KnownFolders.GetPath(KnownFolder.Downloads);
            try
            {
                multiDownloadTask = urlList.ForEachAsync(s => MultiAsyncDownload.DownloadFileTaskAsyncWithTimeout(s, downloadPath, progressIndicator, downlodCts.Token), (url, t) => results.Add(t),downlodCts.Token);
                await multiDownloadTask.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // if modeless what happens?
            }
            finally
            {
                Debug.WriteLine("Release the task and token");   
                multiDownloadTask.Dispose();
                multiDownloadTask = null;
                downlodCts.Dispose();
                downlodCts = null;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (downlodCts != null && multiDownloadTask != null)
            {
                downlodCts.Cancel();
                multiDownloadTask.Wait();
            }
        }

        private void btnDownloadCancel_Click(object sender, EventArgs e)
        {
            if (downlodCts != null && multiDownloadTask != null)
            {
                downlodCts.Cancel();
            }
        }
    }


    public static class Extensions
    {
        public static Task ForEachAsync<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, Task<TResult>> taskSelector, Action<TSource, TResult> resultProcessor, CancellationToken token)
        {
#if USE_CONCURRENTBAG

            var tasks = new ConcurrentBag<Task>();

            foreach (var item in source)
            {
                var t = Task.Run(() => ProcessAsync(item, taskSelector, resultProcessor, null), token);            
            }

            return Task.WhenAll(tasks.ToArray());
#else

            var oneAtATime = new SemaphoreSlim(5, 10);
            return Task.WhenAll(
                from item in source
                select ProcessAsync(item, taskSelector, resultProcessor, oneAtATime));
#endif

        }

        private static async Task ProcessAsync<TSource, TResult>(
            TSource item,
            Func<TSource, Task<TResult>> taskSelector, Action<TSource, TResult> resultProcessor,
            SemaphoreSlim oneAtATime)
        {
            // actual download
            TResult result = await taskSelector(item);
#if USE_CONCURRENTBAG
            // should rename from .download to original name or virsus scan??
            resultProcessor(item, result);
#else

            await oneAtATime.WaitAsync();
            try
            {
                resultProcessor(item, result);
            }
            finally
            {
                oneAtATime.Release();
            }
#endif
        }
    }

}
