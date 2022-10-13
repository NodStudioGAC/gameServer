using GameServer.ServerUtils;
using System;

namespace GameServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server started, waiting for some connecttions...\n");
            new TcpServer(Param.PORT, 10000);
        }
    }
}
