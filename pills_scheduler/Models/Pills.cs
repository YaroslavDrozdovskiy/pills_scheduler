using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace pills_scheduler.Models
{
    public class Pills
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string PillCount { get; set; }
        public int FreqInDay { get; set; }
        public string FreqByDays { get; set; }
        public string CompWithFood { get; set; }
        public string CourseDuration { get; set; }
        public DateTime InitialDate { get; set; }

    }


}

 