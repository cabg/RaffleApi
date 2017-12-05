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

        private readonly int RaffleCounterId = 1;

        public RafflesController(IRaffleRepository repository, RaffleApiContext context)
        {

            this.Repository = repository;
            Repository.Context = context;
        }

        // GET api/raffles/getRandom/2
        [HttpGet("getRandom/{PrizeId}")]
        public async Task<IActionResult> GetRadom(int PrizeId)
        {
            var prize = await Repository.GetPrize(PrizeId);

            var rcounter = await Repository.GetRaffleCounter(RaffleCounterId);

            var exclude = await Repository.GetRaffleParticipant(rcounter.Counter);

            var userRange = await Repository.GetParticipantRange();

            var range = Enumerable.Range(userRange.First, userRange.Last).Where(i => !exclude.Contains(i));

            var rand = new System.Random();

            int index = rand.Next(0, userRange.Last - exclude.Count);

            var raffle = new Raffle
            {
                Prize = prize,
                Cicle = rcounter.Cicle,
                UserId = range.ElementAt(index),
                RaffleCounter = rcounter.Counter,
                Status = RaffleStatus.NonWinner
            };

            var test = await Repository.AddOrUpdateAsync(raffle);

            rcounter.Cicle = rcounter.Cicle + 1;

            var rctest = await Repository.UpdateCounter(rcounter);

            return Ok(test);
        }

        // GET api/raffles/getRandom/5
        [HttpGet("givePrize/{RaffleId}")]
        public async Task<IActionResult> GivePrize(int RaffleId)
        {
            var raffleData = await Repository.FindByIdAsync(RaffleId);

            var prize = await Repository.GetPrize(raffleData.Prize.Id);

            var rcounter = await Repository.GetRaffleCounter(RaffleCounterId);

            raffleData.Status = RaffleStatus.Winner;

            var test = await Repository.AddOrUpdateAsync(raffleData);

            prize.Stock = prize.Stock - 1;

            var ptest = await Repository.DiscountStock(prize);

            rcounter.Counter = rcounter.Counter + 1;
            rcounter.Cicle = 1;

            var rctest = await Repository.UpdateCounter(rcounter);

            return Ok(test);
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