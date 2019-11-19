using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Query
    {
        public List<int> devicesId;
        
        public Query()
        {
            devicesId = new List<int>();
        }
        public Query(Query query)
        {
            devicesId = new List<int>();
            foreach (var e in query.devicesId)
                devicesId.Add(e);
        }
    }
}
