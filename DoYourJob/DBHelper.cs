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
using SQLite;
using System.IO;
using Newtonsoft.Json;

namespace DoYourJob
{
    class DBHelper
    {
        public SQLiteConnection dbConn;

        //public DBHelper()
        //{
        //    dbConn = new SQLiteConnection("foofoo");
        //}

        public void OpenConn()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "do-your-job.db3");
            dbConn = new SQLiteConnection(path);
        }

        //FIXME: We should have a function to drop a table if its borked
        //FIXME: This should be a generic DropTable(Type T) function
        public void DropHouseTable()
        {
            return;
        }

        //FIXME: This should be a generic CreateTable(Type T) function
        public void CreateHouseTable()
        {
            //Executes a "Create table if not exists" command on the DB
            dbConn.CreateTable<House>();
        }

        public void AddHouse(string h, string c)
        {
            House house = new House(h, c);
            //if(db does not already have the house in it...)
            dbConn.InsertOrReplace(house);
        }

        public void AddHouse(House house)
        {
            dbConn.InsertOrReplace(house);
        }

        public List<House> QueryHouses()
        {
            return dbConn.Query<House>("SELECT * FROM House");
        }

        public List<Chore> GetMyChores(string houseName)
        {
            return JsonConvert.DeserializeObject<List<Chore>>(dbConn.ExecuteScalar<string>("SELECT ListJson FROM House WHERE HouseName = ?", houseName));
        }
    }
}