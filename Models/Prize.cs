using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public partial class Prize : BaseModel
    {
        public string Name { get; set;}
        
        public int Stock { get; set;}

        public PrizeStatus Status { get; set; }
    }
}
