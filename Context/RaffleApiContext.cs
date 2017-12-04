using Microsoft.EntityFrameworkCore;
using Models;

namespace Context
{
    public class RaffleApiContext : DbContext
    {
        public RaffleApiContext(DbContextOptions<RaffleApiContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Raffle> Raffles { get; set; }

        public DbSet<Prize> Prizes { get; set; }
    }
}
