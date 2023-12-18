using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using Repositroy_And_Services.common;
using Repositroy_And_Services.context;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Repositroy_And_Services.Services.CustomService.UserServices
{
    public class UserService : IUserService<User>
    {
        private readonly IRepository<User> _repository;
        private MainDBContext _dbContext;
      

        public UserService(IRepository<User> repository, MainDBContext context)
        {
            _repository = repository;
            _dbContext = context;
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
            var leaveApplications = (await _repository.GetAll()).ToList();

            var result = leaveApplications.Select(l =>
            {
                var user = _dbContext.UserTypes.FirstOrDefault(u => u.Id == l.UserTypeId);
                var username = user != null ? user.TypeName : "Unknown User";

                return new UserViewModel
                {
                    Id = l.Id,
                    UserName = l.UserName,
                    MobileNo = l.MobileNo,
                    City = l.City,
                    Email = l.Email,
                    Password = Encryptor.DecryptString(l.Password),
                    UserTypeId = l.UserTypeId,
                    TypeName = username,
                    Role = l.Role,

                };
            }).ToList(); // Explicitly convert to ICollection<UserViewModel>

            return result;
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
                // Retrieve UserType information
                var userType = _dbContext.UserTypes.FirstOrDefault(u => u.Id == result.UserTypeId);
                var typeName = userType != null ? userType.TypeName : "Unknown Type";

                UserViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    MobileNo = result.MobileNo,
                    City = result.City,
                    Email = result.Email,
                    Password = Encryptor.DecryptString(result.Password),
                    UserTypeId = result.UserTypeId,
                    TypeName = typeName,  // Use TypeName instead of UserTypeId
                    Role = result.Role,
                };
                return viewModel;
            }
        }

        /*public async Task<UserViewModel> GetById(int id)
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
                    Password = Encryptor.DecryptString(result.Password),
                    UserTypeId = result.UserTypeId,
                    Role = result.Role,
                };
                return viewModel;
            }
        }*/

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


        public async Task<bool> ResetPassword(string email, string newPassword)
        {
            var user = await _repository.Find(u => u.Email == email);

            if (user != null)
            {
                string newPasswordHash = Encryptor.EncryptString(newPassword);

                Console.WriteLine($"User ID: {user.Id}, Email: {user.Email}, New Password Hash: {newPasswordHash}");

                user.Password = newPasswordHash;

                bool updateResult = await _repository.Update(user);


                Console.WriteLine($"Update Result: {updateResult}");

                if (updateResult)
                {

                    User updatedUser = await _repository.GetById(user.Id);


                    Console.WriteLine($"Updated User ID: {updatedUser?.Id}, Email: {updatedUser?.Email}, Password: {updatedUser?.Password}");

                    if (updatedUser != null)
                    {

                        return true;
                    }
                }
            }

            return false;
        }


    }
}







