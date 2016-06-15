using System;
using System.IO;
using System.Text;

namespace CommonLib
{
    public class LogHelper
    {
        private static object lockobj = new object();

        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="logText">内容</param>
        /// <param name="logPath">文件路径</param>
        /// <param name="logName">日志文件名称（不带扩展名），为空时默认以日期自动创建</param>
        public static void WriteLog(string logText, string logPath, string logName = "")
        {
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            if (string.IsNullOrEmpty(logName))
            {
                logName = DateTime.Now.ToString("yyyy-MM-dd");
            }

            logPath = string.Format("{0}/{1}.txt", logPath, logName);
            logPath = logPath.Replace("//", "/");

            lock (lockobj)
            {
                using (StreamWriter sWriter = new StreamWriter(logPath, true))
                {
                    sWriter.WriteLine(String.Format("{0}# {1}", DateTime.Now, logText));
                    sWriter.Close();
                }
            }
        }

        /// <summary>
        /// 读取日志文件
        /// </summary>
        /// <param name="logPath">文件路径</param>
        public static string ReadLog(string logPath)
        {
            if (!File.Exists(logPath))
            {
                return "";
            }

            using (FileStream FStream = new FileStream(logPath, FileMode.Open))
            {
                using (StreamReader SReader = new StreamReader(FStream, Encoding.Default))
                {
                    return SReader.ReadToEnd();
                }
            }
        }
    }
}
