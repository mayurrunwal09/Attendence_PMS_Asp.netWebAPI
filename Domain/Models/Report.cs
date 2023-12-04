using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Report : BaseEntityClass
    {
        public int UserId { get; set; }
        public int AttendenceId {  get; set; }
        public int BreakId { get; set; }
        public int ClockOutId { get; set; } 
        public int FinishBreakId { get; set; }
        
       


        [JsonIgnore]   
        
        public Attendence Attendence { get; set; }
        public User User { get; set; }
        public Break Breaks { get; set; }
        public ClockOut ClockOut { get; set; }
        public FinishBreak finishBreak { get; set; }    

        public ClockOut ClockOuts { get; set; }
        public FinishBreak FinishBreaks { get; set; }
    }
}
