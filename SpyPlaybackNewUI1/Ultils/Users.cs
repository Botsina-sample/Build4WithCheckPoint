using System;
using System.IO;

namespace SpyandPlaybackTestTool.Ultils
{
    internal class Users
    {
        /// <summary>
        /// Client username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Get username of this computer
        /// </summary>
        /// <returns></returns>
        public string GetUsername()
        {
            return Username = Environment.UserName;
        }

        /// <summary>
        /// Create a folder to store log files
        /// </summary>
        /// <returns></returns>
        public string CreateScriptFolder()
        {
            try
            {
                string sysdrive = Path.GetPathRoot(Environment.SystemDirectory);
                string scriptFolder = sysdrive + @"Users\" + GetUsername() + @"\AppData\Roaming\Botsina\Scripts\";

                if (!Directory.Exists(scriptFolder))
                {
                    Directory.CreateDirectory(scriptFolder);
                }

                return scriptFolder;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}