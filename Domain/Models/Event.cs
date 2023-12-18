using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Event : BaseEntityClass
    {
        public string EventName {  get; set; }  
        public DateTime DateOfEvent { get; set; }
    }
}
