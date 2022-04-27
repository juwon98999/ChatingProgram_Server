using System.Net;
using System.Net.Sockets;
using System.Text;
using System;


class Chat_Server
{
    public static void Main()
    {
        TcpListener server = null;
        try
        {

            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(localAddr, port);

            Console.WriteLine("Waiting for a connection... ");

            server.Start();
            Console.WriteLine("'수' 님이 127.0.0.1에서 접속하셨습니다.");
            String message = Console.ReadLine();
            Console.WriteLine($"[수] {message}");


            Byte[] bytes = new Byte[256];
            String data = null;

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                Console.ReadLine();

                data = null;

                NetworkStream stream = client.GetStream();

                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine($"Received: {data}");

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine($"Sent: {data}");
                }

                client.Close();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            server.Stop();
        }

        Console.WriteLine("\nHit enter to continue...");
        Console.Read();
    }
}