using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Program
    {
        static void Main(string[] args)
        {
            var net = new Network();
            net.StartNetwork();
            Console.ReadKey();
        }
    }
}
