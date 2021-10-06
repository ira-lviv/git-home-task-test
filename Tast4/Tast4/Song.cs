using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tast4
{
    class Song
    {
        public int Minute { get; set; }
        public int Second { get; set; }

        public Song(string text)
        {
            var texts = text.Split(':');
            Minute = Convert.ToInt32(texts[0]);
            Second = Convert.ToInt32(texts[1]);
        }
    }
}
