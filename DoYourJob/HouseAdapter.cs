using System;
using System.Collections.Generic;
using Android.Views;
using Android.Support.V7.Widget;

namespace DoYourJob
{
    class HouseAdapter : RecyclerView.Adapter
    {
        //Include an event so our client can act when a user touches individual items
        public event EventHandler<int> ItemClick;
        //include the data source for our Adapter
        public List<House> houseCollection;
        //Constructor takes a List of Chores
        public HouseAdapter(List<House> hCollection)
        {
            houseCollection = hCollection;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.HouseCardView, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            HouseViewHolder vh = new HouseViewHolder(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            HouseViewHolder vh = holder as HouseViewHolder;

            // Load the Chore Name from the container:
            vh.HouseName.Text = houseCollection[position].HouseName;
            // Load the Chore Date from the container:
            vh.Location.Text = houseCollection[position].Location;
        }

        public override int ItemCount
        {
            get
            {
                if (houseCollection != null)
                    return houseCollection.Count;
                else
                    return 0;
            }
        }

        private void OnClick(int position)
        {
            //if (ItemClick != null)
            //{
            ItemClick?.Invoke(this, position);
            //}
        }
    }
}