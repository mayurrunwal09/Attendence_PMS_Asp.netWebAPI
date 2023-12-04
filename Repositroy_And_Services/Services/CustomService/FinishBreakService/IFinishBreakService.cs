using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.FinishBreakService
{
    public  interface IFinishBreakService
    {
        Task<ICollection<FinishBreakViewModel>> GetAll();
        Task<FinishBreakViewModel> GetById(int id);
        Task<FinishBreakViewModel> GetByName(string name);
        FinishBreak GetLast();
        Task<bool> Insert(InsertFinishBreak inserFood);
        Task<bool> Update(UpdateFinishBreak StudentUpdateModel);
        Task<bool> Delete(int id);
        Task<FinishBreak> Find(Expression<Func<FinishBreak, bool>> match);

        Task<FinishBreak> GetUserReport(int userId);
    }
}
