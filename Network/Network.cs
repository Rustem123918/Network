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

        private int maxConnectionsInSwitch = 4;
        private int maxConnectionsInServer = 2;

        private int seed = 2;
        private Random rnd;


        public Network()
        {
            computers = new Computer[countComputers];
            switches = new Switch[countSwitches];
            servers = new Server[serversNames.Length];

            rnd = new Random(seed);
            GenerateRandomId(rnd);
            GenerateConnections(rnd);

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
        
        private void GenerateRandomId(Random rnd)
        {
            var countDevices = countComputers + serversNames.Length + countSwitches;
            int[] arrId = GenerateRandomArray(rnd, 10, 99, countDevices);

            //Каждому устройству в сети присваиваю Id из массива arrId
            for (int i = 0; i < countComputers; i++)
                computers[i] = new Computer(arrId[i], serversNames);
            for (int i = 0; i < countSwitches; i++)
                switches[i] = new Switch(arrId[i + countComputers], serversNames);
            for (int i = 0; i < serversNames.Length; i++)
                servers[i] = new Server(arrId[i + countComputers + countSwitches], serversNames[i]);
        }

        private void GenerateConnections(Random rnd)
        {
            //1)
            //Подключаем сервера к свичам
            var array1 = GenerateRandomArray(rnd, 0, countSwitches, countSwitches);
            ConnectServers(array1);

            //2)
            //Подключаем свичи к свичам
            var array2 = GenerateRandomArray(rnd, 0, countSwitches, countSwitches);
            ConnectSwitches(array2);

            //3)
            //Подключаем компьютеры к свичам
            var array3 = GenerateRandomArray(rnd, 0, countSwitches, countSwitches);
            ConnectComputers(array3);
        }

        private void ConnectComputers(int[] arraySwitches)
        {
            foreach(var computer in computers)
            {
                bool allRight = false;

                foreach(var index in arraySwitches)
                {
                    if(switches[index].connections.Count < maxConnectionsInSwitch)
                    {
                        ConnectTwoDevices(computer, switches[index]);
                        allRight = true;
                        break;
                    }
                }

                if (!allRight) throw new Exception("We need more switches or more ports in every switch");
            }
        }

        private void ConnectSwitches(int[] arraySwitches)
        {
            bool allRight = false;

            //Соединяю свичи по парам
            for (int i = 0; i < countSwitches - 1; i++)
                ConnectTwoDevices(switches[arraySwitches[i]], switches[arraySwitches[++i]]);

            //Если количество свичей четное, то сработает if и все будет окей,
            //если нечентое, то сработает else и последний свич присоединится к свичу, удовлетворяющему условиям
            if (switches[arraySwitches[countSwitches - 1]].connections.Contains(switches[arraySwitches[countSwitches - 2]]))
            {
                allRight = true;
            }
            else
            {
                foreach (var index in arraySwitches)
                {
                    if (index != arraySwitches[countSwitches - 1] && 
                        switches[index].connections.Count < maxConnectionsInSwitch)
                    {
                        ConnectTwoDevices(switches[index], switches[arraySwitches[countSwitches - 1]]);
                        allRight = true;
                        break;
                    }
                }
            }

            if(!allRight) throw new Exception("We need more switches or more ports in every switch");
        }

        private void ConnectServers(int[] arraySwitches)
        {
            int index = 0;
            foreach (var server in servers)
            {
                for (int i = 0; i < maxConnectionsInServer; i++)
                {
                    if (switches[arraySwitches[index]].connections.Count == maxConnectionsInSwitch - 1)
                        throw new Exception("We need more switches or more ports in every switch");
                    ConnectTwoDevices(server, switches[arraySwitches[index]]);
                    index++;
                    if (index == countSwitches) index = 0;
                }
            }

            //for (int i = 0; i < serversNames.Length; i++)
            //{
            //    bool flag = true;
            //    int first = 0;
            //    int second = 0;
            //    int counter = 0;

            //    while (flag)
            //    {
            //        counter++;
            //        first = rnd.Next(0, countSwitches);
            //        second = rnd.Next(0, countSwitches);
            //        if (first != second && 
            //            switches[first].connections.Count<4 &&
            //            switches[second].connections.Count<4) flag = false;
            //        if (counter > 1000000) throw new Exception("We need more switches or more ports in every switch");
            //    }
            //    ConnectTwoDevices(servers[i], switches[first]);
            //    ConnectTwoDevices(servers[i], switches[second]);
            //}
        }

        private void ConnectTwoDevices(Device dev1, Device dev2)
        {
            dev1.connections.Add(dev2);
            dev2.connections.Add(dev1);
        }

        private int[] GenerateRandomArray(Random rnd, int minValue, int maxValue, int arrayLength)
        {
            int[] array = new int[arrayLength];
            bool flag;

            //Заполняю массив array случайными уникальными числами от minValue до maxValue
            for (int i = 0; i < arrayLength;)
            {
                flag = false;
                var nR = rnd.Next(minValue, maxValue);

                for (int j = 0; j < i; j++)
                {
                    if (array[j] == nR)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    array[i] = nR;
                    i++;
                }
            }

            return array;
        }
    }
}
