using RemoteControl.Services;
using RemoteControl.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebSocketSharp.Server;

namespace RemoteControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var wsServer = new WebSocketServer("ws://192.168.1.29:8080/");
            //string machineName = Environment.MachineName;

            //wsServer.AddWebSocketService<JsWebSocketBehavior>("/" + machineName);
            //wsServer.Start();

            //Console.WriteLine("WebSocket Server started on ws://192.168.1.29:8080/" + machineName);
            //Console.ReadKey(true);

            //wsServer.Stop();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<WebSocketService>();
                });



    }
}
