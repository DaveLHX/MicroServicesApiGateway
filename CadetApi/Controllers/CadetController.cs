using CadetApi.Db;
using CadetApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Linq;

namespace CadetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadetController : ControllerBase
    {
        private readonly ILogger<CadetController> _logger;
        private readonly CadetContext _Context;

        public CadetController(ILogger<CadetController> logger,CadetContext context)
        {
            _logger = logger;
            _Context = context;
        }

        [HttpGet]
        [Route("get/{cadetId}")]
        public IActionResult Get(int cadetId)
        {
            return Ok(_Context.Cadets.FirstOrDefault(o => o.Id == cadetId));
        }


        [HttpGet]
        [Route("getAll")]
        public IActionResult Get()
        {
            return Ok(_Context.Cadets.ToList());
        }


        [HttpGet]
        [Route("whoami")]
        public IDictionary WhoAmI()
        {
            return  Environment.GetEnvironmentVariables();
        }
    }
}