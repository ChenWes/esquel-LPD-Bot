using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class BaseResponse<T>
    {
        public string resultType { get; private set; }
        public T results { get; set; }
        public string resultMsg { get; set; }

        public object exceptionDetail { get; private set; }

        public BaseResponse() { }
    }
}