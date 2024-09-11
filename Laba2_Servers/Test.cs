using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2_Servers
{
     class Test
    {
        static void Main(string[] args)
        {//_______________________ ленивый синглтон_______________
            ServerLazy servers = new ServerLazy();
            Console.WriteLine(servers.AddServer("http://example.com"));
            Console.WriteLine(servers.AddServer("http://myau.com"));
            Console.WriteLine(servers.AddServer("https://secure.com"));
            Console.WriteLine(servers.AddServer("http://example.com"));
            Console.WriteLine(servers.AddServer("ftp://invalid.com"));
            Console.WriteLine(servers.AddServer(null));

            var httpServers = servers.GetHttpServers();
            var httpsServers = servers.GetHttpsServers();

            Console.WriteLine("http сервера:");
            foreach (var server in httpServers)
            {
                Console.WriteLine(server.ToString());
            }

            Console.WriteLine("https сервера:");
            foreach (var server in httpsServers)
            {
                Console.WriteLine(server.ToString());
            }

            servers.RemoveServer("http://example.com");

            httpServers = servers.GetHttpServers();

            Console.WriteLine("http сервера:");
            foreach (var server in httpServers)
            {
                Console.WriteLine(server.ToString());
            }
            //______________________жадный синглтон_________________
            Server serverManager = Server.Instance;


            serverManager.AddServer("http://example.com");
            serverManager.AddServer("https://example.com");
            serverManager.AddServer("ftp://example.com");

            var httpServers2 = serverManager.GetHttpServers();
            var httpsServers2 = serverManager.GetHttpsServers();

            Console.WriteLine("http сервера:");
            foreach (var server in httpServers2)
            {
                Console.WriteLine(server.ToString());
            }

            Console.WriteLine("https сервера:");
            foreach (var server in httpsServers2)
            {
                Console.WriteLine(server.ToString());
            }

            serverManager.RemoveServer("http://example.com");
            serverManager.RemoveServer("http://nonexistent.com");


            Console.WriteLine("Оставшиеся HTTP сервера:");
            foreach (var s in serverManager.GetHttpServers())
            {
                Console.WriteLine(s);
            }

        }
    }
}
