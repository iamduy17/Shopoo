using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCommon.CustomException
{
    public class CustomException : Exception
    {
        public string Code { get; set; }
        public CustomException() : base()
        {

        }

        public CustomException(string code)
        {
            Code = code;
        }
    }
}
