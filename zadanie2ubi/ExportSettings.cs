
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Google.Apis.Auth.OAuth2;

namespace zadanie2ubi
{
    [Activity(Label = "ExportSettings")]

    public class ExportSettings : Activity
    {
        Backend backend;
        UserCredential credential;
        static string[] Scopes = { DriveService.Scope.DriveReadonly };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.settings);
            backend = Backend.Instance;
            EditText password = FindViewById<EditText>(Resource.Id.editText1);
            EditText login = FindViewById<EditText>(Resource.Id.editText2);
            Button butt1 = FindViewById<Button>(Resource.Id.button1);
            Button butt2 = FindViewById<Button>(Resource.Id.button2);

            butt1.Click += (sender, e) =>
              {
                  Export(login.Text);
              };
            butt2.Click += (sender, e) =>
              {
                  var intent = new Intent(this, typeof(MainActivity));
                  StartActivity(intent);
              };
        }

        private void Export(string login)
        {
            var file = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            file=file.Remove(file.Length - 5);
            file+= "Result.xml";
            using (var stream =
                new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    login,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Bricklist",
            });
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var fil in files)
                {
                    Console.WriteLine("{0} ({1})", fil.Name, fil.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            Console.Read();
        }

    }
}
