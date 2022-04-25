using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Contracts.Enums
{
    public enum OrderStatus
    {
        [Description("Reserve")]
        Reserved = 1,
        [Description("Cancel")]
        Cancelled = 2,
        [Description("Complete")]
        Completed = 3
    }
}
