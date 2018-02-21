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
        const string apiLoginId = "5KP3u95bQpv";
        const string transactionKey = "346HZ32z3fP4hTG2";
        static void Main(string[] args)
        {
            Console.WriteLine(AuthorizeConnector.getToken(
                s => Console.WriteLine(s),
                apiLoginId,
                transactionKey,
                "30"
                ));
            Console.ReadKey();
        }
    }
}
