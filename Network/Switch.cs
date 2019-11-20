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

        public Switch(int _id, string[] serversNames) : base(_id)
        {
            serversAnswers = new Dictionary<string, int>();
            foreach (var name in serversNames)
                serversAnswers.Add(name, 0);
        }

        public override List<Answer> Action(Query query) //Resend query
        {
            var list = ResendQuery(query);

            if (list != null && list.Count > 0)
            {
                foreach (var e in list)
                    serversAnswers[e.serverName]++;
                return list;
            }
            return null;
        }
    }
}
