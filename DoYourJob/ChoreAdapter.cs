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
    public class ChoreAdapter : RecyclerView.Adapter
    {
        //Include an event so our client can act when a user touches individual items
        public event EventHandler<int> ItemClick;
        //include the data source for our Adapter
        public List<Chore> choreCollection;
        //Constructor takes a List of Chores
        public ChoreAdapter(List<Chore> cCollection)
        {
           choreCollection = cCollection;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.ChoreCardView, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            ChoreViewHolder vh = new ChoreViewHolder(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ChoreViewHolder vh = holder as ChoreViewHolder;

            // Load the Chore Name from the container:
            vh.Name.Text = choreCollection[position].name;
            // Load the Chore Date from the container:
            vh.Date.Text = choreCollection[position].date;

            // Load the photo image resource from the photo album:
            //   vh.Image.SetImageResource(mPhotoAlbum[position].PhotoID);
            // Load the photo caption from the photo album:
            //  vh.Caption.Text = mPhotoAlbum[position].Caption;
        }

        public override int ItemCount
        {
            get {
                if (choreCollection != null)
                    return choreCollection.Count;
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


//public class PhotoAlbumAdapter : RecyclerView.Adapter
//{
//    public PhotoAlbum mPhotoAlbum;

//    public PhotoAlbumAdapter(PhotoAlbum photoAlbum)
//    {
//        mPhotoAlbum = photoAlbum;
//    }
//    ...
//}