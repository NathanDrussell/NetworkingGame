using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiPlayer_shooter
{
    class Constants
    {
        public enum MessageTypes
        {
            getid = 0,
            move,
            id
        }
        public static Dictionary<MessageTypes, string> StringsFromType = new Dictionary<MessageTypes, string>()
        {
            { MessageTypes.getid, "getid" },
            { MessageTypes.id, "giveid"},
            { MessageTypes.move, "move"}
        };
        public static Dictionary<string, MessageTypes> TypeFromStrings = new Dictionary<string, MessageTypes>()
        {
            { "getid", MessageTypes.getid },
            { "giveid", MessageTypes.id},
            { "move", MessageTypes.move}
        };
    }
}
