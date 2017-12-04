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

        //public async Task<List<User>> GetAllAsync()
        //{
        //    return await Context.Users.ToListAsync();
        //}

        
        //public async Task<User> AddOrUpdateAsync(User user)
        //{
        //    InsertOrUpdate(user);
        //    await SaveAsync();

        //    return user;
        //}
        
        //public async Task DeleteAsync(int id)
        //{
        //    var model = await Context.Users.FirstOrDefaultAsync(u => u.Id == id);

        //    Context.Entry(model).State = EntityState.Deleted;

        //    await Context.SaveChangesAsync();
        //}

        //public async Task<User> FindByIdAsync(int id)
        //{
        //    return await Context.Users.FindAsync(id);
        //}

        //private void InsertOrUpdate(User user)
        //{
        //    Context.Entry(user).State = EntityState.Modified;
        //}

        //public Task SaveAsync()
        //{
        //    return Context.SaveChangesAsync();
        //}
    }
}
