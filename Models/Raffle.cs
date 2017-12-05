using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public partial class Raffle: BaseModel
    {
        public Prize Prize { get; set; }

        public int Cicle { get; set;}

        public int UserId { get; set; }

        public RaffleStatus Status { get; set; }

        public int RaffleCounter { get; set; }
    }


}
