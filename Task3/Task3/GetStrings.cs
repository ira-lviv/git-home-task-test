using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public delegate void Input(string text);


    class GetStrings
    {
        // public  Input input;
        //public event Input input;
        public Action<string> input;
        public void Run()
        {
            var text = "";
            if (input != null)
            {
                do
                {
                    text = Console.ReadLine();
                    input(text);

                } while (text != "0");
            }
        }
    }
}