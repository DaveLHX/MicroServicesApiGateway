using CadetApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;

namespace CadetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadetController : ControllerBase
    {
        private readonly ILogger<CadetController> _logger;

        public CadetController(ILogger<CadetController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("get/{cadetId}")]
        public Cadet Get(int cadetId)
        {
            return new Cadet() { BirthDate = DateTime.Today.AddDays(-13), CurrentRank = 2, 
                Element = 1, Id = cadetId, FirstName = "CadetFirstName", 
                LastName = "CadetLastName", Program = 3 };
        }

        [HttpGet]
        [Route("whoami")]
        public IDictionary WhoAmI()
        {
            return  Environment.GetEnvironmentVariables();
        }
    }
}