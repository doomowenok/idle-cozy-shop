using System.Net.Sockets;

namespace GameServer.Data;

public class ConnectedClient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TcpClient TcpClient { get; set; }
    public NetworkStream Stream { get; set; }
    public DateTime LastActivity { get; set; }
}