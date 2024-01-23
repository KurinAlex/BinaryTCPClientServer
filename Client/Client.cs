using System.Net.Sockets;

using Utility;

try
{
    Console.Write("Enter your name: ");
    string? name = Console.ReadLine();

    using var client = new TcpClient(Data.Host, Data.Port);
    using NetworkStream stream = client.GetStream();
    using var writer = new BinaryWriter(stream, Data.Encoding);

    SendMessage(writer, name);

    for (int i = 0; i < Data.MessagesCount; i++)
    {
        string message = $"message {i + 1}";
        SendMessage(writer, message);
        Console.WriteLine($"Message sent: {message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static void SendMessage(BinaryWriter writer, string? message)
{
    if (message == null)
    {
        return;
    }

    byte[] messageBytes = Data.Encoding.GetBytes(message);
    writer.Write(messageBytes.Length);
    writer.Write(messageBytes);
    writer.Flush();
}
