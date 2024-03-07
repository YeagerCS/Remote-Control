using RemoteControl.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Commands
{
    public class RJSCommandLock : IReactCommand
    {
        public string Command => "lock";

        public void Execute(JsWebSocketBehavior webSocket, string[] @params)
        {
            LockWorkStation();
        }

        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();
    }
}
