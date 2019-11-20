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

        public Switch(int _id) : base(_id)
        {
            serversAnswers = new Dictionary<string, int>();
            serversAnswers.Add("tiktok", 0);
            serversAnswers.Add("snapchat", 0);
            serversAnswers.Add("faceapp", 0);
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
