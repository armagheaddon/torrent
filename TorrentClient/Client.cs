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

        //private Queue<Dictionary<string, dynamic>> incoming = new Queue<Dictionary<string, dynamic>>();
        //private Thread receiveLoop;

        public Client()
        {
            // Create a TCP/IP  socket.  
            client = new UdpClient(11000);
            try
            {
                //Thread receiveLoop = new Thread(new ThreadStart(ReceiveLoop));
                //receiveLoop.Start();

                Console.WriteLine("Sending file name, 'test.txt'");
                Send("test.txt", serverIP, serverPORT);
                Console.WriteLine("Sent file name to server");

                (string senderIP, int senderPORT, string data) = ReceiveToString();
                Console.WriteLine("Received - " + data);
                CloseSocket();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                CloseSocket();
                Environment.Exit(-1);
            }
        }
        public (string, int, string) ReceiveToString()
        {
            // IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                byte[] receiveBytes = client.Receive(ref RemoteIpEndPoint);
                string received = Encoding.ASCII.GetString(receiveBytes);
                return (RemoteIpEndPoint.Address.ToString(), RemoteIpEndPoint.Port, received);
            }
            catch (Exception e)
            {
                return ("", 0, "");
            }
        }
        public void Send(string str, string ip, int port)
        {
            // Encode the data string into a byte array.  
            byte[] msg = Encoding.ASCII.GetBytes(str);

            // Send the data through the socket.  
            client.Send(msg, msg.Length, ip, port);
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
    }
}
