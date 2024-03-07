using RemoteControl.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Commands
{
    public class RJSCommandCreate : IReactCommand
    {
        public string Command => "create";

        public void Execute(JsWebSocketBehavior webSocket, string[] @params)
        {
            try
            {
                using (var fileStream = File.Create(@params[0]))
                {
                    Console.WriteLine("Success");
                    webSocket.SendMessage("Created File: " + @params[0]);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                webSocket.SendMessage(e.Message);
            }
        }
    }
}
