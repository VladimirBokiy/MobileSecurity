using System.Data;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Controllers;
using WUApiLib;

namespace lab1.Services;

public class UpdatesService
{
    public class UpdatesInfo
    {
        public class Update
        {
            public String HotFixId { get; set; }
            public String InstalledOn { get; set; }
            public String Description { get; set; }
            public String CSName { get; set; }
            public String InstalledBy { get; set; }
        }

        public List<Update> Updates;
    }

    public static UpdatesInfo GetUpdatesInfo()
    {
        UpdatesInfo resultInfo = new UpdatesInfo();
        resultInfo.Updates = new List<UpdatesInfo.Update>();
        
        var s = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_QuickFixEngineering");

        foreach (var queryObj in s.Get())
        {
            UpdatesInfo.Update u = new UpdatesInfo.Update();

            u.HotFixId = queryObj["HotFixId"].ToString();
            u.InstalledOn = queryObj["InstalledOn"].ToString();
            u.Description = queryObj["Description"]?.ToString() ?? "null";
            u.CSName = queryObj["CSName"].ToString();
            u.InstalledBy = queryObj["InstalledBy"].ToString();

            resultInfo.Updates.Add(u);
        }

        return resultInfo;
    }
}