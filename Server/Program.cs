using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static TcpListener host;
        static Dictionary<int, TcpClient> users;

        static void Main(string[] args)
        {
            users = new Dictionary<int, TcpClient>();
            host = new TcpListener(IPAddress.Any, 8091);
            host.Start();
            host.BeginAcceptTcpClient(userConnected, host);
            while (true) ;
        }

        static void RecieveData(IAsyncResult ar, ref byte[] data)
        {
            NetworkStream n = ar.AsyncState as NetworkStream;
            int size = n.EndRead(ar);
            string s = Encoding.ASCII.GetString(data,0, size);

            Console.WriteLine(size + " " + s);
            byte[] newData = new byte[10000];
            n.BeginRead(newData, 0, 10000, (ara) => RecieveData(ara, ref newData), n);
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
