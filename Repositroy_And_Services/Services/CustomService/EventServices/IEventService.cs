using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.EventServices
{
    public  interface IEventService
    {
        Task<ICollection<EventViewModel>> GetAll();
        Task<EventViewModel> GetById(int id);
        Task<EventViewModel> GetByName(string name);
        Event GetLast();
        Task<bool> Insert(InsertEvent inserFood);
        Task<bool> Update(UpdateEvent StudentUpdateModel);
        Task<bool> Delete(int id);
        Task<Event> Find(Expression<Func<Event, bool>> match);
    }
}
