using loanapi.Facades;
using System;
using Xunit;

namespace loanapi.tests
{
    public class LoanTests
    {
        [Theory]
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
    }
}