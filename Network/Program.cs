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
            //Id of computers: 1 - 20
            //Id of switches: 21 - 30
            //Id of servers: 31 - 33
            var net = new Network();
            net.StartNetwork();
            Console.ReadKey();
        }
    }
}
