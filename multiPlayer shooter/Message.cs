using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiPlayer_shooter
{
    class Message
    {
        private Constants.MessageTypes type;
        private int id = 0;
        private string body;
        public Message(int i, Constants.MessageTypes m, params string[] msg)
        {
            id = i;
            type = m;
            body = "";
            foreach (string s in msg)
            {
                body += " " + s;
            }
        }

        public byte[] getData()
        {
            string sendString = "id " + id + " " + Constants.StringsFromType[type] + body;
            return  Encoding.ASCII.GetBytes(sendString);
        }


    }
}
