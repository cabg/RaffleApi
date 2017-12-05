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
            return await Context.Prizes.ToListAsync();
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
            Context.Entry(prize).State = EntityState.Modified;
        }

        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }        
    }
}
