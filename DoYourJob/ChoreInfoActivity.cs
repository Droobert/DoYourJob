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
using Newtonsoft.Json;

namespace DoYourJob
{
    [Activity(Label = "ChoreInfoActivity")]
    public class ChoreInfoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ChoreInfo);
            // Create your application here
            //Pull our choreCollection in from the Intent extras
            List<Chore> choreCollection = JsonConvert.DeserializeObject<List<Chore>>(Intent.GetStringExtra("collection"));

            int index = Intent.GetIntExtra("index", -1);

            if (index < 0)
                return;

            FindViewById<TextView>(Resource.Id.nameTextDisplay).Text = choreCollection[index].name;
            FindViewById<TextView>(Resource.Id.dateTextDisplay).Text = choreCollection[index].date;
            FindViewById<TextView>(Resource.Id.detailsTextDisplay).Text = choreCollection[index].details;
        }
    }
}