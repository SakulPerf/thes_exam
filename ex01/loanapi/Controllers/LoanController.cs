﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using loanapi.Facades;
using loanapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace loanapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        [HttpGet]
        public double Get() => LoanFacade.Rate;

        [HttpPost]
        public void Post([FromBody]SetRateRequest req) => LoanFacade.Rate = req?.Rate ?? 0;

        // PUT api/values/5
        [HttpPost("{volume}/{years}")]
        public IEnumerable<InterestInfo> Put(double volume, int years) => new LoanFacade().GetInterestInfo(volume, years);
    }
}
