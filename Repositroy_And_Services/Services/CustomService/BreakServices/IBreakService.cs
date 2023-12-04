using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.BreakServices
{
    public interface IBreakService
    {
        Task<ICollection<BreakViewModel>> GetAll();
        Task<BreakViewModel> GetById(int id);
        Task<BreakViewModel> GetByName(string name);
        Break GetLast();
        Task<bool> Insert(InsertBreak inserFood);
        Task<bool> Update(UpdateBreak StudentUpdateModel);
        Task<bool> Delete(int id);
        Task<Break> Find(Expression<Func<Break, bool>> match);
    }
}
