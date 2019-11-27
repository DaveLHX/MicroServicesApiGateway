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
    public class MilitaryController : ControllerBase
    {
        private readonly ILogger<MilitaryController> _logger;

        public MilitaryController(ILogger<MilitaryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Ranks")]
        public IEnumerable<Rank> GetRanks()
        {
            var rnd  =new Random();
            return Enumerable.Range(1, 20).Select(index => new Rank
            {
                Sequence = (byte)index,
                Element = rnd.Next(1, 3),
                Text = $"Rank-{index}",
                Id = index
            }).ToArray();
        }

        [HttpGet]
        [Route("Elements")]
        public IEnumerable<Element> GetElements()
        {
            var rnd = new Random();
            return Enumerable.Range(1, 3).Select(index => new Element
            {
                Sequence = (byte)index,             
                Text = $"Element-{index}",
                Id = index
            }).ToArray();
        }
    }
}
