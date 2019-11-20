using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Device
    {
        public int id;
        public List<Device> connections;

        public Device(int _id)
        {
            id = _id;
            connections = new List<Device>();
        }

        public virtual List<Answer> Action(Query query)
        {
            return null;
        }

        protected List<Answer> ResendQuery(Query query)
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

            return list.Count > 0 ? list : null;
        }
    }
}
