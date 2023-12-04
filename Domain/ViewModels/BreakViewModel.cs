using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class BreakViewModel
    {
        public int Id {  get; set; }

        public int UserId { get; set; }

        public DateTime RequestTime { get; set; }
     
    }
    public class InsertBreak
    {
      
        public int UserId { get; set; }
        public DateTime RequestTime { get; set; }
       
    }
    public class UpdateBreak : InsertBreak
    {
        public int Id { get; set;}
    }
}
