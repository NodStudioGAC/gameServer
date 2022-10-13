using System;
using System.Net;
using System.Net.Sockets;

namespace GameServer.ServerUtils
{
    class TcpServer
    {
        #region STATIC_VARIABLES
        #endregion

        #region VARIABLES
        TcpListener _tcpListener;
        Client[] _clients;
        int _clientsLimit;
        bool _isRunning;
        #endregion

        #region CONSTRUCTOR
        public TcpServer(int port, int clientsLimit)
        {
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _clientsLimit = clientsLimit;
            _clients = new Client[_clientsLimit];
            for (int i = 0; i < _clientsLimit; i++)
                _clients[i] = null;

            _isRunning = true;
            _tcpListener.Start();
            _handleServer();
        }
        #endregion

        #region HANDLE_SERVER
        void _handleServer()
        {
            int place = GetAvailablePlace();

            while (_isRunning)
            {
                if (place == -1)
                    Console.WriteLine("Server full");

                else 
                    _clients[place] = new Client(this, _tcpListener.AcceptTcpClient(), place);

                place = GetAvailablePlace();
            }

        }
        #endregion

        #region UTILS
        int GetAvailablePlace()
        {
            int place = 0;
            foreach (Client client in _clients)
            {
                if (client == null)
                    return place;

                place++;
            }

            return -1;
        }

        internal void RemoveClient(int id) { _clients[id] = null; }
        #endregion
    }
}
