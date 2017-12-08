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
    public class PrizesController : Controller
    {
        private readonly IPrizeRepository Repository;

        public PrizesController(IPrizeRepository repository, RaffleApiContext context)
        {
            this.Repository = repository;
            Repository.Context = context;
        }

        // GET api/prizes
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var prizes = await Repository.GetAllAsync();

            if (!prizes.Any()) NotFound();

            return Ok(prizes);
        }

        [HttpGet("save")]
        public async Task<IActionResult> SavePrize(String Name, int Stock)
        {
            var userData = await Repository.SavePrize(Name, Stock);

            return Ok("ok");
        }


        [HttpGet("update")]
        public async Task<IActionResult> UpdatePrize(int id,String Name, int Stock, int Status)
        {
            var prizeData = await Repository.UpdatePrize(id,Name, Stock,Status);

            return Ok("ok");
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeletePrize(int id)
        {
            var prizeData = await Repository.DeletePrize(id);

            return Ok("ok");
        }

        [HttpGet("getprize")]
        public async Task<IActionResult> GetPrize(int id)
        {
            var prizeData = await Repository.GetPrize(id);

            //if (!prizeData.Any()) NotFound();

            return Ok(prizeData);

           
        }
        // GET api/prizes
        [HttpGet("GetAllPrizes")]
        public async Task<IActionResult> GetAllPrizes()
        {
            var prizes = await Repository.GetAllPrizes();

            if (!prizes.Any()) NotFound();

            return Ok(prizes);
        }




    }
}