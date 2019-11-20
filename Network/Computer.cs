using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Computer : Device
    {
        public Dictionary<string, bool> connectedServers;

        public Computer(int _id, string[] serversNames) : base(_id)
        {
            connectedServers = new Dictionary<string, bool>();
            foreach (var name in serversNames)
                connectedServers.Add(name, false);
        }

        public override List<Answer> Action(Query query) //Send query
        {
            var list = ResendQuery(query);

            if (list != null && list.Count > 0)
            {
                foreach (var e in list)
                    connectedServers[e.serverName] = true;
                return list;
            }
            return null;
        }
    }
}
