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
    public interface IExcludeRepository
    {
        
        RaffleApiContext Context { get; set; }

        Task<List<Exclude>> GetAllAsync();

        Task<Exclude> FindByIdAsync(int id);

        Task<Exclude> AddOrUpdateAsync(Exclude user);

        Task DeleteAsync(int id);

        Task SaveAsync();

    }
}