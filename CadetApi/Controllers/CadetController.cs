using CadetApi.Db;
using CadetApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CadetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadetController : ControllerBase
    {
        
        private readonly CadetContext _Context;

        public CadetController(CadetContext context)
        {          
            _Context = context;
        }

        [HttpGet]
        [Route("get/{cadetId}")]
        public IActionResult Get(int cadetId)
        {
            var cadet = _Context.Cadets.FirstOrDefault(o => o.Id == cadetId);
            Log.Information("returned cadet:{@cdt}", cadet);
            return Ok(cadet);
        }


        [HttpGet]
        [Route("getAll")]
        public IActionResult Get()
        {
            List<Cadet> cadets = _Context.Cadets.ToList();
            Log.Information("retruned a list of cadets: {@cdts}",cadets);
            return Ok(cadets);
        }


        [HttpGet]
        [Route("whoami")]
        public IDictionary WhoAmI()
        {
            return  Environment.GetEnvironmentVariables();
        }
    }
}