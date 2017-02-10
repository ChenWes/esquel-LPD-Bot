using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class Product
    {
        public int linePlanProductID { get; set; }
        public int linePlanID { get; set; }
        public string productID { get; set; }
        public string productNo { get; set; }
        public string productStyleID { get; set; }
        public string productVersion { get; set; }
        public string productVersionSerialNo { get; set; }

        public string fds { get; set; }
        public string description { get; set; }
        public string productType { get; set; }
        public string productCategory { get; set; }
        public string subCategory { get; set; }
        public string customerStyleNo { get; set; }

        public string fitName { get; set; }
        public string stylingName { get; set; }
        public string collarName { get; set; }
        public string placketName { get; set; }
        public string cuffName { get; set; }
        public string pocketName { get; set; }
        public string collectionName { get; set; }
        public string sleeveName { get; set; }

        public string gender { get; set; }

        public string[] longDescriptions { get; set; }
        public string remark { get; set; }

        public MaterialConfig[] productMaterialConfigs { get; set; }

        public Product() { }
    }
}