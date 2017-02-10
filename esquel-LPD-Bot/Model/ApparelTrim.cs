using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class ApparelTrim
    {
        public string apparelTrimID { get; set; }
        public string apparelTrimNo { get; set; }
        public string[] longDescriptions { get; set; }
        public string imageURL { get; set; }

        public ApparelTrim()
        {

        }
    }
}