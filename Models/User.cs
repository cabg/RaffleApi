using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public partial class User: BaseModel
    {
        public int First { get; set; }

        public int Last { get; set; }
    }
}
