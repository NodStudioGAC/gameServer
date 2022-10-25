using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.ServerUtils
{
    class Login
    {
        internal static void ReadLogin(Client client)
        {
            string login = client.sReader.ReadString();
            Console.WriteLine(login);
            client.Write("loginResponse");
        }
    }
}
