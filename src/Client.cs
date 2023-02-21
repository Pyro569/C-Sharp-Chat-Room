using System.Net;
using System.Net.Sockets;
using System.Text;

IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("");
IPAddress ipAddress = ipHostInfo.AddressList[0];
IPEndPoint ipEndPoint = new(ipAddress, 11_000);

using Socket client = new(
    ipEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);

Console.WriteLine("Please enter your username you would like to use: ");
String user = Console.ReadLine();
Console.WriteLine("Welcome to the chat room " + user + "!");

await client.ConnectAsync(ipEndPoint);
while (true)
{
    // Send message.
    var message = Console.ReadLine();
    var messageBytes = Encoding.UTF8.GetBytes(user + ": " +message);
    _ = await client.SendAsync(messageBytes, SocketFlags.None);
}