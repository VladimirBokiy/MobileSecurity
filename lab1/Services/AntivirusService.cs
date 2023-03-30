using System.Management;

namespace lab1.Services;

public class AntivirusService
{
    public class AntivirusInfo
    {
        public string AntivirusInstalled { get; set; }

        public class SingelAntivirusinfo
        {
            public String DisplayName { get; set; }
            public String ProductState { get; set; }
            public String InstanceGuid { get; set; }

            public String TimeStamp { get; set; }
        }

        public List<SingelAntivirusinfo> AntivirusesList { get; set; }
    }

    public static AntivirusInfo GetAntivirusInfo()
    {
        ManagementObjectCollection instances = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct").Get();

        AntivirusInfo resultInfo = new AntivirusInfo();
        resultInfo.AntivirusesList = new List<AntivirusInfo.SingelAntivirusinfo>();

        resultInfo.AntivirusInstalled = (instances.Count > 0).ToString();

        foreach (var i in instances)
        {
            AntivirusInfo.SingelAntivirusinfo s = new AntivirusInfo.SingelAntivirusinfo();

            s.DisplayName = i["displayName"].ToString() ?? "null";
            s.ProductState = i["productState"].ToString() ?? "null";
            s.InstanceGuid = i["instanceGuid"].ToString() ?? "null";
            s.TimeStamp = i["timestamp"].ToString() ?? "null";
    
            resultInfo.AntivirusesList.Add(s);
        }
        
        
        
        return resultInfo;
    }
}