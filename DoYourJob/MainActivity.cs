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
using Android.Content.Res;
using System.IO;

namespace DoYourJob
{
    [Activity(Label = "DoYourJob", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        RecyclerView choreRecyclerView;
        RecyclerView.LayoutManager choreLayoutManager;
        ChoreAdapter choreAdapter;
        public static List<Chore> choreCollection; //Replaced ChoreCollection type with List<Chore>

        //IOHelper ioHelper;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //ioHelper = new IOHelper();

            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "Json.txt");
            // Prepare the data source:
            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                choreCollection = JsonConvert.DeserializeObject<List<Chore>>(content);
                //System.Diagnostics.Debug.WriteLine(content);
            }
            // choreCollection = ioHelper.ReadFromJsonFile<List<Chore>>(this);            
            if (choreCollection == null)
                    choreCollection = new List<Chore>();

            //Add a new element from our AddChoreActivity
            if (Intent.HasExtra("NewChore"))
            {
                choreCollection.Add(JsonConvert.DeserializeObject<Chore>(Intent.GetStringExtra("NewChore")));

                using (var streamWriter = new StreamWriter(filename, false))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(choreCollection));
                }
                //ioHelper.WriteToJsonFile<List<Chore>>(this, choreCollection);
            }
            
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

            //Attach the event for individual items being clicked:
            choreAdapter.ItemClick += OnItemClick;
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

            void OnItemClick(object sender, int position)
            {
                var choreInfoIntent = new Intent(this, typeof(ChoreInfoActivity));
                choreInfoIntent.PutExtra("index", position);
                choreInfoIntent.PutExtra("collection", JsonConvert.SerializeObject(choreCollection));
                //choreInfoIntent.PutExtra("imageResourceId", SharedData.CrewManifest[position].PhotoResourceId);

                StartActivity(choreInfoIntent);
            }
        }

        //public void addChore(Chore c) { choreCollection.Add(c); }
    }
}

