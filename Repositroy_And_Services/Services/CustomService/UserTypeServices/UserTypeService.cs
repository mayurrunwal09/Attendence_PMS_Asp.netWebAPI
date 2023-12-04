using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.UserTypeServices
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IRepository<UserType> _repository;
        public UserTypeService(IRepository<UserType> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                UserType student = await _repository.GetById(id);
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

        public Task<UserType> Find(Expression<Func<UserType, bool>> match)
        {
            return _repository.Find(match);
        }

        public async Task<ICollection<UserTypeViewModel>> GetAll()
        {
            ICollection<UserTypeViewModel> orderViewModels = new List<UserTypeViewModel>();
            ICollection<UserType> orders = await _repository.GetAll();
            foreach (UserType order in orders)
            {
                UserTypeViewModel viewModel = new()
                {
                    Id = order.Id,
                    TypeName = order.TypeName,
                   

                };
                orderViewModels.Add(viewModel);
            }
            return orderViewModels;
        }

        public async Task<UserTypeViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                UserTypeViewModel viewModel = new()
                {
                    Id = result.Id,
                   TypeName = result.TypeName,  
                };
                return viewModel;
            }
        }

        public async Task<UserTypeViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                UserTypeViewModel viewModel = new()
                {
                    Id = result.Id,
                   TypeName = result.TypeName,
                };
                return viewModel;
            }
        }

        public UserType GetLast()
        {
            return _repository.GetLast();
        }

        public Task<bool> Insert(InsertUserType inserFood)
        {
            UserType order = new UserType()
            {
               TypeName = inserFood.TypeName,
            };
            return _repository.Insert(order);
        }

        public async Task<bool> Update(UpdateUserType StudentUpdateModel)
        {
            UserType student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
               student.Id = StudentUpdateModel.Id;
              student.TypeName = StudentUpdateModel.TypeName;

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
