using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.Webkit;

namespace zadanie2ubi
{
    public class Downloader 
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public async Task DownloadFile(string url, string folder)
        {
            string pathToNewFolder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);
            Directory.CreateDirectory(pathToNewFolder);

            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                await webClient.DownloadFileTaskAsync(new Uri(url), folder);
                
              
            }
            catch (Exception ex)
            {
 
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                System.Console.WriteLine( e.Error);
            }
        }
    }
}
