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

namespace DoYourJob
{
    public class House
    {
        //[PrimaryKey, AutoIncrement]
        //public int ID { get; set; }

        [PrimaryKey]
        public string HouseName { get; set; }

        public string ListJson { get; set; }

        public string Location { get; set; }

        public House() { }

        public House(string houseName, string listJson, string location = "The Moon")
        {
            HouseName = houseName;
            ListJson = listJson;
            Location = location;
        }

        public override string ToString()
        {
            return string.Format("[Person: HouseName={0}, ListJson={1}]", HouseName, ListJson);
        }
    }
}