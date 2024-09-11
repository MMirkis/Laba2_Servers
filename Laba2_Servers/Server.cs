
//_________________Реализация жадного синглтона с многопоточкой_____________
public class Server
{
    private static readonly Server instance = new Server();

    private readonly HashSet<string> servers ;

    private static readonly object lockObject = new object();

    private Server() {
        servers = new HashSet<string>();
    }
    public static Server Instance
    {
        get { return instance; }
    }
    public bool AddServer(string serverAdress)
    {
        if (string.IsNullOrEmpty(serverAdress)||// в ленивом он в этом месте у меня ругался на тоже самое....
            (!serverAdress.StartsWith("http://") &&
            !serverAdress.StartsWith("https://")))
        {
            Console.WriteLine("Так не пойдет");
            return false;
        }


        lock (lockObject)
        {
            if (!servers.Add(serverAdress))
            {
                Console.WriteLine("Уже есть...");
                return false;
            }
        }
        Console.WriteLine("Сервер добавлен успешно :)");
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
                if (servers.Remove(serverAddress))
                {
                    Console.WriteLine("Сервер удален успешно.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Сервера тут и не было.");
                    return false;
                }
            }
        }
    public List<string> GetHttpServers()
    {
        lock (lockObject)
        {
            return new List<string>(servers.Where(s => s.StartsWith("http://")));
        }
    }
    

    public List<string> GetHttpsServers()
    {
        lock (lockObject)
        {
            return new List<string>(servers.Where(s => !s.StartsWith("http://")));
        }
    }

}


