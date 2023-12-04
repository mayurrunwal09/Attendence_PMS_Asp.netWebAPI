using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.ClockOutServices
{
    public  interface IClockOutService
    {
        Task<ICollection<ClockOutViewModel>> GetAll();
        Task<ClockOutViewModel> GetById(int id);
        Task<ClockOutViewModel> GetByName(string name);
        ClockOut GetLast();
        Task<bool> Insert(InserClockOut inserFood);
        Task<bool> Update(UpdateClockOut StudentUpdateModel);
        Task<bool> Delete(int id);
        Task<ClockOut> Find(Expression<Func<ClockOut, bool>> match);

        Task<ClockOut> GetUserReport(int userId);
    }
}
