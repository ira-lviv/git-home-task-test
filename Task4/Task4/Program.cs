using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputString1 = " Davis, Clyne, Fonte, Hooiveld, Shaw, Davis, Schneiderlin, Cork, Lallana, Rodriguez, Lambert";
            string[] players = InputString1.Split(',');
            var result = Enumerable.Range(1, players.Length).Select( p1=>$"{p1}. {players[p1-1].Trim()} ");
            Console.WriteLine("Task 1");
            foreach (var k in result)
            {
                Console.WriteLine(k);
            }
            
            Console.WriteLine();
            Console.WriteLine("Task 2");
            string InputString2 = "Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988";
            string[] Players = InputString2.Split(';');
            List<Players> play = new List<Players>();
            foreach (var item in Players)
            {
                play.Add(new Players(item));
            }
            var orderPlayers = play.OrderBy(playrs => playrs.Age);
            foreach (var item in orderPlayers)
            {
                Console.WriteLine($"{item.Name} ({item.Bday}/{item.Bmonth}/{item.Byear}) {item.Age} years");
            }

            Console.WriteLine();
            Console.WriteLine("Task 3");
            string InputString3 = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27";
            var Songs = InputString3.Split(',').Select(x => TimeSpan.Parse("00:" + x)).Aggregate((current, next) => current + next);
            Console.WriteLine($"Total time of all songs is {Songs}");
            Console.Read();
        }
    }
}
