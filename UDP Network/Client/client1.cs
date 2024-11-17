using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

const string DEFAULT_IP = "127.0.0.1";
const int DEFAULT_PORT = 2100;

UdpClient client = new UdpClient();
IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(DEFAULT_IP), DEFAULT_PORT);
Console.OutputEncoding = Encoding.UTF8;

while (true)
{
    Console.Write("Client 1 - Nhập lệnh: ");
    string message = Console.ReadLine();
    byte[] sendBytes = Encoding.UTF8.GetBytes(message);

    // Gửi dữ liệu đến server
    client.Send(sendBytes, sendBytes.Length, serverEndPoint);

    if (message.ToUpper() == "QUIT")
    {
        break;
    }

    // Nhận phản hồi từ server
    byte[] receivedBytes = client.Receive(ref serverEndPoint);
    string response = Encoding.UTF8.GetString(receivedBytes);
    Console.WriteLine("Client 1 - Server trả lời: " + response);
}

client.Close();
