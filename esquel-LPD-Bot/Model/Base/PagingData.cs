using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class PagingData<T>
    {
        public int totalPage { get; set; }
        public IEnumerable<T> data { get; set; }

        public PagingData() { }
    }
}