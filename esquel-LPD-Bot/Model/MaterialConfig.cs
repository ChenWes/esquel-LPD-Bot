using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class MaterialConfig
    {
        public string optionNo { get; set; }
        public int productColorwayID { get; set; }
        public int lineplanProductID { get; set; }

        public string materialConfigID { get; set; }
        public string materialConfigNo { get; set; }

        public string colorway { get; set; }
        public string bodyPattern { get; set; }
        public string pluNumber { get; set; }

        public string primaryFabricID { get; set; }
        public string PrimaryFabricImageUrl { get; set; }
        public string[] LongDescriptions { get; set; }
        public int? rank { get; set; }

        public Auxiliary[] appliedAuxiliaries { get; set; }
        //public ColorwayMarketInfo[] markets { get; set; }
        //public ColorwayBuyPeriodInfo[] buyPeriods { get; set; }

        public MaterialConfig() { }
    }
}