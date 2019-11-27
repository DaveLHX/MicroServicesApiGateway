using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommonListsApi.Model;

namespace Military.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HrmsController : ControllerBase
    {
        private readonly ILogger<HrmsController> _logger;

        public HrmsController(ILogger<HrmsController> logger)
        {
            _logger = logger;
        }

    }
}
