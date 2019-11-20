using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Server : Device
    {
        public string name;

        public Server(int _id, string _name) : base(_id)
        {
            name = _name;
        }

        public override List<Answer> Action(Query query) //Send answer
        {
            query.devicesId.Add(id);

            var answer = new Answer(name, query);
            var list = new List<Answer>();
            list.Add(answer);
            
            return list;
        }
    }
}
