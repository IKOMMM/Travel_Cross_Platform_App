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
    }
}
