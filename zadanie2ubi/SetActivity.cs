
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace zadanie2ubi
{
    [Activity(Label = "SetActivity")]
    public class SetActivity : Activity
    {
        Backend backend;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.set_layout);
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            Button button3 = FindViewById<Button>(Resource.Id.button3);
            ListView listView1 = FindViewById<ListView>(Resource.Id.listView1);
            backend = Backend.Instance;
            
            backend.GetInventoryParts(int.Parse(backend.ChosenSet));
            var staticValues = backend.GetBricksStableInfo();

            listView1.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, staticValues);

            listView1.ItemClick +=(sender, e)=>{
                backend.SetBrick(e.Position);
                var intent = new Intent(this, typeof(ChangeActivity));
                StartActivity(intent);
            };
            button1.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ExportSettings));
                  StartActivity(intent);
              };
            button2.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                backend.DeleteInventory(int.Parse(backend.ChosenSet));
                StartActivity(intent);
            };
            button3.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

        }


        protected override void OnRestart()
        {
            base.OnRestart();
            ListView listView1 = FindViewById<ListView>(Resource.Id.listView1);
            backend = Backend.Instance;

            backend.GetInventoryParts(int.Parse(backend.ChosenSet));
            var staticValues = backend.GetBricksStableInfo();
            listView1.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, staticValues);

            listView1.ItemClick += (sender, e) => {
                backend.SetBrick(e.Position);
                var intent = new Intent(this, typeof(ChangeActivity));
                StartActivity(intent);
            };
        }

        protected override void OnPostResume()
        {
            base.OnPostResume();
            ListView listView1 = FindViewById<ListView>(Resource.Id.listView1);
            backend = Backend.Instance;

            backend.GetInventoryParts(int.Parse(backend.ChosenSet));
            var staticValues = backend.GetBricksStableInfo();
            listView1.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, staticValues);

            listView1.ItemClick += (sender, e) => {
                backend.SetBrick(e.Position);
                var intent = new Intent(this, typeof(ChangeActivity));
                StartActivity(intent);
            };
        }

    }
}
