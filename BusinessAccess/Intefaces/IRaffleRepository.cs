﻿using Context;
using Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Interfaces
{
    public interface IRaffleRepository
    {
        
        RaffleApiContext Context { get; set; }

        Task<int> GetRaffleCounter(int id);

        Task<List<int>> GetRaffleParticipant(int id);
        //Task<List<Raffle>> GetAllAsync();

        //Task<User> FindByIdAsync(int id);

        //Task<User> AddOrUpdateAsync(User user);

        //Task DeleteAsync(int id);

        //Task SaveAsync();


    }
}