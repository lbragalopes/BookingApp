using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core.Exception
{
    public class ConflictException : CustomException
    {
        public ConflictException(string message, int? code = null) : base(message, code: code)
        {
        }
    }
}

