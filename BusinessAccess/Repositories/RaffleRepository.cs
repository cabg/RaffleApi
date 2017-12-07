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

        private readonly int RaffleCounterId = 1;

        public RaffleRepository()
        {

        }
        public async Task<List<Raffle>> GetWinnersAsync()
        {
            return await Context.Raffles.Include("Prize").Where(r => r.Status == RaffleStatus.Winner).ToListAsync();
        }

        public async Task<Raffle> GetRandom(int PrizeId)
        {
            var prizeDetail = Context.Prizes.FirstOrDefault(p => p.Id == PrizeId);

            var raffleCounter = Context.RaffleCounter.FirstOrDefault(c => c.Id == RaffleCounterId);

            var exclude = Context.Raffles.Where(r => r.RaffleCounter == raffleCounter.Counter && r.Status == RaffleStatus.NonWinner).Select(rp => rp.UserId).ToHashSet();
            var participantsRange = Context.Users.FirstOrDefault();
            var rangeRandom = Enumerable.Range(participantsRange.First, participantsRange.Last).Where(i => !exclude.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, participantsRange.Last - exclude.Count);

            var raffle = new Raffle
            {
                Prize = prizeDetail,
                Cicle = raffleCounter.Cicle,
                UserId = rangeRandom.ElementAt(index),
                RaffleCounter = raffleCounter.Counter,
                Status = RaffleStatus.NonWinner
            };

            InsertOrUpdate(raffle);

            raffleCounter.Cicle = raffleCounter.Cicle + 1;
            UpdateCounter(raffleCounter);

            await SaveAsync();


            return raffle;
        }

        public async Task<Raffle> GivePrize(int RaffleId)
        {
            var raffleData = await FindByIdAsync(RaffleId);
            
            var prizeDetail = Context.Prizes.FirstOrDefault(p => p.Id == raffleData.Prize.Id);
            
            var raffleCounter = Context.RaffleCounter.FirstOrDefault(c => c.Id == RaffleCounterId);

            raffleData.Status = RaffleStatus.Winner;
            InsertOrUpdate(raffleData);

            prizeDetail.Stock = prizeDetail.Stock - 1;
            DiscountStock(prizeDetail);

            raffleCounter.Counter = raffleCounter.Counter + 1;
            raffleCounter.Cicle = 1;
            UpdateCounter(raffleCounter);

            await SaveAsync();

            return raffleData;
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
            return await Context.Raffles.Include("Prize").FirstOrDefaultAsync(u => u.Id == id);
        }

        private void InsertOrUpdate(Raffle raffle)
        {
            if (raffle.Id != 0)
            {
                Context.Entry(raffle).State = EntityState.Modified;
            }
            else
            {
                Context.Set<Raffle>().Add(raffle);
            }            
        }

        private void UpdateCounter(RaffleCounter raffleCounter)
        {
            if (raffleCounter.Id != 0)
            {
                Context.Entry(raffleCounter).State = EntityState.Modified;
                
            }
            else
            {
                Context.Set<RaffleCounter>().Add(raffleCounter);
            }
           
        }

        public void DiscountStock(Prize prize)
        {
            if (prize.Id != 0)
            {
                Context.Entry(prize).State = EntityState.Modified;

            }
            else
            {
                Context.Set<Prize>().Add(prize);
            }
        }

        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
