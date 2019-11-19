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
        public virtual List<Answer> Action(Query query)
        {
            return null;
        }
    }
}
