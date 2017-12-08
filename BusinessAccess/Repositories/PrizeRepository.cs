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
    public class PrizeRepository: IPrizeRepository
    {
        public RaffleApiContext Context { get; set; }

        public PrizeRepository()
        {

        }

        public async Task<List<Prize>> GetAllAsync()
        {
            return await Context.Prizes.Where(p => p.Status == PrizeStatus.Active && p.Stock > 0).ToListAsync();

        }
        
        public async Task<Prize> AddOrUpdateAsync(Prize prize)
        {
            InsertOrUpdate(prize);
            await SaveAsync();

            return prize;
        }
        
        public async Task DeleteAsync(int id)
        {
            var model = await Context.Prizes.FirstOrDefaultAsync(p => p.Id == id);

            Context.Entry(model).State = EntityState.Deleted;

            await Context.SaveChangesAsync();
        }

        public async Task<Prize> FindByIdAsync(int id)
        {
            return await Context.Prizes.FindAsync(id);
        }

        private void InsertOrUpdate(Prize prize)
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

        public async Task<Prize> SavePrize(String Name, int Stock)
        {
                var newUser = new Prize
                {
                    Name = Name,
                    Stock = Stock,
                    Status = PrizeStatus.Active
                };
                if (Name != null && Stock >= 0)
                {
                    await AddOrUpdateAsync(newUser);
                
                }
            return newUser;
        }

        public async Task<Prize> UpdatePrize(int id, String Name, int Stock, int Status)
        {

            var preziData = Context.Prizes.FirstOrDefault(p => p.Id == id);
            if (id>=0 && Name != null &&  Stock >=0 && (Status==0 || Status == 1) ) { 
           // var preziData = await Context.Prizes.FirstOrDefaultAsync(p => p.Id == id);
           
            if (preziData != null)
            {
                preziData.Name = Name;
                preziData.Stock = Stock;
                preziData.Status = (Status == 1)? PrizeStatus.Active:PrizeStatus.Inactive;

                await AddOrUpdateAsync(preziData);
            }
            }
            return preziData;
        }

        public async Task<Prize> DeletePrize(int id)
        {
            var model = await Context.Prizes.FirstOrDefaultAsync(p => p.Id == id);

            Context.Entry(model).State = EntityState.Deleted;

            await Context.SaveChangesAsync();
            return model;
        }

        public async Task<Prize>  GetPrize(int id)
        {

           
            var model = await Context.Prizes.FirstOrDefaultAsync(p => p.Id == id);
            return model;
        }


        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }
        public async Task<List<Prize>> GetAllPrizes()
        {
            return await Context.Prizes.Where(p => p.Stock > 0).ToListAsync();

        }

    }
}
