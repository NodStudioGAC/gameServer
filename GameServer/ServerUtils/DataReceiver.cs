namespace GameServer.ServerUtils
{
    class DataReceiver
    {
        internal static void Read(Client client, string data)
        {
            switch (data)
            {
                case "keep connection":
                    client.Write("keep connection");
                    break;

                case "disconnection":
                    client.Disconnect();
                    break;

                default:
                    break;
            }
        }
        
    }
}
