using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class Fabric
    {
        public string fabricID { get; set; }
        public string fabricNo { get; set; }

        public string contentName { get; set; }
        public string comboName { get; set; }

        public string[] longDescriptions { get; set; }
        public string imageURL { get; set; }

        public Fabric()
        {

        }
    }
}