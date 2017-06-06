using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayoutVer2._0.Core
{
    public class Setting
    {
        public string ServerName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ExtentionBus { get; set; }
        public string ExtentionDto { get; set; }
        public string ExtentionDal { get; set; }

        public string NamespaceBus { get; set; }
        public string NamespaceDto { get; set; }
        public string NamespaceDal { get; set; }

        public readonly string nameServer = @".\SQLEXPRESS";
        public readonly string username = "sa";
        public readonly string password = "123456";

        public readonly string extentionBus = "BCL";
        public readonly string extentionDal = "Dao";
        public readonly string extentionDto = "Object";

        public readonly string namespaceBus = "BUS";
        public readonly string namespaceDal = "DAL";
        public readonly string namespaceDto = "DTO";
    }
}
