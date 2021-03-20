using System;

namespace TorrentClient
{
    public static (int, ) ParseMsg()
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Console.WriteLine("Sending file name, 'test.txt'");
            client.SendToServer("test.txt");
            Console.WriteLine("Sent file name to server");

            (string senderIP, int senderPORT, string data) = client.ReceiveToString();
            Console.WriteLine("Received - " + data);
            if(data == "file name not found")
                Console.WriteLine("no such file available");
            else
            {
                
            }
        }
    }
}
