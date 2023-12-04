using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.UserTypeServices
{
    public  interface IUserTypeService
    {
        Task<ICollection<UserTypeViewModel>> GetAll();
        Task<UserTypeViewModel> GetById(int id);
        Task<UserTypeViewModel> GetByName(string name);
        UserType GetLast();
        Task<bool> Insert(InsertUserType inserFood);
        Task<bool> Update(UpdateUserType StudentUpdateModel);
        Task<bool> Delete(int id);
        Task<UserType> Find(Expression<Func<UserType, bool>> match);
    }
}
