using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Diagnostics;

namespace MultiDownloadSample
{
   
    static internal class MultiAsyncDownload
    {

        /// <summary>
        ///     Downloads a file from a specified Internet address.
        /// </summary>
        /// <param name="url">Internet address of the file to download.</param>
        /// <param name="localPath">
        ///     Local file name where to store the content of the download, if null a temporary file name will
        ///     be generated.
        /// </param>
        /// <param name="timeOut">Duration in miliseconds before cancelling the  operation.</param>
        /// <returns>A tuple containing the remote path, the local path and an exception if one occurred.</returns>
        public static async Task<Tuple<string,  Exception>> DownloadFileTaskAsyncWithTimeout(string url, string dirPath,
             IProgress<Tuple<string, double>> progress,CancellationToken token, int timeOut = 3000)
        {
            try
            {
                if (url == null)
                {
                    Debug.WriteLine("DownloadFileTaskAsync (null remote path): skipping");
                    throw new ArgumentNullException("remotePath");
                }

                // TODO check the filename duplication
                var  localPath = dirPath + System.IO.Path.GetFileName(url) + ".download";
                Debug.WriteLine("DownloadFileTaskAsync save #{localPath}");

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(timeOut);
                    await DownloadFileAsync(client, url, localPath,progress,token);
                    Debug.WriteLine(string.Format("DownloadFileTaskAsync (downloaded): {0}", url));
                    return new Tuple<string,  Exception>(url, null);
                }
            }
            catch (Exception ex)
            {
                return new Tuple<string, Exception>(url, ex);
            }
        }

#if false

        public static async Task Main()
    {
        var tokenSource = new CancellationTokenSource();
        var token = tokenSource.Token;

        // Store references to the tasks so that we can wait on them and
        // observe their status after cancellation.
        Task t;
        var tasks = new ConcurrentBag<Task>();

        Console.WriteLine("Press any key to begin tasks...");
        Console.ReadKey(true);
        Console.WriteLine("To terminate the example, press 'c' to cancel and exit...");
        Console.WriteLine();

        // Request cancellation of a single task when the token source is canceled.
        // Pass the token to the user delegate, and also to the task so it can
        // handle the exception correctly.
        t = Task.Run(() => DoSomeWork(1, token), token);
        Console.WriteLine("Task {0} executing", t.Id);
        tasks.Add(t);

        // Request cancellation of a task and its children. Note the token is passed
        // to (1) the user delegate and (2) as the second argument to Task.Run, so
        // that the task instance can correctly handle the OperationCanceledException.
        t = Task.Run(() =>
        {
            // Create some cancelable child tasks.
            Task tc;
            for (int i = 3; i <= 10; i++)
            {
                // For each child task, pass the same token
                // to each user delegate and to Task.Run.
                tc = Task.Run(() => DownloadFileAsync(i, token), token);
                Console.WriteLine("Task {0} executing", tc.Id);
                tasks.Add(tc);
                // Pass the same token again to do work on the parent task.
                // All will be signaled by the call to tokenSource.Cancel below.
                DoSomeWork(2, token);
            }
        }, token);

        Console.WriteLine("Task {0} executing", t.Id);
        tasks.Add(t);

        // Request cancellation from the UI thread.
        char ch = Console.ReadKey().KeyChar;
        if (ch == 'c' || ch == 'C')
        {
            tokenSource.Cancel();
            Console.WriteLine("\nTask cancellation requested.");

            // Optional: Observe the change in the Status property on the task.
            // It is not necessary to wait on tasks that have canceled. However,
            // if you do wait, you must enclose the call in a try-catch block to
            // catch the TaskCanceledExceptions that are thrown. If you do
            // not wait, no exception is thrown if the token that was passed to the
            // Task.Run method is the same token that requested the cancellation.
        }

        try
        {
            await Task.WhenAll(tasks.ToArray());
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"\n{nameof(OperationCanceledException)} thrown\n");
        }
        finally
        {
            tokenSource.Dispose();
        }

        // Display status of all tasks.
        foreach (var task in tasks)
            Console.WriteLine("Task {0} status is now {1}", task.Id, task.Status);
    }

    static void DoSomeWork(int taskNum, CancellationToken ct)
    {
        // Was cancellation already requested?
        if (ct.IsCancellationRequested)
        {
            Console.WriteLine("Task {0} was cancelled before it got started.",
                              taskNum);
            ct.ThrowIfCancellationRequested();
        }

        int maxIterations = 100;

        // NOTE!!! A "TaskCanceledException was unhandled
        // by user code" error will be raised here if "Just My Code"
        // is enabled on your computer. On Express editions JMC is
        // enabled and cannot be disabled. The exception is benign.
        // Just press F5 to continue executing your code.
        for (int i = 0; i <= maxIterations; i++)
        {
            // Do a bit of work. Not too much.
            var sw = new SpinWait();
            for (int j = 0; j <= 100; j++)
                sw.SpinOnce();

            if (ct.IsCancellationRequested)
            {
                Console.WriteLine("Task {0} cancelled", taskNum);
                ct.ThrowIfCancellationRequested();
            }
        }
    }
#endif

        private static async Task DownloadFileAsync(HttpClient client, string url, string localpath, IProgress<Tuple<string, double>> progress, CancellationToken token)
    {
        var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(string.Format("The request returned with HTTP status code {0}", response.StatusCode));
        }

        var total = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : -1L;
        var canReportProgress = total != -1 && progress != null;

        using (var stream = await response.Content.ReadAsStreamAsync())
        {
            var totalRead = 0L;
            var buffer = new byte[4096];
            var isMoreToRead = true;

            do
            {
                token.ThrowIfCancellationRequested();

                var read = await stream.ReadAsync(buffer, 0, buffer.Length, token);

                if (read == 0)
                {
                    isMoreToRead = false;
                }
                else
                {
                    var data = new byte[read];
                    buffer.ToList().CopyTo(0, data, 0, read);

                    // TODO: put here the code to write the file to disk

                    totalRead += read;

                    if (canReportProgress)
                    {
                        progress.Report(new Tuple<string,double>(url,(totalRead * 1d) / (total * 1d) * 100));
                    }
                }
            } while (isMoreToRead);
        }
    }
    }
}
