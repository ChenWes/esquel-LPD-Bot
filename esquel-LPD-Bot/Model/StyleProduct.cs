using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class StyleProduct
    {
        public Product linePlanProducts { get; set; }
        public Style productStyles { get; set; }
        public Fabric[] productFabrics { get; set; }
        public ApparelTrim[] productTrims { get; set; }

        //  Do not remove this, cannot serialize if removed.
        public StyleProduct() { }
    }
}