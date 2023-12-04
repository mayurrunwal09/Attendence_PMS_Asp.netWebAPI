using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using Repositroy_And_Services.context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntityClass
    {
        private readonly MainDBContext _context;
        private readonly DbSet<T> _entities;

        public Repository(MainDBContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<bool> Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Remove(entity);
            var result = await _context.SaveChangesAsync();
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<T> Find(Expression<Func<T, bool>> match)
        {
            return await _entities.FirstOrDefaultAsync(match);
        }

        public async Task<ICollection<T>> FindAll(Expression<Func<T, bool>> match)
        {
            return await _entities.Where(match).ToListAsync();
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

       

        public async Task<T> GetById(int id)
        {
            return await _entities.SingleOrDefaultAsync(e => e.Id == id);
          
        }

        public async Task<T> GetByName(string name)
        {
            return await _entities.FindAsync(name);
        }

        public T GetLast()
        {
            return _entities.LastOrDefault();
        }

        public async Task<bool> Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _entities.AddAsync(entity);
            var result = await _context.SaveChangesAsync();
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<bool> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Update(entity);
            var result = await _context.SaveChangesAsync();
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }
        public async Task<List<Attendence>> GetAttendanceDetails(int userId)
        {
            var attendanceDetails = await _context.Attenants
                .Where(a => a.UserId == userId)
                .ToListAsync();
            return attendanceDetails;
        }

    }
}
