using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace GameServer.ServerUtils
{
    class Client
    {
        #region VARIABLES
        BinaryWriter _sWriter;
        BinaryReader _sReader;
        TcpClient _tcpClient;
        Thread _handleThread;
        TcpServer _tcpServer;
        bool _connected;
        int _id;
        #endregion

        #region CONSTRUCTOR
        public Client(TcpServer tcpServer, TcpClient tcpClient, int id)
        {
            _id = id;
            _tcpServer = tcpServer;
            _tcpClient = tcpClient;
            _sWriter = new BinaryWriter(_tcpClient.GetStream());
            _sReader = new BinaryReader(_tcpClient.GetStream());
            _handleThread = new Thread(new ThreadStart(_handleClient));
            _handleThread.Start();
            _connected = true;
        }
        #endregion

        #region HANDLE_CLIENT
        void _handleClient()
        {
            Console.WriteLine($"Client[{_id}] is connected");
            while (_connected)
            {
                try { DataReceiver.Read(this, _sReader.ReadString()); }
                catch
                {
                    Disconnect();
                    break;
                }
            }
        }
        #endregion

        #region CLIENT_UTILS
        internal void Disconnect()
        {
            Console.WriteLine($"Client[{_id}] is disconnected");
            if (_connected)
            {
                _connected = false;
                _tcpServer.RemoveClient(_id);
                try { _handleThread.Abort(); } catch { }
            }
        }
        #endregion

        #region WRITE_FUNCTIONS
        internal void Write(bool data)
        {
            try
            {
                _sWriter.Write(data);
                _sWriter.Flush();
            }
            catch { }
        }
        internal void Write(char data)
        {
            try
            {
                _sWriter.Write(data);
                _sWriter.Flush();
            }
            catch { }
        }
        internal void Write(string data)
        {
            try
            {
                _sWriter.Write(data);
                _sWriter.Flush();
            }
            catch { }
        }
        internal void Write(int data)
        {
            try
            {
                _sWriter.Write(data);
                _sWriter.Flush();
            }
            catch { }
        }
        internal void Write(float data)
        {
            try
            {
                _sWriter.Write(data);
                _sWriter.Flush();
            }
            catch { }
        }
        #endregion

    }
}
