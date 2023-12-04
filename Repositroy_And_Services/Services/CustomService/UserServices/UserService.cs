using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.common;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.UserServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                User student = await _repository.GetById(id);
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

        public Task<User> Find(Expression<Func<User, bool>> match)
        {
            return _repository.Find(match);
        }

        public async Task<ICollection<UserViewModel>> GetAll()
        {
            ICollection<UserViewModel> orderViewModels = new List<UserViewModel>();
            ICollection<User> orders = await _repository.GetAll();
            foreach (User order in orders)
            {
                UserViewModel viewModel = new()
                {
                    Id = order.Id,
                    UserName = order.UserName,
                    MobileNo = order.MobileNo,
                    City = order.City,
                    Email = order.Email,
                    Password = Encryptor.DecryptString(order.Password),

                    Role = order.Role,
                    UserTypeId = order.UserTypeId,



                };
                orderViewModels.Add(viewModel);
            }
            return orderViewModels;
        }

        public async Task<UserViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                UserViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    MobileNo = result.MobileNo,
                    City = result.City,
                    Email = result.Email,
                    Password = result.Password,
                    UserTypeId = result.UserTypeId,
                    Role = result.Role,
                };
                return viewModel;
            }
        }

        public async Task<UserViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                UserViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    MobileNo = result.MobileNo,
                    City = result.City,
                    Email = result.Email,
                    Password = result.Password,
                    UserTypeId = result.UserTypeId,
                    Role = result.Role,
                };
                return viewModel;
            }
        }

        public User GetLast()
        {
            return _repository.GetLast();
        }

        public Task<bool> Insert(InsertUser inserFood)
        {
            User order = new User()
            {
                UserName = inserFood.UserName,
                MobileNo = inserFood.MobileNo,
                City = inserFood.City,
                Email = inserFood.Email,
                Password = inserFood.Password,
                UserTypeId = inserFood.UserTypeId,
                Role = inserFood.Role,
            };
            return _repository.Insert(order);
        }

        public async Task<bool> Update(UpdateUser StudentUpdateModel)
        {
            User student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
                student.Id = StudentUpdateModel.Id;
                student.UserName = StudentUpdateModel.UserName;
                student.MobileNo = StudentUpdateModel.MobileNo;
                student.City = StudentUpdateModel.City;
                student.Email = StudentUpdateModel.Email;
                student.Password = student.Password;
                student.UserTypeId = StudentUpdateModel.UserTypeId;
                student.Role = StudentUpdateModel.Role;
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
