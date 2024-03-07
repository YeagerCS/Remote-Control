using RemoteControl.Parser;
using RemoteControl.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Invoker
{
    public class TerminalCommandInvoker
    {
        public static void Execute(JsWebSocketBehavior webSocket, string command, string[] @params)
        {
            string fullCommand = CommandParser.CombineCommandAndParams(command, @params);

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + fullCommand;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Command Error: \n" + error);
                    webSocket.SendMessage(error);
                }
                else
                {
                    Console.WriteLine("Command Output: \n" + output);
                    webSocket.SendMessage(output);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Exception occurred: " + ex.Message);
                webSocket.SendMessage(ex.Message);
            }
        }
    }
}
