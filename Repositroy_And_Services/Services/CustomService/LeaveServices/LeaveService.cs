/*using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
                RequestTime = DateTime.Now,
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
                   IsApproved = order.IsApproved,

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
                RequestTime = l.RequestTime,
                ApprovalTime = l.ApprovalTime,
                Reason = l.Reason,
                IsApproved = l.IsApproved,
                IsPending = l.IsPending,
                IsRejected = l.IsRejected,
                
            });
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
                    IsApproved = result.IsApproved,
                };
                return viewModel;
            }
        }

        public async Task<LeaveViewModel> GetByName(string name)
        {
            var result = await  _repository.GetByName(name);
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
                    IsApproved = result.IsApproved,
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
            var leaveData = await _repository.Find(l => l.UserId == userId);

            if (leaveData != null)
            {
                return leaveData.Select(l => new LeaveViewModel
                {
                    Id = l.Id,
                    UserId = l.UserId,
                    LeaveType = l.LeaveType,
                    RequestTime = l.RequestTime,
                    ApprovalTime = l.ApprovalTime,
                    Reason = l.Reason,
                    IsApproved = l.IsApproved,
                    IsPending = l.IsPending,
                    IsRejected = l.IsRejected,
                });
            }
            else
            {
                return Enumerable.Empty<LeaveViewModel>(); // or null, depending on your preference
            }
        }


        public Task<bool> Insert(InsertLeave inserFood)
        {
            Leave order = new Leave()
            {
              UserId = inserFood.UserId,
              LeaveType = inserFood.LeaveType,
                RequestTime = inserFood.RequestTime,
                
                ApprovalTime = inserFood.ApprovalTime,
                Reason = inserFood.Reason,
                IsApproved=inserFood.IsApproved,
                

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
                student.RequestTime = StudentUpdateModel.RequestTime;
                student.ApprovalTime = StudentUpdateModel.ApprovalTime;
              

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
                RequestTime = DateTime.Now,
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
                RequestTime = inserFood.RequestTime,

                ApprovalTime = inserFood.ApprovalTime,
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
                student.RequestTime = StudentUpdateModel.RequestTime;
                student.ApprovalTime = StudentUpdateModel.ApprovalTime;


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
