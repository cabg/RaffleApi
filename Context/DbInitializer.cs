using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;

namespace Context
{
    public class DbInitializer
    {
        public static void Initialize(RaffleApiContext dbContext)
        {
            RaffleConfig(dbContext);
        }

        public static void RaffleConfig(RaffleApiContext dbContext)
        {

            if (!dbContext.RaffleCounter.Any())
            {
                var rafflecounter = new RaffleCounter
                {
                    Counter = 1,
                    Cicle = 1
                };
                dbContext.Set<RaffleCounter>().Add(rafflecounter);
            }

            dbContext.SaveChanges();
        }
    }
}
