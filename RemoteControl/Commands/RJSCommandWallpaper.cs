using RemoteControl.WebSocket;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Commands
{
    public class RJSCommandWallpaper : IReactCommand
    {
        public string Command => "wallpaper";

        public void Execute(JsWebSocketBehavior webSocket, string[] @params)
        {
            try
            {
                string path = DownloadImage(@params[0]);
                SetRandomLastWriteTime(path, new DateTime(2021, 1, 1), new DateTime(2021, 12, 31));

                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                if (registryKey != null)
                {
                    registryKey.SetValue("WallpaperStyle", 10.ToString());
                    registryKey.SetValue("TileWallpaper", 0.ToString());

                    SystemParametersInfo(0x0014, 0, path, 0x0001 | 0x0002);

                    registryKey.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private string DownloadImage(string imageUrl)
        {
            string localImagePath = $@"C:\Users\{Environment.UserName}\Downloads\IMG_{Guid.NewGuid()}.png";

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(imageUrl, localImagePath);
            }

            return localImagePath;
        }

        private void SetRandomLastWriteTime(string filePath, DateTime startDate, DateTime endDate)
        {
            Random random = new Random();
            TimeSpan timeSpan = endDate - startDate;
            int randomDays = random.Next(0, (int)timeSpan.TotalDays);
            DateTime randomDate = startDate.AddDays(randomDays);

            File.SetLastWriteTime(filePath, randomDate);
        }

        [DllImport("user32.dll")]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    }
}
