using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

const string DEFAULT_IP = "127.0.0.1"; // Địa chỉ IP của server.
const int DEFAULT_PORT = 2100; // Cổng mà server sẽ lắng nghe.
Console.OutputEncoding = Encoding.UTF8;

UdpClient server = new UdpClient(DEFAULT_PORT); // Tạo một UdpClient để lắng nghe dữ liệu từ client.
IPEndPoint clientEndpoint = new IPEndPoint(IPAddress.Any, 0); // Định nghĩa endpoint mặc định để nhận dữ liệu từ bất kỳ client nào.
Console.WriteLine("Server đang lắng nghe...");

while (true)
{
    byte[] receivedData = server.Receive(ref clientEndpoint); // Nhận dữ liệu từ client và lưu thông tin client vào `clientEndpoint`.
    string req = Encoding.UTF8.GetString(receivedData).ToUpper(); // Giải mã dữ liệu từ client và chuyển thành chữ in hoa.
    Console.WriteLine($"Nhận từ {clientEndpoint}: {req}");

    string res = "";
    if (req == "QUIT")
    {
        Console.WriteLine("Client đã ngắt kết nối.");
        break;
    }
    else if (req == "HI")
    {
        res = "Chào bạn!";
    }
    else if (req == "WHAT TIME IS IT ?")
    {
        res = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
    else
    {
        res = "Lệnh sai! Kiểm tra lại!";
    }
    byte[] responseData = Encoding.UTF8.GetBytes(res); // Mã hóa phản hồi thành dạng byte.
    server.Send(responseData, responseData.Length, clientEndpoint); // Gửi phản hồi đến client thông qua endpoint đã nhận.
}

server.Close(); // Đóng kết nối server.
