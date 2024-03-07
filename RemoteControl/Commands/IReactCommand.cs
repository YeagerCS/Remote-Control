using RemoteControl.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Commands
{
    public interface IReactCommand
    {
        string Command { get; }
        void Execute(JsWebSocketBehavior webSocket, string[] @params);
    }
}
