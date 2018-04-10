using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using multiPlayer_shooter;


namespace Server
{
    class Server
    {
        static TcpListener host;
        static Dictionary<int, TcpClient> users;
        
        static void Main(string[] args)
        {
            users = new Dictionary<int, TcpClient>();
            host = new TcpListener(IPAddress.Any, 8091);
            host.Start();
            host.BeginAcceptTcpClient(userConnected, host);
            Console.Title = "Server";
            while (true) ;
        }

        static void send (string message, int id = -1)
        {
            byte[] b = Encoding.ASCII.GetBytes(message);
            if (id != -1)
            {
                foreach (var a in users)
                {
                    if (a.Key != id)
                    {
                        a.Value.GetStream().Write(b, 0, b.Length);
                    }
                }
            }
            else if (id == -1)
            {
                users[users.Count].GetStream().Write(b, 0, b.Length);
            }
            
        }

        static void RecieveData(IAsyncResult ar, ref byte[] data)
        {
            NetworkStream n = ar.AsyncState as NetworkStream;
            int size = n.EndRead(ar);
            string s = Encoding.ASCII.GetString(data, 0, size);

            parseData(ref n, s);

            byte[] newData = new byte[1024];
            n.BeginRead(newData, 0, 1024, (ara) => RecieveData(ara, ref newData), n);
        }

        static void parseData(ref NetworkStream n, string msg)
        {
            Console.WriteLine(msg);

            string[] arr = msg.Split(' ');
            Constants.MessageTypes type = Constants.TypeFromStrings[arr[2]];

            if (type == Constants.MessageTypes.getid)
            {
                send("id "+ arr[1] + " giveid " + users.Count.ToString());
            }
            else if (type == Constants.MessageTypes.move)

            { 
                send("id host move " + arr[3] + " " + arr[4], int.Parse(arr[1]));
            }
        }

        static void userConnected(IAsyncResult res)
        {
            TcpListener temp = res.AsyncState as TcpListener;
            if (temp != null)
            {
                TcpClient tmp = temp.EndAcceptTcpClient(res);
                users.Add(users.Count+1, tmp);
                byte[] data = new byte[10000];
                tmp.GetStream().BeginRead(data, 0, 10000,(ar) => RecieveData(ar, ref data), tmp.GetStream());
                temp.BeginAcceptTcpClient(userConnected, res.AsyncState);
            }
        }
    }
}
