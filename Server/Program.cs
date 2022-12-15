using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static byte[] ServerPayam = new byte[1024];
        static byte[] resend = new byte[1024];
        static string Data;
        static Socket AcceptedServer;
        static void Main(string[] args)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress[] AllIP = Dns.GetHostAddresses("127.0.0.1");
            IPEndPoint serverEndPoint = new IPEndPoint(AllIP[0], 9999);
            server.Bind(serverEndPoint);
            server.Listen(100);
            while (true)
            {
                server.BeginAccept(AcceptCallback, server);
                void AcceptCallback(IAsyncResult ar)
                {
                    Socket Server = (Socket)ar.AsyncState;
                    Socket AcceptedServer = server.EndAccept(ar);
                    Console.WriteLine("server Accepted Connection");
                    server.BeginAccept(AcceptCallback, server);
                    AcceptedServer.BeginReceive(ServerPayam, 0, ServerPayam.Length, SocketFlags.None, ReceiveCallback, AcceptedServer);
                }

                void ReceiveCallback(IAsyncResult ar)
                {
                    AcceptedServer = (Socket)ar.AsyncState;
                    int ServerRecived = AcceptedServer.EndReceive(ar);
                    Data = Encoding.UTF8.GetString(ServerPayam, 0, ServerRecived);
                    Console.WriteLine("Server Recive \"" + ServerRecived + "\" byte From Client");
                    Console.WriteLine("Server Recive \"" + Data + "\" From Client");
                    Resend();
                    AcceptedServer.BeginReceive(ServerPayam, 0, ServerPayam.Length, SocketFlags.None, ReceiveCallback, AcceptedServer);
                }
                void Resend()
                {
                    resend = Encoding.UTF8.GetBytes(Data);
                    AcceptedServer.Send(resend, SocketFlags.None);
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Server send \"" + Data + "\" to client");
                    Console.WriteLine("-----------------------------");
                }
                Console.ReadKey();
            }
        }
    }
}