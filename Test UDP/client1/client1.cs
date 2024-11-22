using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

const string DEFAULT_IP = "127.0.0.1"; // Địa chỉ IP của server.
const int DEFAULT_PORT = 2100; // Cổng của server.
Console.OutputEncoding = Encoding.UTF8;

UdpClient client = new UdpClient(); // Tạo một UdpClient để gửi và nhận dữ liệu.
IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Parse(DEFAULT_IP), DEFAULT_PORT); // Định nghĩa endpoint của server.
Console.WriteLine("Client1 đã kết nối tới server.");

while (true)
{
    Console.Write("Lệnh gửi: ");
    string req = Console.ReadLine();
    byte[] requestData = Encoding.UTF8.GetBytes(req); // Mã hóa lệnh gửi thành dạng byte.
    client.Send(requestData, requestData.Length, serverEndpoint); // Gửi dữ liệu tới server.

    if (req.ToUpper() == "QUIT")
    {
        break; // Nếu lệnh là QUIT, thoát khỏi vòng lặp.
    }

    IPEndPoint serverResponseEndpoint = new IPEndPoint(IPAddress.Any, 0); // Định nghĩa endpoint để nhận phản hồi từ server.
    byte[] responseData = client.Receive(ref serverResponseEndpoint); // Nhận dữ liệu phản hồi từ server
    string res = Encoding.UTF8.GetString(responseData); // Giải mã dữ liệu phản hồi.
    Console.WriteLine("Phản hồi từ server: " + res);
}

client.Close(); // Đóng client.
