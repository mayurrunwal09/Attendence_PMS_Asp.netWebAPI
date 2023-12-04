using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.LeaveServices
{
    public  interface ILeaveService
    {
        Task<ICollection<LeaveViewModel>> GetAll();
        Task<LeaveViewModel> GetById(int id);
        Task<LeaveViewModel> GetByName(string name);
        Leave GetLast();
        Task<bool> Insert(InsertLeave inserFood);
        Task<bool> Update(UpdateLeave StudentUpdateModel);
        Task<bool> Delete(int id);
        Task<Leave> Find(Expression<Func<Leave, bool>> match);

        Task<bool> ApplyLeave(InsertLeave leaveModel);
        Task<IEnumerable<LeaveViewModel>> GetAllLeaveForHR();
        Task<IEnumerable<LeaveViewModel>> GetLeaveByUserId(int userId);
        Task<bool> ApproveLeave(int leaveId);
        Task<bool> RejectLeave(int leaveId);
    }
}
