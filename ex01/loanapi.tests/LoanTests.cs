using FluentAssertions;
using loanapi.Facades;
using loanapi.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace loanapi.tests
{
    public class LoanTests
    {
        [Theory(DisplayName = "Loan in a year")]
        [InlineData(12, 10000, 1200, 11200)]
        [InlineData(120, 10000, 12000, 22000)]
        [InlineData(0, 10000, 0, 10000)]
        [InlineData(0.5, 10000, 50, 10050)]
        [InlineData(10, 10000.5, 1000.05, 11000.55)]
        public void LoanRate1YearCanBeCalculatedCorrectly(double rate, double volume, double expInterest, double expTotal)
        {
            LoanFacade.Rate = rate;
            var sut = new LoanFacade();
            var actual = sut.GetInterestInfo(volume);
            Assert.Equal(volume, actual.Volume);
            Assert.Equal(expInterest, actual.Interest);
            Assert.Equal(expTotal, actual.Total);
        }

        [Theory(DisplayName = "Loan in multi years")]
        [ClassData(typeof(LoanInMultiYears))]
        public void LoanRateMuliYearsCanBeCalculatedCorrectly(double rate, double volume, int years, IEnumerable<InterestInfo> expected)
        {
            LoanFacade.Rate = rate;
            var sut = new LoanFacade();
            var actual = sut.GetInterestInfo(volume, years);
            expected.Should().BeEquivalentTo(actual);
        }
    }

    public class LoanInMultiYears : TheoryData<double, double, int, IEnumerable<InterestInfo>>
    {
        public LoanInMultiYears()
        {
            Add(12, 10000, 3, new[]
            {
                new InterestInfo{ Volume = 10000, Interest = 1200, Total = 11200 },
                new InterestInfo{ Volume = 11200, Interest = 1344, Total = 12544 },
                new InterestInfo{ Volume = 12544, Interest = 1505.28, Total = 14049.28 },
            });

            Add(10, 10000.5, 2, new[]
            {
                new InterestInfo{ Volume = 10000.5, Interest = 1000.05, Total = 11000.55 },
                new InterestInfo{ Volume = 11000.55, Interest = 1100.055, Total = 12100.605 },
            });
        }
    }
}