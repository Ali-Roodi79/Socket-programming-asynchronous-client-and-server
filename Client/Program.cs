using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace client
{
    class Program
    {
        static byte[] clientPayam2 = new byte[1024];
        static void Main(string[] args)
        {
            while (true)
            {
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress[] AllIP = Dns.GetHostAddresses("127.0.0.1");
                IPEndPoint serverEndPoint = new IPEndPoint(AllIP[0], 9999);
                client.BeginConnect(serverEndPoint, connectcall, client);
                Console.WriteLine("Client connecting to Server");
                Console.WriteLine("Type your Message");
                string Message = Console.ReadLine();
                byte[] ClientPayam = Encoding.UTF8.GetBytes(Message);
                client.BeginSend(ClientPayam, 0, ClientPayam.Length, SocketFlags.None, sendcall, client);
                void connectcall(IAsyncResult ar)
                {
                    Socket Client = (Socket)ar.AsyncState;
                    Client.EndConnect(ar);
                }
                client.BeginReceive(clientPayam2, 0, clientPayam2.Length, SocketFlags.None, receievecall, client);
                void receievecall(IAsyncResult ar)
                {

                    Socket Client = (Socket)ar.AsyncState;
                    int clientRecived = Client.EndReceive(ar);
                    string Data = Encoding.UTF8.GetString(clientPayam2, 0, clientRecived);
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Client Recive \"" + clientRecived + "\" byte From Server");
                    Console.WriteLine("Client Recive \"" + Data + "\" From Server");
                    Console.WriteLine("-----------------------------");
                }
                void sendcall(IAsyncResult ar)
                {
                    Socket Client = (Socket)ar.AsyncState;
                    int ClientSent = Client.EndSend(ar);
                    Console.WriteLine("Client Send \"" + Message + "\" To Server");
                }
                Console.ReadKey();
            }
        }
    }
}
