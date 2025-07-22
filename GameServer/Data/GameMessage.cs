namespace GameServer.Data;

public class GameMessage
{
    public string Type; // "join", "update", "leave", "broadcast"
    public PlayerData Data;
}