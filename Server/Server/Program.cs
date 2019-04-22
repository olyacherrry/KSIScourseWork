using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        const int port = 8888;
        const string adrress = "127.0.0.1";
        //const string adrress = "192.168.100.8";

        static void Main(string[] args)
        {
            TCPServer server = new TCPServer();
            server.Create(adrress, port);
            server.InfoServer();
            server.StartServer();
        }
    }
}