using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace loanapi.Models
{
    public class CalculateRequest
    {
        public double Volume { get; set; }
        public int Years { get; set; }
    }
}
