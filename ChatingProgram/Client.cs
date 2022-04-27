using System.Net.Sockets;
using System.Text;
using System;
using System.Collections.Generic;
using System.Threading;


public class Chat_Client
{
    public static void Main()
    {
        Byte[] data = new Byte[256];
        bool Client_Connect = false;
        bool Trigger = false;
        LinkedList<String> Client_list = new LinkedList<string>();

        try
        {
            string server = "127.0.0.1";
            Int32 port = 9000;

            while (Client_Connect == false)
            {
                String InputTCP = Console.ReadLine();
                //if (InputTCP == $"/c {server}:{port}")
                Console.Clear();
                if (InputTCP == $"/c")
                {
                    Client_list.AddLast("127.0.0.1:9000에 접속시도중...");
                    Client_Connect = true;
                }
                else
                {
                    Console.WriteLine("서버를 다시 입력해주세요.");
                }
            }

          
            TcpClient client = new TcpClient(server, port);
            NetworkStream stream = client.GetStream();
            Client_list.AddLast("'주'님께 연결되었습니다.");

            foreach (var chat in Client_list)
            {
                Console.WriteLine(chat);
            }


            while (true)
            {

                if (Client_Connect == true)
                {
                    Thread cl_write = new Thread(() =>
                    Client_Write(Client_list, data, stream, client, Trigger));
                    cl_write.Start();

                    Thread cl_read = new Thread(() =>
                    Client_Read(Client_list, data, stream, Trigger));
                    cl_read.Start();
                }

            }

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

    public static void Client_Write(LinkedList<string> Cl_list,Byte[] data,
        NetworkStream stream, TcpClient client, bool Trigger)
    {

        if (Console.ReadKey().Key == ConsoleKey.D1)
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("메세지를 입력해주세요.");

            String message = Console.ReadLine();

            if (message == "/q")
            {
                client.Close();
                stream.Close();
                Environment.Exit(0);
            }

            data = new Byte[256];
            data = System.Text.Encoding.Default.GetBytes(message);
            stream.Write(data, 0, data.Length);
            if (Cl_list.Count < 10)
            {
                Cl_list.AddLast($"[수]: {message}");
                Console.Clear();
                foreach (var chat in Cl_list)
                {
                    Console.WriteLine(chat);
                }
                Trigger = true;
            }

        }
       
    }

    public static void Client_Read(LinkedList<string> Cl_list, Byte[] data,
        NetworkStream stream, bool Trigger)
    {
        data = new Byte[256];
        String responseData = String.Empty;
        Int32 bytes = stream.Read(data, 0, data.Length);
        responseData = System.Text.Encoding.Default.GetString(data, 0, bytes);
        if (Cl_list.Count < 10)
        {
            Cl_list.AddLast($"[주]: {responseData}");
            Console.Clear();
            foreach (var chat in Cl_list)
            {
                Console.WriteLine(chat);
            }
            Trigger = false;
        }

    }
}
