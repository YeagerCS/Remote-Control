using RemoteControl.App;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace RemoteControl.WebSocket
{
    public class JsWebSocketBehavior : WebSocketBehavior
    {
        private ApplicationInstance app;

        public JsWebSocketBehavior()
        {
            app = new ApplicationInstance();
            app.SetWebSocketBehavior(this);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            string data = e.Data;
            app.Run(data);
        }

        protected override void OnOpen()
        {
            base.OnOpen();

            this.Send(Directory.GetCurrentDirectory());
        }

        public void SendMessage(string data)
        {
            this.Send(data);
        }
    }
}
