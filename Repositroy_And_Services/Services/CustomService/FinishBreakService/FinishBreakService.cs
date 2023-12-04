using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.FinishBreakService
{
    public class FinishBreakService : IFinishBreakService
    {
        private readonly IRepository<FinishBreak> _repository;

        public FinishBreakService(IRepository<FinishBreak> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                FinishBreak student = await _repository.GetById(id);
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

        public Task<FinishBreak> Find(Expression<Func<FinishBreak, bool>> match)
        {
           return _repository.Find(match);
        }

        public async Task<ICollection<FinishBreakViewModel>> GetAll()
        {
            ICollection<FinishBreakViewModel> reportViewModels = new List<FinishBreakViewModel>();
            ICollection<FinishBreak> reports = await _repository.GetAll();

            foreach (FinishBreak report in reports)
            {
                FinishBreakViewModel viewModel = new()
                {
                    Id = report.Id,
                    UserId = report.UserId,
                   
                    FinishTime = report.FinishTime,

                   

                };

                reportViewModels.Add(viewModel);
            }


            return reportViewModels;
        }

        public async Task<FinishBreakViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                FinishBreakViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserId = result.UserId,

                  
                    FinishTime=result.FinishTime,
                };
                return viewModel;
            }
        }

        public async Task<FinishBreakViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                FinishBreakViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserId = result.UserId,

                   
                    FinishTime = result.FinishTime,
                };
                return viewModel;
            }
        }

        public FinishBreak GetLast()
        {
            throw new NotImplementedException();
        }

        public Task<FinishBreak> GetUserReport(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(InsertFinishBreak inserFood)
        {
            FinishBreak order = new FinishBreak()
            {
                UserId = inserFood.UserId,
              
                FinishTime = DateTime.Now,


            };
            return _repository.Insert(order);
        }

        public async Task<bool> Update(UpdateFinishBreak StudentUpdateModel)
        {
            FinishBreak student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
                student.Id = StudentUpdateModel.Id;
                student.UserId = StudentUpdateModel.UserId;
               

                student.FinishTime = DateTime.Now;
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
