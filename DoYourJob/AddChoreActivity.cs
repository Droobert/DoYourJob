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
    [Activity(Label = "AddChoreActivity")]
    public class AddChoreActivity : Activity
    {
        //TextView dateDisplay;
        Button dateButton;
        Button submitButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddChore);
            // Create your application here

            dateButton = FindViewById<Button>(Resource.Id.selectDateButton);

            dateButton.Click += (sender, e) =>
            {
                pickDate(sender, e);
            };

            submitButton = FindViewById<Button>(Resource.Id.submitButton);

            submitButton.Click += (sender, e) =>
            {
                createChore();
            };

            //_dateSelectButton = FindViewById<Button>(Resource.Id.dateSelectButton);
            //_dateDisplay.Click += DateSelect_OnClick;
        }

        void pickDate(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime date)
            {
                dateButton.Text = date.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        public void createChore()
        {
            Chore c = new Chore(FindViewById<EditText>(Resource.Id.choreEditText).Text, 
                                FindViewById<Button>(Resource.Id.selectDateButton).Text, 
                                FindViewById<EditText>(Resource.Id.descriptionEditText).Text);

            //if (c.date != null && c.name != null && c.details != null)
            //    Toast.MakeText(this, c.date, ToastLength.Short).Show();
            //else
            //    Toast.MakeText(this, "You failed to chore!", ToastLength.Long).Show();

            //string temp = JsonConvert.SerializeObject(c);
            //Chore d = JsonConvert.DeserializeObject<Chore>(temp);

            //if (d.date != null && d.name != null && d.details != null)
            //    Toast.MakeText(this, d.date, ToastLength.Long).Show();
            //else
            //    Toast.MakeText(this, "You failed to JSON!", ToastLength.Long).Show();

            var mainActivity = new Intent(this, typeof(MainActivity));
            mainActivity.PutExtra("NewChore", JsonConvert.SerializeObject(c));
            StartActivity(mainActivity);
            //MainActivity.addChore(c);
            //MainActivity.choreCollection.Add(c);
        }
    }
}