using System.Management;

namespace lab1.Services;

public class NetworkService
{
    public class NetworkAdaptersInfo
    {
        public class NetworkAdapter
        {
            public String Index { get; set; }
            public String Description { get; set; }
            public String SettingID { get; set; }
            public String MACAddress { get; set; }
        }

        public List<NetworkAdapter> Adapters { get; set; }
    }

    public static NetworkAdaptersInfo GetNetworkAdaptersInfo()
    {
        ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

        NetworkAdaptersInfo resultInfo = new NetworkAdaptersInfo();
        resultInfo.Adapters = new List<NetworkAdaptersInfo.NetworkAdapter>();
        
        ManagementObjectCollection queryCollection = searcher.Get();
        foreach (ManagementObject m in queryCollection)
        {
            NetworkAdaptersInfo.NetworkAdapter n = new NetworkAdaptersInfo.NetworkAdapter();
            n.Index = m["Index"].ToString();
            n.Description = m["Description"].ToString();
            n.SettingID = m["SettingID"].ToString();
            n.MACAddress = (String)m["MACAddress"];
            
            resultInfo.Adapters.Add(n) ;
        }

        return resultInfo;
    }
}