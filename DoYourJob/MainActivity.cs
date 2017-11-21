using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Android.Database.Sqlite;

namespace DoYourJob
{
    [Activity(Label = "DoYourJob", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        RecyclerView choreRecyclerView;
        RecyclerView.LayoutManager choreLayoutManager;
        ChoreAdapter choreAdapter;

        public static List<Chore> choreCollection; //Replaced ChoreCollection type with List<Chore>
        public static House myHouse;

        //IOHelper ioHelper;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //-----------LOAD CHORE LIST FROM FILE----------
            //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //string filename = Path.Combine(path, "Json.txt");
            ////Prepare the data source:
            //using (var streamReader = new StreamReader(filename))
            //{
            //    string content = streamReader.ReadToEnd();
            //    choreCollection = JsonConvert.DeserializeObject<List<Chore>>(content);
            //    System.Diagnostics.Debug.WriteLine(content);
            //}
            ////choreCollection = ioHelper.ReadFromJsonFile<List<Chore>>(this);
            //if (choreCollection == null)
            //    choreCollection = new List<Chore>();
            
            //-----------LOAD CHORE LIST FROM DB-----------
            //Get the dbPath
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            dbPath = Path.Combine(dbPath, "do-your-job.db3");

            SQLiteDatabase mydatabase = OpenOrCreateDatabase(dbPath, FileCreationMode.Private, null);
            //Create dbHelper
            DBHelper dbHelper = new DBHelper();
            //Open dbConnection
            dbHelper.OpenConn();
            //Create the table if it does not exist
            dbHelper.CreateHouseTable();

            //check Intent for an updated choreCollection from DeleteChoreButton in ChoreInfoActivity
            if(Intent.HasExtra("updatedChoreList"))
            {
                //FIXME: The DB stuff should be done here instead of in the ChoreInfoActivity
                choreCollection = JsonConvert.DeserializeObject<List<Chore>>(Intent.GetStringExtra("updatedChoreList"));
            }
            
            //check to see if Intent has an extra for my house
            if (Intent.HasExtra("myHouse"))
            {
                myHouse = JsonConvert.DeserializeObject<House>(Intent.GetStringExtra("myHouse"));
                //Update DB by adding House to the table, replacing the previous version if it exists
                dbHelper.AddHouse(myHouse);
            }
            //else if(I don't have a house)
            if(myHouse == null)
            {
                //go to the screen to select a house...
                //start the SelectHouseActivity
                var intent = new Intent(this, typeof(SelectHouseActivity));
                StartActivity(intent);
            }

            //choreCollection = JsonConvert.DeserializeObject<List<Chore>>(myHouse.ListJson);
            if (myHouse != null)
                try
                {
                    choreCollection = dbHelper.GetMyChores(myHouse.HouseName);
                }
                catch
                {
                    Toast.MakeText(this, "Failed to load chores from DB.", ToastLength.Long).Show();
                    if (choreCollection == null)
                        choreCollection = new List<Chore>();
                }

            //-----------ADD NEW CHORE TO FILE-------
            ////Add a new element from our AddChoreActivity to the file
            //if (Intent.HasExtra("NewChore"))
            //{
            //    choreCollection.Add(JsonConvert.DeserializeObject<Chore>(Intent.GetStringExtra("NewChore")));

            //    using (var streamWriter = new StreamWriter(filename, false))
            //    {
            //        streamWriter.Write(JsonConvert.SerializeObject(choreCollection));
            //    }
            //    //ioHelper.WriteToJsonFile<List<Chore>>(this, choreCollection);
            //}

            //-----------ADD NEW CHORE TO DB-------
            if (Intent.HasExtra("NewChore"))
            {
                choreCollection.Add(JsonConvert.DeserializeObject<Chore>(Intent.GetStringExtra("NewChore")));
                //Update DB by adding House to the table, replacing the previous version if it exists
                dbHelper.AddHouse("Dudes", JsonConvert.SerializeObject(choreCollection));
            }

            //-----------SET UP RECYCLERVIEW AND HELPERS------
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
            
            //-----------BUTTONS------------------------------
            Button addChoreButton = FindViewById<Button>(Resource.Id.AddChoreButton);
            Button selectHouseButton = FindViewById<Button>(Resource.Id.SelectHouseButton);

            selectHouseButton.Click += (sender, e) =>
            {
                //FIXME: We should probably pass the list of houses or the dbConnection here instead of opening the dbConnection twice...
                //Bring up the Select Group menu
                var intent = new Intent(this, typeof(SelectHouseActivity));
                StartActivity(intent);
            };

            addChoreButton.Click += (sender, e) =>
            {
                //Bring up the Add Chore menu
                var intent = new Intent(this, typeof(AddChoreActivity));
                StartActivity(intent);
            };

            void OnItemClick(object sender, int position)
            {
                var choreInfoIntent = new Intent(this, typeof(ChoreInfoActivity));
                choreInfoIntent.PutExtra("index", position);
                choreInfoIntent.PutExtra("collection", JsonConvert.SerializeObject(choreCollection));

                StartActivity(choreInfoIntent);
            }
        }
    }
}

