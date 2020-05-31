
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
    [Activity(Label = "AddActivity")]
    public class AddActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.add_set_layout);
            EditText setId = FindViewById<EditText>(Resource.Id.editText1);
            EditText setName = FindViewById<EditText>(Resource.Id.editText2);
            EditText partType = FindViewById<EditText>(Resource.Id.editText3);
            EditText partItem = FindViewById<EditText>(Resource.Id.editText4);
            EditText partQuantity = FindViewById<EditText>(Resource.Id.editText5);
            EditText partColor = FindViewById<EditText>(Resource.Id.editText6);
            EditText partExtra = FindViewById<EditText>(Resource.Id.editText7);
            Button set = FindViewById<Button>(Resource.Id.button1);
            Button part = FindViewById<Button>(Resource.Id.button2);
            Button toMenu = FindViewById<Button>(Resource.Id.button3);

            Backend backend = Backend.Instance;
            set.Click += (sender, e) =>
             {
                 if (setId.Text.Length > 0 & setName.Text.Length > 0)
                     backend.CreateInventory(int.Parse(setId.Text), setName.Text, 1, 0);
             };

            part.Click += (sender, e) =>
              {
                  if (setId.Text.Length > 0 & partType.Text.Length > 0 & partItem.Text.Length > 0 &
                      partQuantity.Text.Length > 0 & partColor.Text.Length > 0 & partExtra.Text.Length > 0)
                      backend.CreatInventoryPart(int.Parse(setId.Text), int.Parse(partType.Text),
                          int.Parse(partItem.Text), int.Parse(partQuantity.Text), int.Parse(partColor.Text), int.Parse(partExtra.Text));
              };

            toMenu.Click += (sender, e) =>
             {
                 var intent = new Intent(this, typeof(MainActivity));
                 StartActivity(intent);
             };
        }
    }
}
