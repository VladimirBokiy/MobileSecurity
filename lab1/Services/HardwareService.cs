using System.Management;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace lab1.Services;

public class HardwareService
{
    public class HardwareInfo
    {
        public String CpuName { get; set; }
        public String NumberOfCores { get; set; }
        public String NumberOfEnabledCores { get; set; }
        public String CpuUsage { get; set; }
        public List<Drive> Drives { get; set; }
        public class Drive
        {
            public String DriveName { get; set; }
            public String DriveType { get; set; }
            public String TotalFreeSpace { get; set; }
            public String TotalSize { get; set; }
        
        }
        public List<RamStick> RamSticks { get; set;}
        public class RamStick
        {
            public String TotalRamMemory { get; set; }
            public String DataWidth { get; set; }
            public String Speed { get; set; }
        }
    }
    
    public static HardwareInfo GetHardwareInfo()
    {
        HardwareInfo resultInfo = new HardwareInfo();
        resultInfo.Drives = new List<HardwareInfo.Drive>();
        resultInfo.RamSticks = new List<HardwareInfo.RamStick>();
        
        ManagementObjectCollection managementobject = new ManagementClass("Win32_Processor").GetInstances();

        foreach (var obj in managementobject)
        {
            resultInfo.CpuName = obj.Properties["Name"].Value.ToString() ?? String.Empty;
            resultInfo.NumberOfCores = obj.Properties["NumberOfCores"].Value.ToString() ?? String.Empty;
            resultInfo.NumberOfEnabledCores = obj.Properties["NumberOfEnabledCore"].Value.ToString() ?? String.Empty;
            resultInfo.CpuUsage = obj.Properties["LoadPercentage"]?.Value?.ToString() ?? "null";
            break;
        }

        DriveInfo[] driveInfos = DriveInfo.GetDrives();

        foreach (var drive in driveInfos)
        {
            HardwareInfo.Drive d = new HardwareInfo.Drive();
            d.DriveName = drive.Name;
            d.DriveType = drive.DriveType.ToString();
            if (drive.IsReady)
            {
                d.TotalFreeSpace = Convert.ToString(Convert.ToInt64(drive.TotalFreeSpace)/1048576);
                d.TotalSize = Convert.ToString(Convert.ToInt64(drive.TotalSize)/1048576);
            }
            
            resultInfo.Drives.Add(d);
        }

        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
        
        foreach (ManagementObject obj in searcher.Get())
        {
            HardwareInfo.RamStick ramStick = new HardwareInfo.RamStick();
            ramStick.TotalRamMemory = Convert.ToString(Convert.ToInt64(obj.Properties["Capacity"].Value)/1048576);
            ramStick.Speed = obj.Properties["Speed"].Value.ToString() ?? String.Empty;
            ramStick.DataWidth = obj.Properties["DataWidth"].Value.ToString() ?? String.Empty;
            resultInfo.RamSticks.Add(ramStick);
        }

        return resultInfo;
    }
}