using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Text;

namespace TorrentClient
{
    class Client
    {
        private string serverIP = "10.0.0.18";
        private int serverPORT = 50000;

        private UdpClient client;
        private byte[] bytes = new byte[1024];  // Data buffer for incoming data. 

        //private Queue<(string, int, string)> incoming = new Queue<(string, int, string)>();
        //private Thread receiveLoop;

        public Client()
        {
            // Create a TCP/IP  socket.  
            client = new UdpClient(11000);
            try
            {
                //Thread receiveLoop = new Thread(new ThreadStart(ReceiveLoop));
                //receiveLoop.Start();

                // TODO: Check if server online
                Console.WriteLine("checking in with the server");
                SendToServer("online");
                (string ip, int port, string data) = ReceiveToString();
                if(data != "ack")
                    throw new Exception();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                CloseSocket();
                Environment.Exit(-1);
            }
        }
        
        public void Send(string str, string ip, int port)
        {
            // Encode the data string into a byte array.  
            byte[] msg = Encoding.ASCII.GetBytes(str);

            // Send the data through the socket.  
            client.Send(msg, msg.Length, ip, port);
        }
        public void SendToServer(string str)
        {
            // Encode the data string into a byte array.  
            byte[] msg = Encoding.ASCII.GetBytes(str);

            // Send the data through the socket.  
            client.Send(msg, msg.Length, serverIP, serverPORT);
        }
        public void CloseSocket()
        {
            try
            {
                // Close the socket.  
                client.Close();
            }
            catch (Exception e)
            {
                return;
            }
        }
        public (string, int, string) ReceiveToString()
        {
            // IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                byte[] receiveBytes = client.Receive(ref RemoteIpEndPoint);
                string receivedMsg = Encoding.ASCII.GetString(receiveBytes);
                return (RemoteIpEndPoint.Address.ToString(), RemoteIpEndPoint.Port, receivedMsg);
            }
            catch (Exception e)
            {
                return ("", 0, "");
            }
        }
        
        //public void ReceiveLoop()
        //{
        //    string data;
        //    string ip;
        //    int port;
        //    // TODO: UPGRADE - check if socket closed
        //    try
        //    {
        //        while (true)
        //        {
        //            incoming.Enqueue(ReceiveToString());
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

        //public string GetIncoming()
        //{
        //    while (incoming.Count() == 0)
        //    {
        //        DoNothing();
        //    }
        //    var msg = incoming.Dequeue();
        //    Console.WriteLine("incoming: " + msg);
        //    return msg;
        //}
        //public void DoNothing()
        //{
        //    return;
        //}
    }
}
