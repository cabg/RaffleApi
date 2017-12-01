using Models;
using System.Data.Entity;

namespace Context
{
    public class RaffleApiContext : DbContext
    {
        public RaffleApiContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Raffle> Raffles { get; set; }

        public DbSet<Prize> Prizes { get; set; }
    }
}
