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

        public Computer(int _id)
        {
            connections = new List<Device>();
            id = _id;
            connectedServers = new Dictionary<string, bool>();
            connectedServers.Add("tiktok", false);
            connectedServers.Add("snapchat", false);
            connectedServers.Add("faceapp", false);
        }

        public override List<Answer> Action(Query query) //Send query
        {
            query.devicesId.Add(id);
            var list = new List<Answer>();

            foreach (var device in connections)
            {
                if (query.devicesId.Contains(device.id)) continue;

                var queryClone = new Query(query);
                var answers = device.Action(queryClone);
                if (answers != null)
                {
                    foreach (var e in answers)
                        list.Add(e);
                }
            }
            
            if (list.Count > 0)
            {
                foreach (var e in list)
                    connectedServers[e.serverName] = true;
                return list;
            }
            return null;
        }
    }
}
