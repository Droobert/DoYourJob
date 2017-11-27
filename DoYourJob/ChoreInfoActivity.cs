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
        Button setReminderButton;
        Button deleteChoreButton;

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

            setReminderButton = FindViewById<Button>(Resource.Id.setReminderButton);
            deleteChoreButton = FindViewById<Button>(Resource.Id.deleteChoreButton);

            setReminderButton.Click += (sender, e) =>
            {
                Remind(DateTime.Parse(choreCollection[index].date), choreCollection[index].name, choreCollection[index].details);
            };
            deleteChoreButton.Click += (sender, e) =>
            {
                choreCollection.Remove(choreCollection[index]);
                //FIXME: We should probably not be opening the DB connection multiple times, but for now this is what we will do
                //DBHelper dbHelper = new DBHelper();
                //dbHelper.OpenConn();
                //dbHelper.AddHouse("Dudes", JsonConvert.SerializeObject(choreCollection));


                var mainActivity = new Intent(this, typeof(MainActivity));
                mainActivity.PutExtra("shortenedChoreList", JsonConvert.SerializeObject(choreCollection));
                StartActivity(mainActivity);
            };
        }

        public void Remind(DateTime dateTime, string title, string message)
        {

            Intent alarmIntent = new Intent(this, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)GetSystemService(Context.AlarmService);

            //TODO: For demo set after 5 seconds.
            //TODO: Set alarm to go off at a selected time of a specific day.
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, pendingIntent);

        }
    }
}