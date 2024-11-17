using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

const string DEFAULT_IP = "127.0.0.1";
const int DEFAULT_PORT = 2100;

UdpClient server = new UdpClient(DEFAULT_PORT);
Console.OutputEncoding = Encoding.UTF8;
IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(DEFAULT_IP), DEFAULT_PORT);

Console.WriteLine("Server UDP đang chờ client gửi dữ liệu...");

while (true)
{
    byte[] receivedBytes = server.Receive(ref endPoint); // Nhận dữ liệu từ client
    string receivedMessage = Encoding.UTF8.GetString(receivedBytes);

    Console.WriteLine("Nhận từ client: " + receivedMessage);

    if (receivedMessage.ToUpper() == "QUIT")
    {
        break;
    }

    string response = "Server đáp lại: " + receivedMessage;
    byte[] responseBytes = Encoding.UTF8.GetBytes(response);

    // Gửi phản hồi về client
    server.Send(responseBytes, responseBytes.Length, endPoint);
}

server.Close();
