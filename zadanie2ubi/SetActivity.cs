
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
            ListView listView2 = FindViewById<ListView>(Resource.Id.listView2);
            ListView listView3 = FindViewById<ListView>(Resource.Id.listView3);
            backend = Backend.Instance;
            if (backend.ChosenSet == "None")
            {
                String[] list = { "Nie wybrano", "żadnego zestawu", "do wyświetlenia :(" };
                listView1.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, list);
            }
            else {
                backend.GetInventoryParts(int.Parse(backend.ChosenSet));
                listView1.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, backend.GetBricksStableInfo());
                listView2.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, backend.GetBricksNums());
                //listView.Adapter = new ArrayAdapter(this,  Android.Resource.Layout.SimpleListItem1, setlist );
            }

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
    }
}
