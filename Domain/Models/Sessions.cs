using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Sessions : BaseEntityClass
    {
        public string EventName { get; set; }
        public string EventType {  get; set; }
        public DateTime EventDate {  get; set; }
        public string MentorName {  get; set; }
        public int UserId {  get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}