using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Leave : BaseEntityClass
    {
        public int UserId {  get; set; }    
        public string LeaveType {  get; set; }
        public DateTime StartLeaveDate { get; set; }
        public DateTime EndLeaveDate { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime? ApprovalTime { get; set; }
       
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
        public bool IsPending { get; set; }
        public bool IsRejected { get; set; }

        [JsonIgnore]
        public User  Users { get; set; }
    }
}
