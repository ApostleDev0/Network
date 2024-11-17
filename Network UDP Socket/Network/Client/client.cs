using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

const string DEFAULT_IP = "127.0.0.1";
const int DEFAULT_PORT = 2100;

IPEndPoint ip = new IPEndPoint(IPAddress.Parse(DEFAULT_IP), DEFAULT_PORT);
Console.OutputEncoding = Encoding.UTF8;
Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

client.Connect(ip);

NetworkStream ns = new NetworkStream(client);
StreamReader reader = new StreamReader(ns);
StreamWriter writer = new StreamWriter(ns) { AutoFlush = true};
while (true)
{
    Console.Write("Lệnh: ");
    string req = Console.ReadLine();
    writer.WriteLine(req); 
    if (req.ToUpper().Equals("QUIT"))
    {
        break;
    }
    string res  = reader.ReadLine(); // đọc phản hòi từ server
    Console.WriteLine("Server phản hồi: " + res);
}
client.Close();
ns.Close();
reader.Close();
writer.Close();