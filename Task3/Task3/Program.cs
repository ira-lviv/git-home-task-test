using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введiть рядки. Для завершення введення необхiдно ввести '0'");
            GetStrings getStrings = new GetStrings();

            AlphaNumericCollector alphaNumericCollection = new AlphaNumericCollector();
            StringCollector stringCollection = new StringCollector();

            getStrings.input += alphaNumericCollection.Add;
            getStrings.input += stringCollection.Add;
            getStrings.Run();

            Console.WriteLine();
            Console.WriteLine();
            alphaNumericCollection.Output();
            stringCollection.Output();

            Console.Read();
        }
    }
}
