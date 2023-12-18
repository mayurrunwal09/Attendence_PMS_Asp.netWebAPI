using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ManualRequest : BaseEntityClass
    {
        public int UserId { get; set; }
        public string AttendenceType { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set;}
        public string EmployeeRemart { get; set; }
        public string status { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
