using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core.Exception
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message, int? code = null) : base(message, code: code)
        {
        }
    }
}
