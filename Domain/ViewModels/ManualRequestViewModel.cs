using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ManualRequestViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AttendenceType { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        public string EmployeeRemart { get; set; }
        public string status { get; set; }
    }
    public class InsertManualRequestViewModel
    {
        public int UserId { get; set; }
        public string AttendenceType { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        public string EmployeeRemart { get; set; }
        public string status { get; set; }
    }
}
