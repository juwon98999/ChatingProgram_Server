using System.Net.Sockets;
using System.Text;
using System;


public class Chat_Client
{
    public static void Main()
    {
        try
        {
            string server = "127.0.0.1";

            Int32 port = 13000;
            TcpClient client = new TcpClient(server, port);
            Console.WriteLine("127.0.0.1:9000에 접속시도중...");
            Console.WriteLine("'주'님께 연결되었습니다.");
            String message = Console.ReadLine();


            Byte[] data = System.Text.Encoding.Default.GetBytes(message);

            NetworkStream stream = client.GetStream();

            stream.Write(data, 0, data.Length);

            Console.WriteLine($"[주] {message}");

            data = new Byte[256];

 
            String responseData = String.Empty;

            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine($"Received: {responseData}");

            // Close everything.
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }

        Console.WriteLine("\n Press Enter to continue...");
        Console.Read();
    }
}
