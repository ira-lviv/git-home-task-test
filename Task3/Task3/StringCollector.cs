using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class StringCollector
    {
        private List<string> _strings = new List<string>();
        public void Add(string text)

        {
            if (!text.Any(c => char.IsDigit(c)))
            {
                _strings.Add(text);
            }
        }


        public void Output()
        {
            Console.WriteLine("Strings collection");
            foreach (var item in _strings)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}