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
    public interface IRaffleRepository
    {
        
        RaffleApiContext Context { get; set; }

        Task<List<Raffle>> GetWinnersAsync();

        Task<Raffle> GetRandom(int PrizeId);

        Task<Raffle> GivePrize(int RaffleId);

        Task<Raffle> FindByIdAsync(int id);

        Task<Raffle> AddOrUpdateAsync(Raffle user);

        Task DeleteAsync(int id);

        Task SaveAsync();
        Task<Raffle> SendEmailWinAsync();
    }
}