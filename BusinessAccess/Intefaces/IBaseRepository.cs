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
    public interface IBaseRepository<TContext, TEntity> where TContext : DbContext where TEntity : BaseModel
    {
        
        //[Dependency]
        TContext Context { get; set; }

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> FindByIdAsync(int id);

        Task<TEntity> AddOrUpdateAsync(TEntity entity);

        Task DeleteAsync(int id);

        Task SaveAsync();


    }
}