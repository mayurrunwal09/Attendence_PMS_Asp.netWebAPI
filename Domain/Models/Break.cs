using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Break : BaseEntityClass
    {
    
        public int UserId { get; set; } 
        public DateTime RequestTime { get; set; }
      

        [JsonIgnore]
    
        public User User { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<FinishBreak> FinishBreak { get; set; }
    }
}
