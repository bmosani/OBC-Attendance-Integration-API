using System;
using System.Configuration;

namespace Attendance_Integration.Models.D365
{

    public class ClientConfiguration
    {
        public static ClientConfiguration Default { get { return OneBox; } }

        public static ClientConfiguration OneBox = new ClientConfiguration
        {
            UriString = ConfigurationManager.AppSettings[nameof(UriString)],
            UserName = ConfigurationManager.AppSettings["AXUserName"],
            Password = ConfigurationManager.AppSettings["AXPassword"],
            ActiveDirectoryResource = ConfigurationManager.AppSettings[nameof(ActiveDirectoryResource)],
            ActiveDirectoryTenant = ConfigurationManager.AppSettings[nameof(ActiveDirectoryTenant)],
            ActiveDirectoryClientAppId = ConfigurationManager.AppSettings[nameof(ActiveDirectoryClientAppId)],
            ActiveDirectoryClientAppSecret = ConfigurationManager.AppSettings[nameof(ActiveDirectoryClientAppSecret)],
            TLSVersion = ""
        };

        public string TLSVersion { get; set; }
        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        public string ActiveDirectoryClientAppSecret { get; set; }
    }

}