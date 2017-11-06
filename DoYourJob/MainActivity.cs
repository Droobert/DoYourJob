using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
//using Android.Support.V7.RecyclerView;
using Android.Support.V7.Widget;
//using Android.Support.V7.CardView;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace DoYourJob
{
    [Activity(Label = "DoYourJob", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        RecyclerView choreRecyclerView;
        RecyclerView.LayoutManager choreLayoutManager;
        ChoreAdapter choreAdapter;
        public static List<Chore> choreCollection; //Replaced ChoreCollection type with List<Chore>
        //Chore jsonChore;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Prepare the data source:
            if(choreCollection == null)
                choreCollection = new List<Chore>();
            //Add a new element from our AddChoreActivity
            if (Intent.HasExtra("NewChore"))
                choreCollection.Add(JsonConvert.DeserializeObject<Chore>(Intent.GetStringExtra("NewChore")));
            
            //Chore example = new Chore("Dishes", new DateTime().ToLongDateString(), "None");
            //choreCollection.Add(example);
            //Chore example2 = new Chore("Garbage", new DateTime(), "None");
            //choreCollection.Add(example2);
            //Chore example3 = new Chore("Cat Box", new DateTime(), "None");
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);
            //choreCollection.Add(example3);

            // Instantiate the adapter and pass in its data source:
            choreAdapter = new ChoreAdapter(choreCollection);

            // Set our view from the "main" layout resource:
            SetContentView(Resource.Layout.Main);

            // Get our RecyclerView layout:
            choreRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            // Plug the adapter into the RecyclerView:
            choreRecyclerView.SetAdapter(choreAdapter);

            choreLayoutManager = new LinearLayoutManager(this);
            choreRecyclerView.SetLayoutManager(choreLayoutManager);

            Button addButton = FindViewById<Button>(Resource.Id.AddButton);

            addButton.Click += (sender, e) =>
            {
                //Bring up the Add Chore menu

                var intent = new Intent(this, typeof(AddChoreActivity));
                //intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
                //PopupMenu addChoreMenu = new PopupMenu(this, addButton);
                //addChoreMenu.Inflate(Resource.Menu.popup_menu);
                //addChoreMenu.Show();
            };
        }

        //public void addChore(Chore c) { choreCollection.Add(c); }
    }
}

