using System.Text;
using System;
namespace DoYourJob
{
    [Serializable]
    public class Chore
    {
        //Members of the Chore class
        //What qualities does a chore have?
        //A name
        public string name { get; set; }
        //A date to be completed by -or- a date to be completed on
        public string date { get; set; }
        //Any details pertaining to the chore
        public string details { get; set; }

        //Constructor for our Chore objects. 
        public Chore(string choreName, string choreDate, string choreDetails)
        {
            name = choreName;
            date = choreDate;
            details = choreDetails;
        }

        //Methods of the Chore class
    }

}