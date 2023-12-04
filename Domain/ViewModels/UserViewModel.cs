using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public string Role { get; set; }

    }
    public class InsertUser
    {
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public string Role { get; set; }
    }
    public class UpdateUser : InsertUser
    {
        public int Id { get; set;}
    }

    public class LoginModel
    {

       
        public string UserName { get; set; }

       
        public string Password { get; set; }
    }
}
