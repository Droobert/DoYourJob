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
    [Activity(Label = "AddHouseActivity")]
    public class AddHouseActivity : Activity
    {
        Button createHouseButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddHouse);

            createHouseButton = FindViewById<Button>(Resource.Id.CreateHouseButton);

            createHouseButton.Click += (sender, e) =>
            {
                createHouse();
            };
        }

        public void createHouse()
        {
            House h = new House(FindViewById<EditText>(Resource.Id.HouseNameEditText).Text,
                                FindViewById<EditText>(Resource.Id.LocationEditText).Text);

            var mainActivity = new Intent(this, typeof(MainActivity));
            mainActivity.PutExtra("myHouse", JsonConvert.SerializeObject(h));
            StartActivity(mainActivity);
        }
    }
}