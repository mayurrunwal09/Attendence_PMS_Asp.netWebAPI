using Domain.Models;
using Domain.ViewModels;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.EventServices
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _repository;
        public EventService(IRepository<Event> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Delete(int id)
        {
            if (id != null)
            {
                Event student = await _repository.GetById(id);
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

        public Task<Event> Find(Expression<Func<Event, bool>> match)
        {
            return _repository.Find(match);
        }

        public async Task<ICollection<EventViewModel>> GetAll()
        {
            ICollection<EventViewModel> orderViewModels = new List<EventViewModel>();
            ICollection<Event> orders = await _repository.GetAll();
            foreach (Event order in orders)
            {
                EventViewModel viewModel = new()
                {
                    Id = order.Id,
                    EventName = order.EventName,
                    DateOfEvent = order.DateOfEvent,


                };
                orderViewModels.Add(viewModel);
            }
            return orderViewModels;
        }

        public async Task<EventViewModel> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                EventViewModel viewModel = new()
                {
                    Id = result.Id,
                    EventName = result.EventName,
                    DateOfEvent = result.DateOfEvent,
                };
                return viewModel;
            }
        }

        public async Task<EventViewModel> GetByName(string name)
        {
            var result = await _repository.GetByName(name);
            if (result == null)
            {
                return null;
            }
            else
            {
                EventViewModel viewModel = new()
                {
                    Id = result.Id,
                    EventName = result.EventName,
                    DateOfEvent = result.DateOfEvent,
                };
                return viewModel;
            }
        }

        public Event GetLast()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(InsertEvent inserFood)
        {
            Event order = new Event()
            {
                EventName = inserFood.EventName,
                DateOfEvent = inserFood.DateOfEvent,
            };
            return _repository.Insert(order);
        }

        public async Task<bool> Update(UpdateEvent StudentUpdateModel)
        {
            Event student = await _repository.GetById(StudentUpdateModel.Id);
            if (student != null)
            {
                student.Id = StudentUpdateModel.Id;
                student.EventName = StudentUpdateModel.EventName;
                StudentUpdateModel.DateOfEvent = StudentUpdateModel.DateOfEvent;

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
