using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class Style
    {
        public string styleID { get; set; }
        public string styleNo { get; set; }
        public string styleVersion { get; set; }
        public string styleVersionSerialNo { get; set; }


        public string fds { get; set; }
        public string description { get; set; }
        public string productType { get; set; }
        public string productCatetory { get; set; }
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

        public string imageURL { get; set; }

        public Style()
        {

        }
    }
}