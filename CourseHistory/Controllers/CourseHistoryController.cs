using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CourseHistory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseHistoryController : ControllerBase
    {
        private readonly ILogger<CourseHistoryController> _logger;

        public CourseHistoryController(ILogger<CourseHistoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("getForCadet/{cadetId}")]
        public IEnumerable<CourseResult> Get(int cadetId)
        {          
            return Enumerable.Range(1, 5).Select(index => new CourseResult
            {
                Sequence = (byte)index,
                StartDate = DateTime.Now.AddYears(index - 5).AddMonths(-8),
                EndDate = DateTime.Now.AddYears(index -5),
                Result = $"result-{index}",
                CadetId = cadetId,
                Id = index + 1000
            }).ToArray();
        }
    }
}
