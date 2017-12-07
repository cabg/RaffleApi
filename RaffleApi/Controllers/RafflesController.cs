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
            var raffle = await Repository.GetRandom(PrizeId);
            if (raffle.Id !=0) NotFound();
            return Ok(raffle);
        }

        // GET api/raffles/getRandom/5
        [HttpGet("giveprize/{RaffleId}")]
        public async Task<IActionResult> GivePrize(int RaffleId)
        {
            return Ok(await Repository.GivePrize(RaffleId));
        }

        // GET api/raffles/winners
        [HttpGet("winners")]
        public async Task<IActionResult> GetWinners()
        {
            return Ok(await Repository.GetWinnersAsync());
        }
    }
}