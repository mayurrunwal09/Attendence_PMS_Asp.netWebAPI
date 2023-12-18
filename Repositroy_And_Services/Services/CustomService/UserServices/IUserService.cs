using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.UserServices
{
    public  interface IUserService<User>
    {
        Task<ICollection<UserViewModel>> GetAll();
        Task<UserViewModel> GetById(int id);
        Task<UserViewModel> GetByName(string name);
        User GetLast();
        Task<bool> Insert(InsertUser inserFood);
        Task<bool> Update(UpdateUser StudentUpdateModel);
        Task<bool> Delete(int id);
        Task<User> Find(Expression<Func<User, bool>> match);
        Task<bool> ResetPassword(string email, string newPassword);
    }
}
