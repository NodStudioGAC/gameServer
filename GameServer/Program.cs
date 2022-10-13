using GameServer.ServerUtils;
using System;

namespace GameServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server started, waiting for some connections...\n");
            new TcpServer(Param.PORT, 1000);
        }
    }
}
