using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Exceptions
{
    public class OrderCannotBeCancelledException: Exception
    {
        public OrderCannotBeCancelledException(string message) : base(message)
        {

        }
    }

}
