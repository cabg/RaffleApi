using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<RaffleApiContext>
    {
        protected override void Seed(RaffleApiContext context)
        {
            base.Seed(context);
            User users = new User
            {
                First = 1,
                Last = 100
            };
            context.Users.Add(users);
            context.SaveChanges();
        }
    }
}
