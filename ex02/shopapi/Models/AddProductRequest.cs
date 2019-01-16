using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapi.Models
{
    public class AddProductRequest
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
