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
    public class ExcludesController : Controller
    {
        private readonly IExcludeRepository Repository;

        public ExcludesController(IExcludeRepository repository, RaffleApiContext context)
        {
            this.Repository = repository;
            Repository.Context = context;
        }

        // GET api/prizes
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var excludes = await Repository.GetAllAsync();

            if (!excludes.Any()) NotFound();

            return Ok(excludes);
        }

        [HttpGet("save")]
        public async Task<IActionResult> SaveExclude(int number)
        {
            var excludeNumber = new Exclude
            {
                Number = number
            };
            var excludeData = await Repository.AddOrUpdateAsync(excludeNumber);

            return Ok("ok");
        }


        [HttpGet("update")]
        public async Task<IActionResult> UpdateExclude(Exclude number)
        {
            var excludeData = await Repository.AddOrUpdateAsync(number);

            return Ok("ok");
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteExclude(int id)
        {
            var excludeData = Repository.DeleteAsync(id);

            return Ok("ok");
        }

        [HttpGet("getexclude")]
        public async Task<IActionResult> GetExclude(int id)
        {
            var excludeData = await Repository.FindByIdAsync(id);            

            return Ok(excludeData);

           
        }




    }
}