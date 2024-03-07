using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Parser
{
    public class CommandParser
    {
        public static void ParseCommand(string commandString, out string command, out string[] @params)
        {
            List<string> parameters = new List<string>();

            string[] parts = commandString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            command = parts.Length > 0 ? parts[0] : "";

            bool isInsideQuotes = false;
            StringBuilder currentParam = new StringBuilder();
            foreach (string param in parts.Skip(1))
            {
                if (param.StartsWith("\""))
                {
                    isInsideQuotes = true;
                    currentParam.Append(param.Substring(1));
                }
                else if (param.EndsWith("\""))
                {
                    isInsideQuotes = false;
                    currentParam.Append(" " + param.Trim('"'));
                    parameters.Add(currentParam.ToString());
                    currentParam.Clear();
                }
                else
                {
                    if (isInsideQuotes)
                    {
                        currentParam.Append(" " + param);
                    }
                    else
                    {
                        parameters.Add(param.Trim('"'));
                    }
                }
            }

            if (isInsideQuotes || (currentParam.Length > 0))
            {
                parameters.Add(currentParam.ToString().Trim('"'));
            }

            @params = parameters.ToArray();
        }


        public static string CombineCommandAndParams(string command, string[] @params)
        {
            return command + " " + string.Join(" ", @params);
        }
    }
}
