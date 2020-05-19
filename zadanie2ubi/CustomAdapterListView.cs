
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

    public class ViewHolder : Java.Lang.Object
    {
        public TextView txta { get; set; }
        public TextView txtb { get; set; }

    public class CustomAdapterListView : BaseAdapter
    {

            private Activity activity;
            private List<InventoryPart> parts;
            public override int Count =>  parts.Count;


            public CustomAdapterListView(Activity acctivity, List<InventoryPart> inventoryParts)
            { activity = acctivity;
                parts = inventoryParts;
            }

            public override Java.Lang.Object GetItem(int position)
        {
                return null;
        }

        public override long GetItemId(int position)
        {
                return parts[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
                var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.abc_list_menu_item_layout, parent, false);

        }
    }
}
