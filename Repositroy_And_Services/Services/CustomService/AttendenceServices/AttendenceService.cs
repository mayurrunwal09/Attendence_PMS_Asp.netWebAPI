using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.AttendenceServices
{
    public class AttendenceService : IAttendenceService
    {
        private readonly IRepository<Attendence> _repository;
        public AttendenceService(IRepository<Attendence> repository)
        {
            _repository = repository;
        }

       

        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                Attendence student = await _repository.GetById(id);
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

        public Task<Attendence> Find(Expression<Func<Attendence, bool>> match)
        {
            return _repository.Find(match);
        }

        public async Task<ICollection<AttendenceViewModel>> GetAll()
        {
            ICollection<AttendenceViewModel> orderViewModels = new List<AttendenceViewModel>();
            ICollection<Attendence> orders = await _repository.GetAll();
            foreach (Attendence order in orders)
            {
                AttendenceViewModel viewModel = new()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    
                    CheckInTime = order.CheckInTime,
                 


                };
                orderViewModels.Add(viewModel);
            }
            return orderViewModels;
        }

       

        public async Task<AttendenceViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                AttendenceViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserId = result.UserId,
                    CheckInTime = result.CheckInTime,
                 

                };
                return viewModel;
            }
        }

        public async Task<AttendenceViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                AttendenceViewModel viewModel = new()
                {
                    Id = result.Id,
                    UserId = result.UserId,
                    CheckInTime = result.CheckInTime,
                   

                };
                return viewModel;
            }
        }

        public Attendence GetLast()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(InsertAttendence inserFood)
        {
            Attendence order = new Attendence()
            {
                UserId = inserFood.UserId,
                CheckInTime =DateTime.Now,
             

            };
            return _repository.Insert(order);
        }

        public async Task<bool> Update(UpdateAttendence StudentUpdateModel)
        {
            Attendence student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
                student.Id = StudentUpdateModel.Id;
                student.UserId = StudentUpdateModel.UserId;
                student.CheckInTime = StudentUpdateModel.CheckInTime;
               
             


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
