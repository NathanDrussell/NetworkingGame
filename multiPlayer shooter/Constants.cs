using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiPlayer_shooter
{
    class Constants
    {
        public static Dictionary<MessageTypes, string> MessageTypStrings = new Dictionary<MessageTypes, string>()
        {
            { MessageTypes.getid, "getid" },
            { MessageTypes.id, "id"},
            { MessageTypes.move, "move"}
        };
    }
}
