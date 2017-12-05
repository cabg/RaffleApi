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
    public class UsersController : Controller
    {
        private readonly IUserRepository Repository;

        public UsersController(IUserRepository repository, RaffleApiContext context)
        {
            this.Repository = repository;
            Repository.Context = context;
        }

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await Repository.GetAllAsync();

            if (!users.Any()) NotFound();

            return Ok(users);
        }

        // GET api/users/save
        [HttpGet("save")]
        public async Task<IActionResult> SaveParticipants(int first, int last)
        {           
            var userData = await Repository.SaveParticipants(first, last);

            return Ok(userData);
        }  
    }
}