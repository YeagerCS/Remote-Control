using RemoteControl.Commands;
using RemoteControl.Invoker;
using RemoteControl.Parser;
using RemoteControl.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.App
{
    public class ApplicationInstance
    {
        private List<IReactCommand?> commandInstances;
        private JsWebSocketBehavior webSocket;

        public void SetWebSocketBehavior(JsWebSocketBehavior webSocket)
        {
            this.webSocket = webSocket;
        }

        public ApplicationInstance()
        {
            IEnumerable<Type> commandTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(IReactCommand).IsAssignableFrom(type) && !type.IsInterface);

            commandInstances = commandTypes.Select(type => (IReactCommand?)Activator.CreateInstance(type)).ToList();
        }

        public void Run(string input)
        {
            CommandParser.ParseCommand(input, out string command, out string[] @params);
            CommandInvoker.Execute(webSocket, commandInstances, command, @params);
        }
    }
}
