using BusinessAccess.Interfaces;
using Context;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Repositories
{
    public class RaffleRepository: IRaffleRepository
    {
        public RaffleApiContext Context { get; set; }

        public RaffleRepository()
        {

        }
        public async Task<List<Raffle>> GetWinnersAsync()
        {
            return await Context.Raffles.Where(r => r.Status == RaffleStatus.Winner).ToListAsync();
        }

        public async Task<RaffleCounter> GetRaffleCounter(int id)
        {
            return Context.RaffleCounter.FirstOrDefault(c => c.Id == id);
        }

        public async Task<HashSet<int>> GetRaffleParticipant(int id)
        {
            return Context.Raffles.Where(r => r.RaffleCounter == id && r.Status == RaffleStatus.NonWinner).Select(rp => rp.UserId).ToHashSet();
        }

        public async Task<User> GetParticipantRange()
        {
            return Context.Users.FirstOrDefault();
        }

        public async Task<Prize> GetPrize(int id)
        {
            return Context.Prizes.FirstOrDefault(p => p.Id == id);
        }

        public async Task<RaffleCounter> UpdateCounter(RaffleCounter rcounter)
        {
            Context.Entry(rcounter).State = EntityState.Modified;
            Context.SaveChangesAsync();

            return rcounter;
        }

        public async Task<Prize> DiscountStock(Prize prize)
        {
            Context.Entry(prize).State = EntityState.Modified;
            Context.SaveChangesAsync();

            return prize;
        }

        public async Task<Raffle> AddOrUpdateAsync(Raffle raffle)
        {
            InsertOrUpdate(raffle);
            await SaveAsync();

            return raffle;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await Context.Users.FirstOrDefaultAsync(u => u.Id == id);

            Context.Entry(model).State = EntityState.Deleted;

            await Context.SaveChangesAsync();
        }

        public async Task<Raffle> FindByIdAsync(int id)
        {
            return await Context.Raffles.FindAsync(id);
        }

        private void InsertOrUpdate(Raffle raffle)
        {
            Context.Entry(raffle).State = EntityState.Modified;
        }

        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
