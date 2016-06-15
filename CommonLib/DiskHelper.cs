using System;

namespace CommonLib
{
    /// <summary>
    /// 说明：磁盘操作帮助类
    /// 作者：BRUCE
    /// 日期：2016-06-15
    /// </summary>
    public class DiskHelper
    {
        /// <summary>
        /// 返回数据大小单位
        /// </summary>
        public enum SizeUnit
        {
            B = 0,
            KB = 1,
            MB = 2,
            GB = 3,
            TB = 4
        }

        /// <summary> 
        /// 获取指定驱动器的空间总大小
        /// </summary> 
        /// <param name="str_HardDiskName">只需输入代表驱动器的字母即可 （大写）</param> 
        /// <returns> </returns> 
        public static double GetHardDiskSpace(string str_HardDiskName, SizeUnit sizeUnit = SizeUnit.GB)
        {
            double totalSize = new double();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    // TotalSize 单位为B
                    totalSize = Math.Round((double)drive.TotalSize / Math.Pow(1024, (int)sizeUnit), 2);
                }
            }
            return totalSize;
        }

        /// <summary> 
        /// 获取指定驱动器的剩余空间总大小
        /// </summary> 
        /// <param name="str_HardDiskName">只需输入代表驱动器的字母即可 </param> 
        /// <returns> </returns> 
        public static double GetHardDiskFreeSpace(string str_HardDiskName, SizeUnit sizeUnit = SizeUnit.GB)
        {
            double freeSpace = new double();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    // TotalFreeSpace 单位为B
                    freeSpace = Math.Round((double)drive.TotalFreeSpace / Math.Pow(1024, (int)sizeUnit), 2);
                }
            }
            return freeSpace;
        }
    }
}
