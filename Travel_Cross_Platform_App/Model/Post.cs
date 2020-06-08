using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel_Cross_Platform_App.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(500)]
        public string Experience { get; set; }

        public string VenueName {get; set;}
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }

    }
}
