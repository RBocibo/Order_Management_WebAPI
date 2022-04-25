using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Contracts.DTO
{
    public class BaseResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string[] Error { get; set; }
    }
}
