using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.BreakServices
{
    public class BreakService : IBreakService
    {
        private readonly IRepository<Break> _repository;
        public BreakService(IRepository<Break> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                Break student = await _repository.GetById(id);
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

        public Task<Break> Find(Expression<Func<Break, bool>> match)
        {
            return _repository.Find(match);
        }

        public async Task<ICollection<BreakViewModel>> GetAll()
        {
            ICollection<BreakViewModel> orderViewModels = new List<BreakViewModel>();
            ICollection<Break> orders = await _repository.GetAll();
            foreach (Break order in orders)
            {
                BreakViewModel viewModel = new()
                {
                    Id = order.Id,
                 
                    UserId = order.UserId,
                    RequestTime = order.RequestTime,
                 
                   

                };
                orderViewModels.Add(viewModel);
            }
            return orderViewModels;
        }

        public async Task<BreakViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                BreakViewModel viewModel = new()
                {
                    Id = result.Id,
                 
                    UserId = result.UserId,
                    RequestTime = result.RequestTime,
                 
                 
                };
                return viewModel;
            }
        }

        public async Task<BreakViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                BreakViewModel viewModel = new()
                {
                    Id = result.Id,
                 
                    UserId = result.UserId,
                    RequestTime = result.RequestTime,
                 

                };
                return viewModel;
            }
        }

        public Break GetLast()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(InsertBreak inserFood)
        {
            Break order = new Break()
            {
              
                UserId = inserFood.UserId,
                RequestTime = DateTime.Now,
              

            };
            return _repository.Insert(order);
        }

        public async Task<bool> Update(UpdateBreak StudentUpdateModel)
        {
            Break student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
                student.Id = StudentUpdateModel.Id;
               
                student.UserId = StudentUpdateModel.UserId;
                student.RequestTime = StudentUpdateModel.RequestTime;
             
               

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
