using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core.Exception
{
    public class AppException : CustomException
    {
        public AppException(string message, int? code = null) : base(message, code: code)
        {
        }

        public AppException() : base()
        {
        }

        public AppException(string message, HttpStatusCode statusCode, int? code = null) : base(message, statusCode, code)
        {
        }

        public AppException(string message, System.Exception innerException, int? code = null) : base(message, innerException, code: code)
        {
        }
    }
}