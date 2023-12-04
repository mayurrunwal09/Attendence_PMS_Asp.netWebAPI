using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class FinishBreak : BaseEntityClass
    {
        public int UserId { get; set; }
   

        public DateTime FinishTime { get; set; }


        [JsonIgnore]

        
        public User User { get; set; }
      
        public ICollection<Report> Reports { get; set; }
    }
}
