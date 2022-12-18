using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    class IssueTracker
    {
        public static void Main()
        {
            for (int i = 0; i < 3; i++)
            {
                Field x = new Field();
                Console.WriteLine(x.ToString());
                _ = Console.ReadLine();
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1 - show ideas\n2 - show issues\n3 - show both\n4 - add new");
        }
    }   
}
