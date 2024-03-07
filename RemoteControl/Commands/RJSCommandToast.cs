using RemoteControl.WebSocket;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace RemoteControl.Commands
{

    //Doesn't work
    public class RJSCommandToast : IReactCommand
    {
        public string Command => "toast";

        public void Execute(JsWebSocketBehavior webSocket, string[] @params)
        {
            ShowToastNotification(@params[0]);
        }

        private void ShowToastNotification(string message)
        {
            new ToastContentBuilder()
                .AddText(message);
        }
    }
}
