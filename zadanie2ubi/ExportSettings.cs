
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
        EditText path;
        TextView text;
        Button butt1;
        Button butt2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.settings);
            backend = Backend.Instance;
            EditText path = FindViewById<EditText>(Resource.Id.editText2);
            TextView text = FindViewById<TextView>(Resource.Id.textView2);
            Button butt1 = FindViewById<Button>(Resource.Id.button1);
            Button butt2 = FindViewById<Button>(Resource.Id.button2);
            text.Text = "Status zapisu:";
            butt1.Click += (sender, e) =>
              {
                  try
                  {
                      
                      var file = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/Result.xml";
                      if (System.IO.File.Exists(file))
                      {
                          System.IO.File.Copy(file, path.Text, true);
                          text.Text = "Zapisano";
                      }
                      else
                          text.Text = "Nie wygenerował się xml";
                  }
                  catch (Exception ex)
                  {
                      text.Text = "Nie udało się zapisać. Istnieje taki folder i masz prawa zapisu?";
                  }
              };
            butt2.Click += (sender, e) =>
              {
                  var intent = new Intent(this, typeof(MainActivity));
                  StartActivity(intent);
              };
        }

    }
}
