using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : BaseEntityClass
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string MobileNo {  get; set; }
        public string City {  get; set; }
        public string Email {  get; set; }
        public string Password {  get; set; }
        public int UserTypeId { get; set; }
       

        [JsonIgnore]
        public UserType UserType { get; set; }
        public ICollection<Attendence> Attendances { get; set; }
        public ICollection<Leave> Leaves { get; set; }
        public ICollection<ClockOut> ClockOuts { get; set; }
        public ICollection<Break> Breaks { get; set; }
        public ICollection<FinishBreak> FinishBreaks { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Sessions> Sessions { get; set; }
        public ICollection<ManualRequest> ManualRequests { get; set; }

    }
}
