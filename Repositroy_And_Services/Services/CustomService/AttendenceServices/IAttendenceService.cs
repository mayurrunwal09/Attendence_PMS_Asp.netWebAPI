using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.AttendenceServices
{
    public interface IAttendenceService
    {
        Task<ICollection<AttendenceViewModel>> GetAll();
        Task<AttendenceViewModel> GetById(int id);
        Task<AttendenceViewModel> GetByName(string name);
        Attendence GetLast();
        Task<bool> Insert(InsertAttendence inserFood);
        Task<bool> Update(UpdateAttendence StudentUpdateModel);
        Task<bool> Delete(int id);
        Task<Attendence> Find(Expression<Func<Attendence, bool>> match);

      
    }
}
