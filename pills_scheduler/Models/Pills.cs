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
        public string Count { get; set; }
        public string FreqInDay { get; set; }
        public string FreqByDays { get; set; }
        public string CompWithFood { get; set; }
        public string CourseDuration { get; set; }
        public DateTime InitialDate { get; set; } = DateTime.Today;
        public TimeSpan TimeFirst { get; set; } = new TimeSpan(12,0,0);
        public TimeSpan? TimeSecond { get; set; } = null;
        public TimeSpan? TimeThird { get; set; } = null;


    }



}

 