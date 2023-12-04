using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public  class AttendenceViewModel
    {
        public int Id {  get; set; }    
        public int UserId { get; set; }
       
        public DateTime CheckInTime { get; set; }
       

    
    }
    public class InsertAttendence
    {
        public int UserId { get; set; }
        public DateTime CheckInTime { get; set; }
       

    }
    public class UpdateAttendence : InsertAttendence
    {
        public int Id { get; set; }
    }
}
