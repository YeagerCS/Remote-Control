using RemoteControl.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Commands
{
    public class RJSCommandWrite : IReactCommand
    {
        public string Command => "write";

        public void Execute(JsWebSocketBehavior webSocket, string[] @params)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(@params[0]))
                {
                    writer.Write(@params[1]);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
                webSocket.SendMessage("Invalid Parameter count");
            }
        }
    }
}
