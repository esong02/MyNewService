using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class FileHandler
    {
        const string _EXT = ".txt";

        public static string FileName
        {
            get
            {
                return "ServiceLog";
            }
        }

        public static string Directory
        {
            get
            {
                return "C:\\Users\\admin\\Documents\\Visual Studio 2017\\Projects\\MyNewService\\Log";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetFileName()
        {
            string filename = Directory + "\\" + FileName + _EXT;
            int counter = 1;
            try
            {
                while (File.Exists(filename))
                {
                    filename = Directory + "\\" + FileName + "_" + counter + _EXT;
                    counter++;
                }
            }
            catch { }
            return filename;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static async void WriteAsync(string message, bool GetDate = true)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                {
                    return;
                }
                string filename = Directory + "\\" + FileName + _EXT;
                
                using (var file = File.AppendText(filename))
                {
                    if (GetDate)
                    {
                        await file.WriteLineAsync(message + "\t\t" + DateTime.Now.ToString());
                    }
                    else
                    {
                        await file.WriteLineAsync(message);
                    }
                }
            }
            catch { }
        }
    }
}
