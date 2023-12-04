using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.ViewModels
{
    public  class ReportViewModel
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public int AttendenceId { get; set; }
        public int BreakId { get; set; }
        public int ClockOutId { get; set; }
        public int FinishBreakId { get; set; }


    }
    public class InsertReport
    {
        public int UserId { get; set; }
        public int AttendenceId { get; set; }
        public int BreakId { get; set; }
        public int ClockOutId { get; set; }
        public int FinishBreakId { get; set; }



    }
    public class UpdateReport : InsertReport
    {
        public int Id { get; set; }
    }
}
