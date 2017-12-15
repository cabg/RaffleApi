using BusinessAccess.Interfaces;
using Context;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Repositories
{
    public class RaffleRepository: IRaffleRepository
    {
        public RaffleApiContext Context { get; set; }

        private readonly int RaffleCounterId = 1;

        public RaffleRepository()
        {

        }

        public async Task<List<Raffle>> GetWinnersAsync()
        {
            return await Context.Raffles.Include("Prize").Where(r => r.Status == RaffleStatus.Winner).ToListAsync();
        }
        
        public async Task<Raffle> GetRandom(int PrizeId)
        {
            var prizeDetail = Context.Prizes.FirstOrDefault(p => p.Id == PrizeId);

            var raffleCounter = Context.RaffleCounter.FirstOrDefault(c => c.Id == RaffleCounterId);

            var partMin = Context.Users.Min(u => u.First);

            var partMax = Context.Users.Max(u => u.Last);

            var participantRanges = Context.Users.OrderBy(u => u.First).ToList();

            var excludeList = Context.Excludes.Select(e => e.Number).ToHashSet();

            //Add winners to exclude list
            excludeList = excludeList.Union(
                Context.Raffles.Where(r => (r.RaffleCounter == raffleCounter.Counter && r.Status == RaffleStatus.NonWinner) 
                || (r.RaffleCounter != raffleCounter.Counter && r.Status == RaffleStatus.Winner)).
                Select(rp => rp.UserId).ToHashSet()
                ).ToHashSet();

            for (int i = 0; i < participantRanges.Count; i++)
            {
                if ((i+1) < participantRanges.Count) {
                    var rangeLast = participantRanges[i + 1].First - participantRanges[i].Last;
                    if (rangeLast > 0)
                    {
                        var rangeExclude = Enumerable.Range(participantRanges[i].Last + 1, rangeLast - 1).ToHashSet();
                        excludeList = excludeList.Union(rangeExclude).ToHashSet();
                    }
                }               
            }

            var rangeRandom = Enumerable.Range(partMin, partMax).Where(i => !excludeList.Contains(i));
            
            var rand = new System.Random();
            int index = rand.Next(0, partMax - excludeList.Count);

            var raffle = new Raffle
            {
                Prize = prizeDetail,
                Cicle = raffleCounter.Cicle,
                UserId = rangeRandom.ElementAt(index),
                RaffleCounter = raffleCounter.Counter,
                Status = RaffleStatus.NonWinner
            };

            InsertOrUpdate(raffle);

            raffleCounter.Cicle = raffleCounter.Cicle + 1;
            UpdateCounter(raffleCounter);

            await SaveAsync();


            return raffle;
        }

        public async Task<Raffle> GivePrize(int RaffleId)
        {
            var raffleData = await FindByIdAsync(RaffleId);
            
            var prizeDetail = Context.Prizes.FirstOrDefault(p => p.Id == raffleData.Prize.Id);
            
            var raffleCounter = Context.RaffleCounter.FirstOrDefault(c => c.Id == RaffleCounterId);

            raffleData.Status = RaffleStatus.Winner;
            InsertOrUpdate(raffleData);

            prizeDetail.Stock = prizeDetail.Stock - 1;
            DiscountStock(prizeDetail);

            raffleCounter.Counter = raffleCounter.Counter + 1;
            raffleCounter.Cicle = 1;
            UpdateCounter(raffleCounter);

            await SaveAsync();

            return raffleData;
        }

        public async Task<Raffle> AddOrUpdateAsync(Raffle raffle)
        {
            InsertOrUpdate(raffle);
            await SaveAsync();

            return raffle;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await Context.Users.FirstOrDefaultAsync(u => u.Id == id);

            Context.Entry(model).State = EntityState.Deleted;

            await Context.SaveChangesAsync();
        }

        public async Task<Raffle> FindByIdAsync(int id)
        {
            return await Context.Raffles.Include("Prize").FirstOrDefaultAsync(u => u.Id == id);
        }

        private void InsertOrUpdate(Raffle raffle)
        {
            if (raffle.Id != 0)
            {
                Context.Entry(raffle).State = EntityState.Modified;
            }
            else
            {
                Context.Set<Raffle>().Add(raffle);
            }            
        }

        private void UpdateCounter(RaffleCounter raffleCounter)
        {
            if (raffleCounter.Id != 0)
            {
                Context.Entry(raffleCounter).State = EntityState.Modified;
                
            }
            else
            {
                Context.Set<RaffleCounter>().Add(raffleCounter);
            }
           
        }

        public void DiscountStock(Prize prize)
        {
            if (prize.Id != 0)
            {
                Context.Entry(prize).State = EntityState.Modified;

            }
            else
            {
                Context.Set<Prize>().Add(prize);
            }
        }

        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }
        
        public async Task<Raffle> SendEmailWinAsync()
        {
            var raffle = new Raffle();

            List<Raffle> list = await Context.Raffles.Include("Prize").Where(r => r.Status == RaffleStatus.Winner).ToListAsync();            
            bool isMessageSent = false;
            //Intialise Parameters  
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("");
            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("", "");
            client.EnableSsl = true;
            client.Credentials = credentials;

            var BodyHtml = "<table style='border: 1px solid #000000'><thead><tr><th>Winner Number</th><th>Prize</th></tr></thead><tbody>";
            foreach (var winner in list)
            {
                BodyHtml += "<tr><td>"+ winner.UserId + "</td><td>"+ winner.Prize.Name + "</td></tr>";
            }

            BodyHtml += "</tbody></table>";

            try
            {
                var mail = new System.Net.Mail.MailMessage("azure_cf21d5bee92f174016fc32b8815a177c@azure.com", "carls.burgos@gmail.com");
                mail.Subject = "Test";
                mail.Body = BodyHtml;
                mail.IsBodyHtml = true;
              
                client.Send(mail);
                isMessageSent = true;
            }
            catch (Exception ex)
            {
                isMessageSent = false;
            }
            return raffle;
        }
        
    }
}
