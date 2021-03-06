﻿using System;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using static Android.Manifest;

namespace zadanie2ubi
{
    [Activity(Label = "Klocki", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        Backend backend;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_main);


            Button button1 = FindViewById<Button>(Resource.Id.button1);
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            ListView listView1 = FindViewById<ListView>(Resource.Id.listView1);
            TextView textView1 = FindViewById<TextView>(Resource.Id.textView1);
            textView1.Text = "Nie wybrano żadnego zestawu";
            backend = Backend.Instance;
            var setlist = backend.GetSetNames();
            listView1.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, setlist);
            
            button1.Click += (sender, e) =>
             {
                 var intent = new Intent(this, typeof(AddActivity));
                 StartActivity(intent);
             };
           
            button2.Click += (sender, e) =>
              {
                  if (backend.ChosenSet == "None")
                  {
                      textView1.Text = "Najpierw wybierz zestaw";
                  }
                  else
                  {
                      var intent = new Intent(this, typeof(SetActivity));
                      StartActivity(intent);
                  }
                  
              };

            listView1.ItemClick += (sender, e) =>
              {
                  String selectedOne = (String)listView1.GetItemAtPosition(e.Position);
                  backend.ChosenSet = selectedOne.Split(" ")[0];
                  textView1.Text="Wybrany zestaw "+(String)listView1.GetItemAtPosition(e.Position);
              };
            
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 0);
            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        
    }
}

