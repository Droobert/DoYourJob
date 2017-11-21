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
using Android.Support.V7.Widget;

namespace DoYourJob
{
    public class HouseViewHolder:RecyclerView.ViewHolder
    {
        //public ImageView Image { get; private set; }
        //public TextView Caption { get; private set; }
        public TextView HouseName { get; private set; }
        //public TextView Description { get; private set; }
        public TextView Location { get; private set; }

        public HouseViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            //Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            //Caption = itemView.FindViewById<TextView>(Resource.Id.textView);
            HouseName = itemView.FindViewById<TextView>(Resource.Id.houseNameView);
            Location = itemView.FindViewById<TextView>(Resource.Id.houseLocationView);

            itemView.Click += (sender, e) => listener(Position);
        }
    }
}