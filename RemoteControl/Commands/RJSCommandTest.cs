using RemoteControl.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Commands
{
    public class RJSCommandTest : IReactCommand
    {
        public string Command => "test";

        public void Execute(JsWebSocketBehavior webSocket, string[] @params)
        {
            webSocket.SendMessage("Test Successful");
        }
    }
}
