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

        Task<RaffleCounter> GetRaffleCounter(int id);

        Task<HashSet<int>> GetRaffleParticipant(int id);

        Task<User> GetParticipantRange();

        Task<Prize> GetPrize(int id);

        Task<RaffleCounter> UpdateCounter(RaffleCounter rconter);

        Task<Prize> DiscountStock(Prize prize);

        Task<Raffle> FindByIdAsync(int id);

        Task<Raffle> AddOrUpdateAsync(Raffle user);

        Task DeleteAsync(int id);

        Task SaveAsync();


    }
}