using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

const string DEFAULT_IP = "127.0.0.1";
const int DEFAULT_PORT = 2100;
const int BACKLOG = 5;

Console.OutputEncoding = Encoding.UTF8;
IPEndPoint ip = new IPEndPoint(IPAddress.Parse(DEFAULT_IP), DEFAULT_PORT);
Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

server.Bind(ip);
server.Listen(BACKLOG);
Console.WriteLine("Đang chờ Client kết nối!...");

Socket client = server.Accept();
NetworkStream ns = new NetworkStream(client);
StreamReader reader = new StreamReader(ns);
StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

string res = "";
while (true)
{
    string req = reader.ReadLine(); // đọc yêu cầu phía client
    req = req.ToUpper();
    if (req.Equals("QUIT"))
    {
        break;
    }
    if (req.Equals("HI"))
    {
        res = "Chào bạn! ";
    }
    else if (req.Equals("WHAT TIME IS IT ?"))
    {
        res = DateTime.Now.ToString("dd/MM/yyy HH:mm:ss");
    }
    else
    {
        res = "Lệnh sai! Kiểm tra lại!";
    }
    writer.WriteLine(res); // gửi phản hồi về res
}
server.Close();
ns.Close();
reader.Close();
writer.Close();