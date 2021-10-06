using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tast4
{
    class Players
    {
        public string Name { get; set; }
        public int Bday { get; set; }
        public int Bmonth { get; set; }
        public int Byear { get; set; }
        public int Age { get; set; }

        public Players(string text)
        {
            string[] texts = text.Split(',');
            Name = texts[0].Trim();
            string[] date = texts[1].Split('/');
            Bday = Convert.ToInt32(date[0]);
            Bmonth = Convert.ToInt32(date[1]);
            Byear = Convert.ToInt32(date[2]);
            DateTime today = DateTime.Today;
            DateTime birth = new DateTime(Byear, Bmonth, Bday);
            TimeSpan interval = today - birth;
            Age = interval.Days/365;
        }
    }
}
