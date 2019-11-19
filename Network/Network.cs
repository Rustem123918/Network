using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Network
    {
        Computer[] computers;
        Switch[] switches;
        Server[] servers;

        public Network()
        {
            computers = new Computer[20];
            for (int i = 0; i < 20; i++)
                computers[i] = new Computer(i+1);
            switches = new Switch[10];
            for (int i = 0; i < 10; i++)
                switches[i] = new Switch(20 + i + 1);
            servers = new Server[3];
                servers[0] = new Server(31, "tiktok");
                servers[1] = new Server(32, "snapchat");
                servers[2] = new Server(33, "faceapp");

            //Initiate
            #region
            computers[0].connections.Add(switches[0]);
            computers[1].connections.Add(switches[1]);
            computers[2].connections.Add(switches[1]);
            computers[3].connections.Add(switches[1]);
            computers[4].connections.Add(switches[2]);
            computers[5].connections.Add(switches[3]);
            computers[6].connections.Add(switches[3]);
            computers[7].connections.Add(switches[3]);
            computers[8].connections.Add(switches[4]);
            computers[9].connections.Add(switches[4]);
            computers[10].connections.Add(switches[5]);
            computers[11].connections.Add(switches[5]);
            computers[12].connections.Add(switches[6]);
            computers[13].connections.Add(switches[7]);
            computers[14].connections.Add(switches[7]);
            computers[15].connections.Add(switches[7]);
            computers[16].connections.Add(switches[8]);
            computers[17].connections.Add(switches[8]);
            computers[18].connections.Add(switches[8]);
            computers[19].connections.Add(switches[9]);


            switches[0].connections.Add(computers[0]);
            switches[0].connections.Add(switches[1]);
            switches[0].connections.Add(servers[0]);
            switches[0].connections.Add(switches[9]);

            switches[1].connections.Add(switches[0]);
            switches[1].connections.Add(computers[1]);
            switches[1].connections.Add(computers[2]);
            switches[1].connections.Add(computers[3]);
            
            switches[2].connections.Add(computers[4]);
            switches[2].connections.Add(switches[3]);
            switches[2].connections.Add(servers[1]);
            switches[2].connections.Add(servers[2]);
            
            switches[3].connections.Add(switches[2]);
            switches[3].connections.Add(computers[5]);
            switches[3].connections.Add(computers[6]);
            switches[3].connections.Add(computers[7]);

            switches[4].connections.Add(servers[2]);
            switches[4].connections.Add(computers[8]);
            switches[4].connections.Add(computers[9]);
            switches[4].connections.Add(switches[5]);

            switches[5].connections.Add(servers[1]);
            switches[5].connections.Add(computers[10]);
            switches[5].connections.Add(computers[11]);
            switches[5].connections.Add(switches[4]);

            switches[6].connections.Add(switches[7]);
            switches[6].connections.Add(computers[12]);

            switches[7].connections.Add(switches[6]);
            switches[7].connections.Add(computers[13]);
            switches[7].connections.Add(computers[14]);
            switches[7].connections.Add(computers[15]);

            switches[8].connections.Add(switches[9]);
            switches[8].connections.Add(computers[16]);
            switches[8].connections.Add(computers[17]);
            switches[8].connections.Add(computers[18]);

            switches[9].connections.Add(switches[0]);
            switches[9].connections.Add(switches[8]);
            switches[9].connections.Add(computers[19]);
            switches[9].connections.Add(servers[0]);


            servers[0].connections.Add(switches[0]);
            servers[0].connections.Add(switches[9]);

            servers[1].connections.Add(switches[2]);
            servers[1].connections.Add(switches[5]);

            servers[2].connections.Add(switches[2]);
            servers[2].connections.Add(switches[4]);
            #endregion
        }

        public void StartNetwork()
        {
            foreach(var comp in computers)
            {
                var query = new Query();
                var answers = comp.Action(query);
            }

            //Display results
            #region
            Console.WriteLine("Id of computers: 1 - 20");
            Console.WriteLine("Id of switches: 21 - 30");
            Console.WriteLine("Id of servers: 31 - 33");
            foreach (var comp in computers)
            {
                Console.Write(comp.id + ": ");
                foreach(var e in comp.connectedServers)
                {
                    Console.WriteLine($"\t{e.Key} - {e.Value}");
                }
                Console.WriteLine("\n");
            }

            foreach(var sw in switches)
            {
                Console.Write(sw.id + ": ");
                foreach(var e in sw.serversAnswers)
                {
                    Console.WriteLine($"\t{e.Key} - {e.Value}");
                }
                Console.WriteLine("\n");
            }
            #endregion
        }
    }
}
