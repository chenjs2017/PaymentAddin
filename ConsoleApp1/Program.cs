using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plug_Ins;

namespace ConsoleApp1
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine(
                AuthorizeConnector.getToken(
                     AuthorizeNetOption.DefaultOption(),
                     "20",
                     s => Console.WriteLine(s)
                     )
                );
            Console.ReadKey();
        }
    }
}
