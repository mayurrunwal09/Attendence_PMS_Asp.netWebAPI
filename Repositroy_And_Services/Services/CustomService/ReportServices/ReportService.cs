using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace Repositroy_And_Services.Services.CustomService.ReportServices
{
    public class ReportService : IReportService
    {
        private readonly IRepository<Report> _repository;
        public ReportService(IRepository<Report> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                Report student = await _repository.GetById(id);
                if (student != null)
                {
                    return await _repository.Delete(student);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Task<Report> Find(Expression<Func<Report, bool>> match)
        {
                return _repository.Find(match);
            }

        /*  public async Task<ICollection<ReportViewModel>> GetAll()
          {
              ICollection<ReportViewModel> orderViewModels = new List<ReportViewModel>();
              ICollection<Report> orders = await _repository.GetAll();
              foreach (Report order in orders)
              {
                  ReportViewModel viewModel = new()
                  {
                      Id = order.Id,
                      UserId = order.UserId,
                      AttendanceDate = order.AttendanceDate,
                      CheckOutTime = order.CheckOutTime,
                      AttendenceId = order.AttendenceId,




                  };
                  orderViewModels.Add(viewModel);
              }
              return orderViewModels;
          }*/

        public async Task<ICollection<ReportViewModel>> GetAll()
        {
            ICollection<ReportViewModel> reportViewModels = new List<ReportViewModel>();
            ICollection<Report> reports = await _repository.GetAll();

            foreach (Report report in reports)
            {
                ReportViewModel viewModel = new()
                {
                    Id = report.Id,
                    UserId = report.UserId,
                    BreakId = report.BreakId,
                    AttendenceId = report.AttendenceId,
                    ClockOutId = report.ClockOutId,
                    FinishBreakId = report.FinishBreakId,



                };

                reportViewModels.Add(viewModel);
            }


            return reportViewModels;
        }

        public async Task<ReportViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                ReportViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserId = result.UserId,

                    BreakId = result.BreakId,
                    AttendenceId= result.AttendenceId,
                    ClockOutId = result.ClockOutId,
                    FinishBreakId = result.FinishBreakId,
                };
                return viewModel;
            }
        }

        public async Task<ReportViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                ReportViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserId = result.UserId,

                    BreakId = result.BreakId,
                    AttendenceId = result.AttendenceId,
                    ClockOutId = result.ClockOutId,
                    FinishBreakId = result.FinishBreakId,
                };
                return viewModel;
            }
        }

        public Report GetLast()
        {
            return _repository.GetLast();
        }

        public async Task<Report> GetUserReport(int userId)
        {
            var userReport = await _repository.Find(r => r.UserId == userId);

            if (userReport != null)
            {
                return userReport;
            }
            else
            {
                return null;
            }
        }

        public Task<bool> Insert(InsertReport inserFood)
        {
            Report order = new Report()
            {
                UserId = inserFood.UserId,
                AttendenceId = inserFood.AttendenceId,
                BreakId = inserFood.BreakId,
                ClockOutId = inserFood.ClockOutId,
                FinishBreakId = inserFood.FinishBreakId,
        
               
                

            };
            return _repository.Insert(order);

        }

        public async Task<bool> Update(UpdateReport StudentUpdateModel)
        {
            Report student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
                student.Id = StudentUpdateModel.Id;
                student.UserId = StudentUpdateModel.UserId;
                student.AttendenceId = StudentUpdateModel.AttendenceId;
                student.BreakId = StudentUpdateModel.BreakId;
                student.ClockOutId = StudentUpdateModel.ClockOutId;
                student.FinishBreakId = StudentUpdateModel.FinishBreakId;


                var result = await _repository.Update(student);
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
