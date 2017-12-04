using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessAccess.Interfaces;
using Context;

namespace RaffleApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RafflesController : Controller
    {
        private readonly IRaffleRepository Repository;

        public RafflesController(IRaffleRepository repository, RaffleApiContext context)
        {

            this.Repository = repository;
            Repository.Context = context;
        }

        // GET api/raffles
        [HttpGet]
        public async Task<IActionResult> GetRadom()
        {
            var exclude = new HashSet<int>() { };
            var range = Enumerable.Range(1, 100).Where(i => !exclude.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, 100 - exclude.Count);

            return Ok(range.ElementAt(index));
        }
    }
}