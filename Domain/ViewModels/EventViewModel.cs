using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public  class EventViewModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime DateOfEvent { get; set; }
    }
    public class InsertEvent
    {
        public string EventName { get; set; }
        public DateTime DateOfEvent { get; set; }
    }
    public class UpdateEvent : InsertEvent
    {
        public int Id { get; set;}
    }
}
