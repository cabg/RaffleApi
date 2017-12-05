using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessAccess.Interfaces;
using Context;
using Models;

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

        // GET api/raffles/getrandom/2
        [HttpGet("getrandom/{PrizeId}")]
        public async Task<IActionResult> GetRadom(int PrizeId)
        {
            var raffleDetail = await Repository.GetRandom(PrizeId);

            return Ok(raffleDetail);
        }

        // GET api/raffles/getRandom/5
        [HttpGet("giveprize/{RaffleId}")]
        public async Task<IActionResult> GivePrize(int RaffleId)
        {
            var raffleDetail = await Repository.GivePrize(RaffleId);

            return Ok(raffleDetail);
        }

        // GET api/raffles/winners
        [HttpGet("winners")]
        public async Task<IActionResult> GetWinners()
        {
            var winners = await Repository.GetWinnersAsync();

            return Ok(winners);
        }
    }
}