using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Support.V7.Widget;
using Newtonsoft.Json;

namespace DoYourJob
{
    [Activity(Label = "SelectHouseActivity")]
    public class SelectHouseActivity : Activity
    {
        Button addHouseButton;
        List<House> houseCollection;

        RecyclerView houseRecyclerView;
        RecyclerView.LayoutManager houseLayoutManager;
        HouseAdapter houseAdapter;
        //DONE: Add some kind of list view (another recycler view maybe?) to display the list of houses we fetched from the DB
        //DONE: Add a way to click/select which house from the list will become myHouse (another click the items in the recycler view?) 
        //DONE: Add button for creating a house (and then they will be added to the table)
        //DONE: Add fields in the createHouseActivity and a confirm/submit button to go through with making the new house
        //DONE: Add multiple houses to the table
        //FIXME: The houses are all displaying The Moon as their location in the SelectHouseActivity
        //FIXME: All of the houses are displaying the same list of chores in MainActivity. 
            //Its always the first list of chores loaded.
        //FIXME: All of the houses are failing to load their chores from the DB in MainActivity.

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //-----------POPULATE HOUSECOLLECTION FROM DB------
            //Create a dbHelper
            DBHelper dbHelper = new DBHelper();
            //Open connection to DB
            dbHelper.OpenConn();
            //Download list of houses from DB
            //While loop to debug
            while(houseCollection == null)
                houseCollection = dbHelper.QueryHouses();
            
            
            //-----------SET UP RECYCLERVIEW AND HELPERS------
            //Create our House Adapter
            houseAdapter = new HouseAdapter(houseCollection);
            //Set our view
            SetContentView(Resource.Layout.SelectHouse);
            //Get our RecyclerView layout:
            houseRecyclerView = FindViewById<RecyclerView>(Resource.Id.HouseRecyclerView);
            // Plug the adapter into the RecyclerView:
            houseRecyclerView.SetAdapter(houseAdapter);
            //Set up our layout manager
            houseLayoutManager = new LinearLayoutManager(this);
            //Give the recycler view to our layout manager
            houseRecyclerView.SetLayoutManager(houseLayoutManager);

            //-----------BUTTONS------------------------------
            //Attach the event for individual recyclerview items being clicked:
            houseAdapter.ItemClick += OnItemClick;
            //Find the button to select a house
            addHouseButton = FindViewById<Button>(Resource.Id.AddHouseButton);
            //Hook up the EDP
            addHouseButton.Click += (sender, e) =>
            {
                //Bring up the Add House menu
                var intent = new Intent(this, typeof(AddHouseActivity));
                StartActivity(intent);
            };

            void OnItemClick(object sender, int position)
            {
                //When a house is clicked, it should either be highlighted with a radio button syle,
                //or immediately chosen as the house and we return back to main activity with myHouse
                //For now lets go with immediately chosen as myHouse
                //STYLE POINTS: I kinda want a radio buttons feel for this.
                var mainIntent = new Intent(this, typeof(MainActivity));
                mainIntent.PutExtra("myHouse", JsonConvert.SerializeObject(houseCollection[position]));
               // mainIntent.PutExtra("houseCollection", JsonConvert.SerializeObject(houseCollection));

                StartActivity(mainIntent);
            }
        }
    }
}