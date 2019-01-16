using loanapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace loanapi.Facades
{
    public class LoanFacade
    {
        public static double Rate { get; set; }

        public InterestInfo GetInterestInfo(double volume)
        {
            var interest = volume * Rate / 100;
            return new InterestInfo
            {
                Volume = volume,
                Interest = interest,
                Total = volume + interest,
            };
        }

        public IEnumerable<InterestInfo> GetInterestInfo(double volum, int years)
        {
            var result = new List<InterestInfo>();
            while (years-- > 0)
            {
                var interest = GetInterestInfo(volum);
                result.Add(interest);
                volum = interest.Total;
            }
            return result;
        }
    }
}