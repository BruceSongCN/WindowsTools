using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskMonitor
{
    /// <summary>
    /// 说明：日志帮助类
    /// 作者：BRUCE
    /// 日期：2016-06-15
    /// </summary>
    class Program
    {
        static string strLogPath;
        static string strLogName;

        static void Main(string[] args)
        {
            Console.WriteLine("磁盘空间检测程序启动...");

            string strDiskSetting = System.Configuration.ConfigurationManager.AppSettings["DiskSetting"];
            string strEmailSetting = System.Configuration.ConfigurationManager.AppSettings["EmailSetting"];
            string strServerName = System.Configuration.ConfigurationManager.AppSettings["ServerName"];
            strLogPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            strLogName = "log";

            try
            {
                foreach (string s in strDiskSetting.Split(','))
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        string strSetting = s.Replace("{", "").Replace("}", "");
                        // 盘符名称
                        string strDiskName = strSetting.Split(':')[0].ToUpper();
                        // 最小报警空间（GB）
                        double minSpace = Convert.ToDouble(strSetting.Split(':')[1]);
                        // 获取磁盘剩余空间（GB）
                        double freeSpace = CommonLib.DiskHelper.GetHardDiskFreeSpace(strDiskName);
                        // 打印记录信息
                        ConsoleAndWriteLog(string.Format("成功检测到{0}盘的剩余空间为{1}GB", strDiskName, freeSpace));
                        // 检测报警条件
                        if (freeSpace < minSpace)
                        {
                            string strSmtpServer = strEmailSetting.Split(',')[0].Split(':')[1];
                            string strUserName = strEmailSetting.Split(',')[1].Split(':')[1];
                            string strPassword = strEmailSetting.Split(',')[2].Split(':')[1];
                            string strMailTo = strEmailSetting.Split(',')[3].Split(':')[1];
                            string strSubject = string.Format("服务器({0})磁盘空间报警", strServerName);
                            string strBody = string.Format("您好，监测到目标服务器{0}盘剩余空间为{1}GB，已低于设定的报警阈值{2}GB，请及时处理！", strDiskName, freeSpace, minSpace);
                            CommonLib.SmtpHelper smtp = new CommonLib.SmtpHelper(strSmtpServer, strUserName, strPassword);

                            try
                            {
                                // 发送报警邮件
                                if (smtp.Send(strMailTo, strSubject, strBody))
                                {
                                    ConsoleAndWriteLog(string.Format("{0}盘剩余空间低于报警阈值{1}GB，已成功给{2}发送报警邮件！", strDiskName, minSpace, strMailTo));
                                }
                                else
                                {
                                    ConsoleAndWriteLog(string.Format("{0}盘剩余空间低于报警阈值{1}GB，给{2}发送报警邮件失败，请查证！", strDiskName, minSpace, strMailTo));
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("邮件发送出现错误，原因：" + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 错误
                ConsoleAndWriteLog(ex.Message);
            }

            Console.WriteLine("磁盘空间检测程序运行完成...");
        }

        static void ConsoleAndWriteLog(string strInfo)
        {
            Console.WriteLine(strInfo);
            // 写入错误日志
            CommonLib.LogHelper.WriteLog(strInfo, strLogPath, strLogName);
        }
    }
}
