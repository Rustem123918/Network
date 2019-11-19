using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Switch : Device
    {
        public Dictionary<string, int> serversAnswers;

        public Switch(int _id)
        {
            connections = new List<Device>();
            id = _id;
            serversAnswers = new Dictionary<string, int>();
            serversAnswers.Add("tiktok", 0);
            serversAnswers.Add("snapchat", 0);
            serversAnswers.Add("faceapp", 0);
        }

        public override List<Answer> Action(Query query) //Resend query
        {
            query.devicesId.Add(id);
            var list = new List<Answer>();

            foreach (var device in connections)
            {
                if (query.devicesId.Contains(device.id)) continue; 

                var queryClone = new Query(query);
                var answers = device.Action(queryClone);
                if(answers != null)
                {
                    foreach (var e in answers)
                        list.Add(e);
                }
            }

            if (list.Count > 0)
            {
                foreach(var e in list)
                    serversAnswers[e.serverName]++;
                return list;
            }
            return null;
        }
    }
}
