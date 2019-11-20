using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Network
    {
        private Computer[] computers;
        private Switch[] switches;
        private Server[] servers;
        private string[] serversNames = { "tiktok", "snapchat", "faceapp" };
        private const int countComputers = 20;
        private const int countSwitches = 10;
        private const int countServers = 3;
        private int seed = 2;


        public Network()
        {
            computers = new Computer[countComputers];
            switches = new Switch[countSwitches];
            servers = new Server[countServers];

            GenerateRandomId();
            GenerateConnections();

            //Initiate
            #region
            //computers[0].connections.Add(switches[0]);
            //computers[1].connections.Add(switches[1]);
            //computers[2].connections.Add(switches[1]);
            //computers[3].connections.Add(switches[1]);
            //computers[4].connections.Add(switches[2]);
            //computers[5].connections.Add(switches[3]);
            //computers[6].connections.Add(switches[3]);
            //computers[7].connections.Add(switches[3]);
            //computers[8].connections.Add(switches[4]);
            //computers[9].connections.Add(switches[4]);
            //computers[10].connections.Add(switches[5]);
            //computers[11].connections.Add(switches[5]);
            //computers[12].connections.Add(switches[6]);
            //computers[13].connections.Add(switches[7]);
            //computers[14].connections.Add(switches[7]);
            //computers[15].connections.Add(switches[7]);
            //computers[16].connections.Add(switches[8]);
            //computers[17].connections.Add(switches[8]);
            //computers[18].connections.Add(switches[8]);
            //computers[19].connections.Add(switches[9]);


            //switches[0].connections.Add(computers[0]);
            //switches[0].connections.Add(switches[1]);
            //switches[0].connections.Add(servers[0]);
            //switches[0].connections.Add(switches[9]);

            //switches[1].connections.Add(switches[0]);
            //switches[1].connections.Add(computers[1]);
            //switches[1].connections.Add(computers[2]);
            //switches[1].connections.Add(computers[3]);
            
            //switches[2].connections.Add(computers[4]);
            //switches[2].connections.Add(switches[3]);
            //switches[2].connections.Add(servers[1]);
            //switches[2].connections.Add(servers[2]);
            
            //switches[3].connections.Add(switches[2]);
            //switches[3].connections.Add(computers[5]);
            //switches[3].connections.Add(computers[6]);
            //switches[3].connections.Add(computers[7]);

            //switches[4].connections.Add(servers[2]);
            //switches[4].connections.Add(computers[8]);
            //switches[4].connections.Add(computers[9]);
            //switches[4].connections.Add(switches[5]);

            //switches[5].connections.Add(servers[1]);
            //switches[5].connections.Add(computers[10]);
            //switches[5].connections.Add(computers[11]);
            //switches[5].connections.Add(switches[4]);

            //switches[6].connections.Add(switches[7]);
            //switches[6].connections.Add(computers[12]);

            //switches[7].connections.Add(switches[6]);
            //switches[7].connections.Add(computers[13]);
            //switches[7].connections.Add(computers[14]);
            //switches[7].connections.Add(computers[15]);

            //switches[8].connections.Add(switches[9]);
            //switches[8].connections.Add(computers[16]);
            //switches[8].connections.Add(computers[17]);
            //switches[8].connections.Add(computers[18]);

            //switches[9].connections.Add(switches[0]);
            //switches[9].connections.Add(switches[8]);
            //switches[9].connections.Add(computers[19]);
            //switches[9].connections.Add(servers[0]);


            //servers[0].connections.Add(switches[0]);
            //servers[0].connections.Add(switches[9]);

            //servers[1].connections.Add(switches[2]);
            //servers[1].connections.Add(switches[5]);

            //servers[2].connections.Add(switches[2]);
            //servers[2].connections.Add(switches[4]);
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
            Console.WriteLine("Computers id: ");
            foreach (var e in computers)
                Console.Write(e.id + " ");
            Console.WriteLine();

            Console.WriteLine("Switches id: ");
            foreach (var e in switches)
                Console.Write(e.id + " ");
            Console.WriteLine();

            Console.WriteLine("Servers id: ");
            foreach (var e in servers)
                Console.Write(e.id + " ");
            Console.WriteLine();
            
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
        
        private void GenerateRandomId()
        {
            var countDevices = countComputers + countServers + countSwitches;
            int[] arrId = new int[countDevices];
            Random rnd = new Random(seed);
            bool flag;

            //Заполняю массив arrId случайными уникальными числами от 10 до 99
            for (int i = 0; i < countDevices;)
            {
                flag = false;
                var nR = rnd.Next(10, 99);

                for (int j = 0; j < i; j++)
                {
                    if (arrId[j] == nR)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    arrId[i] = nR;
                    i++;
                }
            }

            //Каждому устройству в сети присваиваю Id из массива arrId
            for (int i = 0; i < countComputers; i++)
                computers[i] = new Computer(arrId[i], serversNames);
            for (int i = 0; i < countSwitches; i++)
                switches[i] = new Switch(arrId[i + countComputers], serversNames);
            for (int i = 0; i < countServers; i++)
                servers[i] = new Server(arrId[i + countComputers + countSwitches], serversNames[i]);
        }

        private void GenerateConnections()
        {
            //1)
            //Подключаем сервера к свичам
            ConnectServers();

            //2)
            //Подключаем свичи к свичам
            ConnectSwitches();

            //3)
            //Подключаем компьютеры к свичам
            ConnectComputers();
        }

        private void ConnectComputers()
        {
            for (int i = 0; i < countComputers; i++)
            {
                int _seed = i;
                var rnd = new Random(_seed);
                bool flag = true;
                int index = 0;

                while (flag)
                {
                    index = rnd.Next(0, countSwitches);
                    if (!switches[index].connections.Contains(computers[i]) &&
                        switches[index].connections.Count < 4) flag = false;
                    else rnd = new Random(++_seed);
                }
                computers[i].connections.Add(switches[index]);
                switches[index].connections.Add(computers[i]);
            }
        }

        private void ConnectSwitches()
        {
            for (int i = 0; i < countSwitches; i++)
            {
                bool check = false;
                foreach (var e in switches)
                    if (switches[i].connections.Contains(e)) check = true;
                if (check) continue;

                int _seed = i;
                var rnd = new Random(_seed);
                bool flag = true;
                int index = 0;

                while (flag)
                {
                    index = rnd.Next(0, countSwitches);
                    if (index != i &&
                        !switches[i].connections.Contains(switches[index]) &&
                        switches[i].connections.Count < 4 &&
                        switches[index].connections.Count < 4)
                        flag = false;
                    else rnd = new Random(++_seed);
                }
                switches[i].connections.Add(switches[index]);
                switches[index].connections.Add(switches[i]);
            }
        }

        private void ConnectServers()
        {
            for (int i = 0; i < countServers; i++)
            {
                int _seed = i;
                var rnd = new Random(_seed);
                bool flag = true;
                int first = 0;
                int second = 0;

                while (flag)
                {
                    first = rnd.Next(0, countSwitches);
                    second = rnd.Next(0, countSwitches);
                    if (first != second) flag = false;
                    else rnd = new Random(++_seed);
                }
                servers[i].connections.Add(switches[first]);
                switches[first].connections.Add(servers[i]);

                servers[i].connections.Add(switches[second]);
                switches[second].connections.Add(servers[i]);
            }
        }
    }
}
