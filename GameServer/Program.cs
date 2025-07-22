namespace GameServer;

public class Program
{
    public static async Task Main(string[] args)
    {
        var server = new GameServerService(7777);

        Console.WriteLine("Запуск игрового сервера...");
        Console.WriteLine("Нажмите 'q' для выхода");

        var serverTask = server.StartAsync();

        // Слушаем команды консоли
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.KeyChar == 'q' || key.KeyChar == 'Q')
            {
                server.Stop();
                break;
            }
        }

        await serverTask;
    }
}