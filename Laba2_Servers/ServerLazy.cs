//_________________Реализация ленивого синглтона с многопоточкой_____________
public class ServerLazy
{
    private static readonly Lazy<ServerLazy> instance = new Lazy<ServerLazy>(() => new ServerLazy());
    private readonly List<string> servers;
    private readonly object lockObject = new object();

    public ServerLazy()
    {
        servers = new List<string>();
    }
    public static ServerLazy Instance => instance.Value;
    public bool AddServer(string serverAddress)
    {
        if (string.IsNullOrEmpty(serverAddress))
        {
            Console.WriteLine("Не то");
            return false;
        }

        bool isValidUrl = serverAddress.StartsWith("http://") || serverAddress.StartsWith("https://");

        if (!isValidUrl)
        {
            Console.WriteLine("Не то");
            return false;
        }
        lock (lockObject)
        {
            if (servers.Contains(serverAddress))
            {
                Console.WriteLine("Уже есть");
                return false;
            }
        }
        servers.Add(serverAddress);
        Console.WriteLine("Получилось");
        return true;

    }
    public bool RemoveServer(string serverAddress)
    {
        if (string.IsNullOrEmpty(serverAddress))
        {
            Console.WriteLine("Нельзя удалить null, чел....");
            return false;
        }

        lock (lockObject)
        {
            bool wasRemoved = servers.Remove(serverAddress);
            if (wasRemoved)
            {
                Console.WriteLine("Сервера больше нет.");
            }
            else
            {
                Console.WriteLine("Сервера тут и не было.");
            }
            return wasRemoved;
        }
    }
    public List<string> GetHttpServers()
    {
        lock (lockObject)
        {
            return servers.Where(s => s.StartsWith("http://")).ToList();
        }
    }

    public List<string> GetHttpsServers()
    {
        lock (lockObject)
        {
            return servers.Where(s => s.StartsWith("https://")).ToList();
        }
    }
}
