using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class Auxiliary
    {
        public const string AuxiliaryTypeTrim = "T";
        public const string AuxiliaryTypeFabric = "F";

        public const string FabricWoven = "W";
        public const string FabricKnit = "K";

        public string auxiliaryID { get; set; }
        public string auxiliaryToUseID { get; set; }
        public string auxiliaryType { get; set; }
        public string auxiliaryFabricType { get; set; }

        public Auxiliary() { }
    }
}