using Context;
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
    public interface IPrizeRepository
    {
        
        RaffleApiContext Context { get; set; }

        Task<List<Prize>> GetAllAsync();

        Task<Prize> FindByIdAsync(int id);

        Task<Prize> AddOrUpdateAsync(Prize prize);

        Task DeleteAsync(int id);

        Task SaveAsync();


    }
}