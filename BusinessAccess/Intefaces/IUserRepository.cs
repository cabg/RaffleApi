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
    public interface IUserRepository
    {
        
        RaffleApiContext Context { get; set; }

        Task<List<User>> GetAllAsync();

        Task<User> FindByIdAsync(int id);

        Task<User> SaveParticipants(int first, int last);

        Task<User> AddOrUpdateAsync(User user);

        Task DeleteAsync(int id);

        Task SaveAsync();

    }
}