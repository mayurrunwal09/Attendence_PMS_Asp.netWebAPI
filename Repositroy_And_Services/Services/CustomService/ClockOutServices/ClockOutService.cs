using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.ClockOutServices
{
    public class ClockOutService : IClockOutService

    {
        private readonly IRepository<ClockOut> _repository;
      
        public ClockOutService(IRepository<ClockOut> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                ClockOut student = await _repository.GetById(id);
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

        public Task<ClockOut> Find(Expression<Func<ClockOut, bool>> match)
        {
           return _repository.Find(match);
        }

        public async Task<ICollection<ClockOutViewModel>> GetAll()
        {
            ICollection<ClockOutViewModel> reportViewModels = new List<ClockOutViewModel>();
            ICollection<ClockOut> reports = await _repository.GetAll();

            foreach (ClockOut report in reports)
            {
                ClockOutViewModel viewModel = new()
                {
                    Id = report.Id,
                    UserId = report.UserId,
                    
                  
              
                    ClockOutTime = report.ClockOutTime,
                    
                };

                reportViewModels.Add(viewModel);
            }


            return reportViewModels;
        }

        public async Task<ClockOutViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                ClockOutViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserId = result.UserId,

                    ClockOutTime = result.ClockOutTime,
                  
                };
                return viewModel;
            }
        }

        public async Task<ClockOutViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                ClockOutViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserId = result.UserId,

                    ClockOutTime = result.ClockOutTime,
             
                };
                return viewModel;
            }
        }

        public ClockOut GetLast()
        {
            throw new NotImplementedException();
        }

        public async Task<ClockOut> GetUserReport(int userId)
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

        public Task<bool> Insert(InserClockOut inserFood)
        {
            ClockOut order = new ClockOut()
            {
                UserId = inserFood.UserId,
            
          

                ClockOutTime = DateTime.Now,


            };
            return _repository.Insert(order);
        }

        public async Task<bool> Update(UpdateClockOut StudentUpdateModel)
        {
            ClockOut student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
                student.Id = StudentUpdateModel.Id;
                student.UserId = StudentUpdateModel.UserId;
          


                student.ClockOutTime = StudentUpdateModel.ClockOutTime;
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
