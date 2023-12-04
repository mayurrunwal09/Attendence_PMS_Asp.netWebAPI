using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public  class LeaveViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartLeaveDate { get; set; }
        public DateTime EndLeaveDate { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime? ApprovalTime { get; set; }
        public string Reason { get; set; }
      
        public string ApprovalStatus { get; set; }
    }
    public class InsertLeave
    {
        public int UserId { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartLeaveDate { get; set; }
        public DateTime EndLeaveDate { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime? ApprovalTime { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }

    }
    public class UpdateLeave : InsertLeave
    {
        public int Id { get; set;}
    }
}
