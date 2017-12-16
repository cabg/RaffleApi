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
    public class ExcludeRepository: IExcludeRepository
    {
        public RaffleApiContext Context { get; set; }

        public ExcludeRepository()
        {

        }

        public async Task<List<Exclude>> GetAllAsync()
        {
            return await Context.Excludes.ToListAsync();
        }

        public async Task<Exclude> AddOrUpdateAsync(Exclude number)
        {
            InsertOrUpdate(number);
            await SaveAsync();

            return number;
        }
        
        public async Task DeleteAsync(int id)
        {
            var model = Context.Excludes.FirstOrDefault(e => e.Id == id);

            Context.Entry(model).State = EntityState.Deleted;

            await Context.SaveChangesAsync();
        }

        public async Task<Exclude> FindByIdAsync(int id)
        {
            return await Context.Excludes.FindAsync(id);
        }

        private void InsertOrUpdate(Exclude number)
        {
            if (number.Id != 0)
            {
                Context.Entry(number).State = EntityState.Modified;
            }
            else
            {
                Context.Set<Exclude>().Add(number);
            }                            
        }

        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
