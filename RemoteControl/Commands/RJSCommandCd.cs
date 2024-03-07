using RemoteControl.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Commands
{
    public class RJSCommandCd : IReactCommand
    {
        public string Command => "cd";

        public void Execute(JsWebSocketBehavior webSocket, string[] @params)
        {
            try
            {
                string param = @params[0];
                string currentDirectory = Directory.GetCurrentDirectory();

                if (param.Equals("..", StringComparison.OrdinalIgnoreCase))
                {
                    string? parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
                    if (parentDirectory != null)
                    {
                        Directory.SetCurrentDirectory(parentDirectory);
                        webSocket.SendMessage(parentDirectory);
                    }
                }
                else
                {
                    string newDirectory = Path.Combine(currentDirectory, param);
                    if (Directory.Exists(newDirectory))
                    {
                        Directory.SetCurrentDirectory(newDirectory);
                        webSocket.SendMessage(newDirectory);
                    }
                    else
                    {
                        webSocket.SendMessage($"Directory '{param}' not found.");
                    }
                }

            }
            catch (Exception ex)
            {
                webSocket.SendMessage("Exception occurred: " + ex.Message);
            }
        }
    }
}
