using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Attendence : BaseEntityClass
    {
        public int UserId { get; set; }
      
        public DateTime CheckInTime { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    
      
        public ICollection<Report> ReportRequests { get; set; }


    }
}
