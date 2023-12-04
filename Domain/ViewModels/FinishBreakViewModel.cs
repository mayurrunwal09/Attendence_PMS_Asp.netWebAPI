using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class FinishBreakViewModel
    {
        public int Id {  get; set; }
        public int UserId { get; set; }
     

        public DateTime FinishTime { get; set; }
    }
    public class InsertFinishBreak
    {
        public int UserId { get; set; }
   

        public DateTime FinishTime { get; set; }
    }
    public class UpdateFinishBreak : InsertFinishBreak
    {
        public int Id { get; set; }
    }
}
