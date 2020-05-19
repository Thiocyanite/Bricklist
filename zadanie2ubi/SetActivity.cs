
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
                var nums = backend.GetBricksNums();
                //listView1.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, backend.GetBricksStableInfo());
                List<Tuple<string, string, Button, Button>> vs = new List<Tuple<string, string, Button, Button>>();
                for (int i = 0; i < staticValues.Count; i++)
                {
                string sign = "+";
                    Button plusButt = new Button(this);
                    Button minusButt = new Button(this);
                    
                    plusButt.Click += (sender, e)=>{ 
                        backend.Bricks[i].Add();
                    };
                    minusButt.Click += (sender, e)=>{
                        backend.Bricks[i].Remove();
                    };
                    vs.Add(Tuple.Create<string, string, Button, Button>(staticValues[i], nums[i], plusButt, minusButt));
                }
                listView1.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, vs);
            

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
