using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using GameServer.Data;

namespace GameServer;

public class GameServerService(int port = 7777)
{
    private readonly Lock _clientsLock = new();

    private readonly Dictionary<int, ConnectedClient> _clients = new(8);
    
    private TcpListener? _listener;
    private bool _isRunning;

    public async Task StartAsync()
    {
        _listener = new TcpListener(IPAddress.Any, port);
        
        _listener.Start();
        
        _isRunning = true;

        Console.WriteLine($"Сервер запущен на порту {port}");
        
        _ = Task.Run(CleanupInactiveClients);

        while (_isRunning)
        {
            try
            {
                TcpClient tcpClient = await _listener.AcceptTcpClientAsync();
                Console.WriteLine($"Новое подключение: {tcpClient.Client.RemoteEndPoint}");
                
                _ = Task.Run(() => HandleClientAsync(tcpClient));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при принятии подключения: {ex.Message}");
            }
        }
    }

    private async Task HandleClientAsync(TcpClient tcpClient)
    {
        NetworkStream stream = tcpClient.GetStream();
        var buffer = new byte[4096];
        int? clientId = null;

        try
        {
            while (tcpClient.Connected && _isRunning)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;

                string jsonMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                GameMessage? message = JsonSerializer.Deserialize<GameMessage>(jsonMessage);

                if (message == null) continue;
                
                await ProcessMessage(message, tcpClient, stream);

                if (!clientId.HasValue && message.Type == "join")
                {
                    clientId = message.Data.PlayerId;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке клиента: {ex.Message}");
        }
        finally
        {
            if (clientId != null)
            {
                RemoveClient(clientId.Value);
                await BroadcastPlayerLeft(clientId.Value);
            }

            tcpClient.Close();
        }
    }

    private async Task ProcessMessage(GameMessage message, TcpClient tcpClient, NetworkStream stream)
    {
        switch (message.Type)
        {
            case "join":
                await HandlePlayerJoin(message.Data, tcpClient, stream);
                break;
            case "update":
                await HandlePlayerUpdate(message.Data);
                break;
            case "leave":
                await HandlePlayerLeave(message.Data.PlayerId);
                break;
        }
    }

    private async Task HandlePlayerJoin(PlayerData playerData, TcpClient tcpClient, NetworkStream stream)
    {
        var client = new ConnectedClient
        {
            Id = playerData.PlayerId,
            Name = playerData.PlayerName,
            TcpClient = tcpClient,
            Stream = stream,
            LastActivity = DateTime.Now
        };

        lock (_clientsLock)
        {
            _clients[playerData.PlayerId] = client;
        }

        Console.WriteLine($"Игрок {playerData.PlayerName} присоединился к игре");
        
        await SendExistingPlayersToNewPlayer(client);
        
        await BroadcastToAllExcept(new GameMessage
        {
            Type = "player_joined",
            Data = playerData
        }, playerData.PlayerId);
    }

    private async Task HandlePlayerUpdate(PlayerData playerData)
    {
        lock (_clientsLock)
        {
            if (_clients.TryGetValue(playerData.PlayerId, out var client))
            {
                client.LastActivity = DateTime.Now;
            }
        }
        
        await BroadcastToAllExcept(new GameMessage
        {
            Type = "player_update",
            Data = playerData
        }, playerData.PlayerId);
    }

    private async Task HandlePlayerLeave(int playerId)
    {
        RemoveClient(playerId);
        await BroadcastPlayerLeft(playerId);
    }

    private Task SendExistingPlayersToNewPlayer(ConnectedClient newClient)
    {
        lock (_clientsLock)
        {
            foreach (var existingClient in _clients.Values)
            {
                if (existingClient.Id != newClient.Id)
                {
                    var message = new GameMessage
                    {
                        Type = "existing_player",
                        Data = new PlayerData
                        {
                            PlayerId = existingClient.Id,
                            PlayerName = existingClient.Name
                        }
                    };
                    _ = SendMessageToClient(newClient, message);
                }
            }
        }
        
        return Task.CompletedTask;
    }

    private async Task BroadcastToAllExcept(GameMessage message, int excludePlayerId)
    {
        List<ConnectedClient> clientsToSendTo;
        lock (_clientsLock)
        {
            clientsToSendTo = new List<ConnectedClient>(_clients.Values);
        }

        foreach (ConnectedClient client in clientsToSendTo)
        {
            if (client.Id != excludePlayerId)
            {
                await SendMessageToClient(client, message);
            }
        }
    }

    private async Task BroadcastPlayerLeft(int playerId)
    {
        var message = new GameMessage
        {
            Type = "player_left",
            Data = new PlayerData { PlayerId = playerId }
        };
        await BroadcastToAllExcept(message, playerId);
        Console.WriteLine($"Игрок {playerId} покинул игру");
    }

    private async Task SendMessageToClient(ConnectedClient client, GameMessage message)
    {
        try
        {
            string jsonMessage = JsonSerializer.Serialize(message);
            byte[] data = Encoding.UTF8.GetBytes(jsonMessage);
            await client.Stream.WriteAsync(data, 0, data.Length);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при отправке сообщения клиенту {client.Id}: {ex.Message}");
            RemoveClient(client.Id);
        }
    }

    private void RemoveClient(int playerId)
    {
        lock (_clientsLock)
        {
            if (_clients.Remove(playerId, out ConnectedClient? client))
            {
                try
                {
                    client.TcpClient.Close();
                }
                catch
                {
                    // ignored
                }
            }
        }
    }

    private async Task CleanupInactiveClients()
    {
        while (_isRunning)
        {
            await Task.Delay(30000);

            List<int> inactiveClients = new();
            lock (_clientsLock)
            {
                foreach (var client in _clients.Values)
                {
                    if (DateTime.Now - client.LastActivity > TimeSpan.FromMinutes(2))
                    {
                        inactiveClients.Add(client.Id);
                    }
                }
            }

            foreach (var clientId in inactiveClients)
            {
                RemoveClient(clientId);
                await BroadcastPlayerLeft(clientId);
            }
        }
    }

    public void Stop()
    {
        _isRunning = false;
        _listener?.Stop();
    }
}