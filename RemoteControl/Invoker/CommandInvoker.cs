using RemoteControl.Commands;
using RemoteControl.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Invoker
{
    public class CommandInvoker
    {
        public static bool Execute(JsWebSocketBehavior webSocket, List<IReactCommand> commandInstances, string command, string[] @params)
        {
            IReactCommand? rjsCommand = commandInstances.FirstOrDefault(cmd => cmd.Command.Equals(command, StringComparison.OrdinalIgnoreCase));

            if (rjsCommand != null)
            {
                rjsCommand.Execute(webSocket, @params);
                return true;
            }
            else
            {
                TerminalCommandInvoker.Execute(webSocket, command, @params);
            }

            return false;
        }
    }
}
