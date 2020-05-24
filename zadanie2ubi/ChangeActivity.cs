
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
using zadanie2ubi.ObjectTypes;

namespace zadanie2ubi
{
    [Activity(Label = "ChangeActivity")]
    public class ChangeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_layout);
            TextView text = FindViewById<TextView>(Resource.Id.textView2);
            Backend backend = Backend.Instance;
            Button but = FindViewById<Button>(Resource.Id.button1);
            EditText editText = FindViewById<EditText>(Resource.Id.editText1);
            InventoryPart brick = backend.Brick;
            editText.Text = brick.QuantityInStore.ToString();
            text.Text = "Na " + brick.QuantityInSet.ToString();

            but.Click += (sender, e) =>
              {
                  if (editText.Text.Length!=0)
                  {
                      brick.Change(int.Parse(editText.Text));
                      backend.SaveBrick(brick);

                  }
                  var intent = new Intent(this, typeof(SetActivity));
                  StartActivity(intent);
              };
            // Create your application here
        }
    }
}
