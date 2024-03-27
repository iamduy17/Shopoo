using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCommon.Response
{
    public class ResponseType
    {
        public static string NotFound = "not.found";
        public static string BadRequest = "bad.request";
        public static string Error = "error";
        public static string Success = "success";
    }

    public class ResponseModel<T>
    {
        public string ReturnCode { get; set; }
        public T Data { get; set; }

        public static ResponseModel<T> Success (T data = default)
        {
            return new ResponseModel<T>()
            {
                ReturnCode = ResponseType.Success,
                Data = data
            };
        }

        public static ResponseModel<T> Error()
        {
            return new ResponseModel<T>()
            {
                ReturnCode = ResponseType.Error
            };
        }

        public static ResponseModel<T> NotFound()
        {
            return new ResponseModel<T>()
            {
                ReturnCode = ResponseType.NotFound
            };
        }

        public static ResponseModel<T> BadRequest()
        {
            return new ResponseModel<T>()
            {
                ReturnCode = ResponseType.BadRequest
            };
        }
    }
}
