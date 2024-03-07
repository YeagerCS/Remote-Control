using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RemoteControl.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using System.Diagnostics;

namespace RemoteControl.Services
{
    public class WebSocketService : BackgroundService
    {
        private WebSocketServer wsServer;
        private readonly string HOST = "192.168.1.29";


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string RemoveApiUrl = $"http://{HOST}:5000/remove";
            string ApiUrl = $"http://{HOST}:5000/ip";

            SendIp(ApiUrl);

            var wsUrl = $"ws://{GetLocalIpAddress()}:8080";
            wsServer = new WebSocketServer(wsUrl);
            wsServer.AddWebSocketService<JsWebSocketBehavior>($"/");
            wsServer.Start();

            Console.WriteLine($"WebSocket Server started on {wsUrl}");


            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.Q)
                    {
                        await SendIdentifier(RemoveApiUrl);
                        wsServer.Stop();

                        Process.GetCurrentProcess().Kill();
                    }

                }
            }


            wsServer?.Stop();
        }


        private string GetLocalIpAddress()
        {
            var hostName = Dns.GetHostName();
            var localIpAddress = Dns.GetHostAddresses(hostName)
                .FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork)
                ?.ToString() ?? "Unknown";

            return localIpAddress;
        }

        private async Task SendIdentifier(string removeApiUrl)
        {
            string id = Environment.MachineName;

            try
            {
                using (var client = new HttpClient())
                {
                    await client.PostAsync(removeApiUrl, new StringContent(id));
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }
        }

        private void SendIp(string ApiUrl)
        {
            Task.Run(async () =>
            {
                using (var client = new HttpClient())
                {

                    var data = new
                    {
                        ip = GetLocalIpAddress(),
                        id = Environment.MachineName
                    };

                    var json = JsonConvert.SerializeObject(data);
                    await client.PostAsync(ApiUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                }
            });
        }
    }
}
