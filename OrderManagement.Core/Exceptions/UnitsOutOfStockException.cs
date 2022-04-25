using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Exceptions
{
    public class UnitsOutOfStockException : Exception
    {
        public UnitsOutOfStockException(string message) : base(message)
        {

        }
    }
}
