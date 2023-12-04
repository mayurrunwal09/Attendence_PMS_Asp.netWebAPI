using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class UserTypeViewModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

      /*  public ICollection<User> Users { get; set; }*/
    }
    public class InsertUserType
    {
        public string TypeName { get; set; }

    
    }
    public class UpdateUserType : InsertUserType
    {
        public int Id { get; set;}
    }

}
