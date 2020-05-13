
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
            button2.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                // delete this set
                StartActivity(intent);
            };
            button3.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
            backend = Backend.Instance;
        }
    }
}
