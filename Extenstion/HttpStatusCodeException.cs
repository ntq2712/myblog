using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Extenstion
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { set; get; }

        public HttpStatusCodeException(int statusCode, string message): base(message)
        {
            StatusCode = statusCode;
        }
    }
}