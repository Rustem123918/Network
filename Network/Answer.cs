using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Answer
    {
        public Query query;
        public string serverName;
        public Answer(string _serverName, Query _query)
        {
            serverName = _serverName;
            query = new Query(_query);
        }
    }
}
