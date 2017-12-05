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
    public class UserRepository: IUserRepository
    {
        public RaffleApiContext Context { get; set; }

        public UserRepository()
        {

        }

        public async Task<List<User>> GetAllAsync()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<User> SaveParticipants(int first, int last)
        {
            var userData = Context.Users.FirstOrDefault();
            if (userData != null)
            {
                userData.First = first;
                userData.Last = last;

                await AddOrUpdateAsync(userData);
            }
            else
            {
                var newUser = new User
                {
                    First = first,
                    Last = last
                };

                await AddOrUpdateAsync(newUser);
                userData = newUser;
            }

            return userData;
        }

        public async Task<User> AddOrUpdateAsync(User user)
        {
            InsertOrUpdate(user);
            await SaveAsync();

            return user;
        }
        
        public async Task DeleteAsync(int id)
        {
            var model = await Context.Users.FirstOrDefaultAsync(u => u.Id == id);

            Context.Entry(model).State = EntityState.Deleted;

            await Context.SaveChangesAsync();
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await Context.Users.FindAsync(id);
        }

        private void InsertOrUpdate(User user)
        {
            if (user.Id != 0)
            {
                Context.Entry(user).State = EntityState.Modified;
            }
            else
            {
                Context.Set<User>().Add(user);
            }                            
        }

        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
