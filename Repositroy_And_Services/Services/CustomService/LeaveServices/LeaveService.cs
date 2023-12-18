/*using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.LeaveServices
{
    public class LeaveService : ILeaveService
    {
        private readonly IRepository<Leave> _repository;
        public LeaveService(IRepository<Leave> repository)
        {
            _repository = repository;
        }

        public async Task<bool> ApplyLeave(InsertLeave leaveModel)
        {
            var leave = new Leave
            {
                UserId = leaveModel.UserId,
                LeaveType = leaveModel.LeaveType,
                StartLeaveDate = leaveModel.StartLeaveDate,
                EndLeaveDate = leaveModel.EndLeaveDate,
               
                ApprovalTime = null,
                Reason = leaveModel.Reason,
                IsApproved = false,
                IsRejected = false,
                IsPending = true,
            };

            await _repository.Insert(leave);
            return true;
        }

        public async Task<bool> ApproveLeave(int leaveId)
        {
            var leave = await _repository.GetById(leaveId);
            if (leave != null)
            {
                leave.IsApproved = true;
                leave.IsPending = false;
                leave.IsRejected = false;
                leave.ApprovalTime = DateTime.Now;
                await _repository.Update(leave);

                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                Leave student = await _repository.GetById(id);
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

        public Task<Leave> Find(Expression<Func<Leave, bool>> match)
        {
            return _repository.Find(match);
        }

        public async Task<ICollection<LeaveViewModel>> GetAll()
        {
            ICollection<LeaveViewModel> orderViewModels = new List<LeaveViewModel>();
            ICollection<Leave> orders = await _repository.GetAll();
            foreach (Leave order in orders)
            {
                LeaveViewModel viewModel = new()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    LeaveType = order.LeaveType,
                    RequestTime = order.RequestTime,
                    ApprovalTime = order.ApprovalTime,
                    Reason = order.Reason,
              

                };
                orderViewModels.Add(viewModel);
            }
            return orderViewModels;
        }

        public async Task<IEnumerable<LeaveViewModel>> GetAllLeaveForHR()
        {
            var leaveApplications = await _repository.GetAll();
            return leaveApplications.Select(l => new LeaveViewModel
            {
                Id = l.Id,
                UserId = l.UserId,
                LeaveType = l.LeaveType,
                StartLeaveDate = l.StartLeaveDate,
                EndLeaveDate = l.EndLeaveDate,
                RequestTime = l.RequestTime,
                ApprovalTime = l.ApprovalTime,
                Reason = l.Reason,
               
                ApprovalStatus = GetApprovalStatus(l),

            });
        }

        private string GetApprovalStatus(Leave leave)
        {
            if (leave.IsApproved)
            {
                return "Approved";
            }
            else if (leave.IsPending)
            {
                return "Pending";
            }
            else if (leave.IsRejected)
            {
                return "Rejected";
            }
            else
            {
                return "Unknown"; 
            }
        }
        public async Task<LeaveViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                LeaveViewModel viewModel = new()
                {
                    Id = result.Id,

                    UserId = result.UserId,
                    LeaveType = result.LeaveType,
                    RequestTime = result.RequestTime,
                    ApprovalTime = DateTime.Now,
                    Reason = result.Reason,
                  
                    ApprovalStatus = GetApprovalStatus(result),
                };
                return viewModel;
            }
        }

        public async Task<LeaveViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                LeaveViewModel viewModel = new()
                {
                    Id = result.Id,

                    UserId = result.UserId,
                    LeaveType = result.LeaveType,
                    RequestTime = result.RequestTime,
                    ApprovalTime = result.ApprovalTime,
                    Reason = result.Reason,
                
                };
                return viewModel;
            }
        }

        public Leave GetLast()
        {
            return _repository.GetLast();
        }

        public async Task<IEnumerable<LeaveViewModel>> GetLeaveByUserId(int userId)
        {
            var leaveData = await _repository.FindAll(l => l.UserId == userId);

            if (leaveData != null && leaveData.Any())
            {
                return leaveData.Select(l => new LeaveViewModel
                {
                    Id = l.Id,
                    UserId = l.UserId,
              
                    LeaveType = l.LeaveType,
                    StartLeaveDate  = l.StartLeaveDate,
                    EndLeaveDate = l.EndLeaveDate,  
                    RequestTime = l.RequestTime,
                    ApprovalTime = l.ApprovalTime,
                    Reason = l.Reason,
                    ApprovalStatus = GetApprovalStatus(l),

                });
            }
            else
            {
                return Enumerable.Empty<LeaveViewModel>();
            }
        }


        public Task<bool> Insert(InsertLeave inserFood)
        {
            Leave order = new Leave()
            {
                UserId = inserFood.UserId,
                LeaveType = inserFood.LeaveType,
             
                Reason = inserFood.Reason,
                IsApproved = inserFood.IsApproved,


            };
            return _repository.Insert(order);
        }

        public async Task<bool> RejectLeave(int leaveId)
        {
            var leave = await _repository.GetById(leaveId);
            if (leave != null)
            {
                leave.IsApproved = false;
                leave.IsPending = false;
                leave.IsRejected = true;
                leave.ApprovalTime = DateTime.Now;
                await _repository.Update(leave);
                return true;
            }
            return false;
        }

        public async Task<bool> Update(UpdateLeave StudentUpdateModel)
        {
            Leave student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
                student.Id = StudentUpdateModel.Id;
                student.UserId = StudentUpdateModel.UserId;
                student.LeaveType = StudentUpdateModel.LeaveType;
                student.Reason = StudentUpdateModel.Reason;
                student.StartLeaveDate = StudentUpdateModel.StartLeaveDate;
                student.EndLeaveDate = StudentUpdateModel.EndLeaveDate;
                student.IsApproved = StudentUpdateModel.IsApproved;
                student.IsRejected = StudentUpdateModel.IsRejected;
              


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
*/








using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using Repositroy_And_Services.context;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.LeaveServices
{
    public class LeaveService : ILeaveService
    {
        private readonly MainDBContext _dbContext;
        private readonly IRepository<Leave> _repository;
        public LeaveService(IRepository<Leave> repository, MainDBContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<bool> ApplyLeave(InsertLeave leaveModel)
        {
            var leave = new Leave
            {
                UserId = leaveModel.UserId,
                LeaveType = leaveModel.LeaveType,
                StartLeaveDate = leaveModel.StartLeaveDate,
                EndLeaveDate = leaveModel.EndLeaveDate,

                ApprovalTime = null,
                Reason = leaveModel.Reason,
                IsApproved = false,
                IsRejected = false,
                IsPending = true,
            };

            await _repository.Insert(leave);
            return true;
        }

        public async Task<bool> ApproveLeave(int leaveId)
        {
            var leave = await _repository.GetById(leaveId);
            if (leave != null)
            {
                leave.IsApproved = true;
                leave.IsPending = false;
                leave.IsRejected = false;
                leave.ApprovalTime = DateTime.Now;
                await _repository.Update(leave);

                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                Leave student = await _repository.GetById(id);
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

        public Task<Leave> Find(Expression<Func<Leave, bool>> match)
        {
            return _repository.Find(match);
        }

        public async Task<ICollection<LeaveViewModel>> GetAll()
        {
            ICollection<LeaveViewModel> orderViewModels = new List<LeaveViewModel>();
            ICollection<Leave> orders = await _repository.GetAll();
            foreach (Leave order in orders)
            {
                LeaveViewModel viewModel = new()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    LeaveType = order.LeaveType,
                    RequestTime = order.RequestTime,
                    ApprovalTime = order.ApprovalTime,
                    Reason = order.Reason,


                };
                orderViewModels.Add(viewModel);
            }
            return orderViewModels;
        }

        /* public async Task<IEnumerable<LeaveViewModel>> GetAllLeaveForHR()
         {
             var leaveApplications = await _repository.GetAll();
             return leaveApplications.Select(l => new LeaveViewModel
             {
                 Id = l.Id,
                 UserId = l.UserId,
                 LeaveType = l.LeaveType,
                 StartLeaveDate = l.StartLeaveDate,
                 EndLeaveDate = l.EndLeaveDate,
                 RequestTime = l.RequestTime,
                 ApprovalTime = l.ApprovalTime,
                 Reason = l.Reason,

                 ApprovalStatus = GetApprovalStatus(l),

             });
         }*/



        public async Task<IEnumerable<LeaveViewModel>> GetAllLeaveForHR()
        {
            var leaveApplications = (await _repository.GetAll()).ToList();

            var result = leaveApplications.Select(l =>
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == l.UserId);

                var username = user != null ? user.UserName : "Unknown User";

                return new LeaveViewModel
                {
                    Id = l.Id,
                    UserId = l.UserId,
                    LeaveType = l.LeaveType,
                    StartLeaveDate = l.StartLeaveDate,
                    EndLeaveDate = l.EndLeaveDate,
                    RequestTime = l.RequestTime,
                    ApprovalTime = l.ApprovalTime,
                    Reason = l.Reason,
                    UserName = username,
                    ApprovalStatus = GetApprovalStatus(l),
                };
            });

            return result;
        }

        private string GetApprovalStatus(Leave leave)
        {
            if (leave.IsApproved)
            {
                return "Approved";
            }
            else if (leave.IsPending)
            {
                return "Pending";
            }
            else if (leave.IsRejected)
            {
                return "Rejected";
            }
            else
            {
                return "Unknown";
            }
        }
        public async Task<LeaveViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                LeaveViewModel viewModel = new()
                {
                    Id = result.Id,

                    UserId = result.UserId,
                    LeaveType = result.LeaveType,
                    RequestTime = result.RequestTime,
                    ApprovalTime = DateTime.Now,
                    Reason = result.Reason,

                    ApprovalStatus = GetApprovalStatus(result),
                };
                return viewModel;
            }
        }

        public async Task<LeaveViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                LeaveViewModel viewModel = new()
                {
                    Id = result.Id,

                    UserId = result.UserId,
                    LeaveType = result.LeaveType,
                    RequestTime = result.RequestTime,
                    ApprovalTime = result.ApprovalTime,
                    Reason = result.Reason,

                };
                return viewModel;
            }
        }

        public Leave GetLast()
        {
            return _repository.GetLast();
        }

        public async Task<IEnumerable<LeaveViewModel>> GetLeaveByUserId(int userId)
        {
            var leaveData = await _repository.FindAll(l => l.UserId == userId);

            if (leaveData != null && leaveData.Any())
            {
                return leaveData.Select(l => new LeaveViewModel
                {
                    Id = l.Id,
                    UserId = l.UserId,

                    LeaveType = l.LeaveType,
                    StartLeaveDate = l.StartLeaveDate,
                    EndLeaveDate = l.EndLeaveDate,
                    RequestTime = l.RequestTime,
                    ApprovalTime = l.ApprovalTime,
                    Reason = l.Reason,
                    
                    ApprovalStatus = GetApprovalStatus(l),

                });
            }
            else
            {
                return Enumerable.Empty<LeaveViewModel>();
            }
        }


        public Task<bool> Insert(InsertLeave inserFood)
        {
            Leave order = new Leave()
            {
                UserId = inserFood.UserId,
                LeaveType = inserFood.LeaveType,

                Reason = inserFood.Reason,
                IsApproved = inserFood.IsApproved,


            };
            return _repository.Insert(order);
        }

        public async Task<bool> RejectLeave(int leaveId)
        {
            var leave = await _repository.GetById(leaveId);
            if (leave != null)
            {
                leave.IsApproved = false;
                leave.IsPending = false;
                leave.IsRejected = true;
                leave.ApprovalTime = DateTime.Now;
                await _repository.Update(leave);
                return true;
            }
            return false;
        }

        public async Task<bool> Update(UpdateLeave StudentUpdateModel)
        {
            Leave student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
                student.Id = StudentUpdateModel.Id;
                student.UserId = StudentUpdateModel.UserId;
                student.LeaveType = StudentUpdateModel.LeaveType;
                student.Reason = StudentUpdateModel.Reason;
                student.StartLeaveDate = StudentUpdateModel.StartLeaveDate;
                student.EndLeaveDate = StudentUpdateModel.EndLeaveDate;
                student.IsApproved = StudentUpdateModel.IsApproved;
                student.IsRejected = StudentUpdateModel.IsRejected;



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
