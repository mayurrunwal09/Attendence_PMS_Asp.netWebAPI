using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public  class ClockOutViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
 
        public DateTime ClockOutTime { get; set; }
   
    }
    public class InserClockOut
    {
        public int UserId { get; set; }
  
        public DateTime ClockOutTime { get; set; }
       
    }
    public class UpdateClockOut : InserClockOut
    {
        public int Id { get; set; }
    }
}
