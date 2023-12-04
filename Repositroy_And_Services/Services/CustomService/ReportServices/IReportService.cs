using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.ReportServices
{
    public  interface IReportService
    {
        Task<ICollection<ReportViewModel>> GetAll();
        Task<ReportViewModel> GetById(int id);
        Task<ReportViewModel> GetByName(string name);
        Report GetLast();
        Task<bool> Insert(InsertReport inserFood);
        Task<bool> Update(UpdateReport StudentUpdateModel);
        Task<bool> Delete(int id);
        Task<Report> Find(Expression<Func<Report, bool>> match);

        Task<Report> GetUserReport(int userId);
    }
}
