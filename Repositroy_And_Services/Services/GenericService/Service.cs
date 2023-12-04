using Domain.Models;
using Repositroy_And_Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.GenericService
{
    public class Service<T> : IService<T> where T : BaseEntityClass
    {
        #region Private Variables And Controller
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        #endregion

        #region GetAll
        public Task<ICollection<T>> GetAll()
        {
            return _repository.GetAll();
        }
        #endregion

        #region GetById
        public Task<T> GetById(int id)
        {
            return _repository.GetById(id);
        }
        #endregion

        #region GetByName
        public Task<T> GetByName(string name)
        {
            return _repository.GetByName(name);
        }
        #endregion

        #region GetLast
        public T GetLast()
        {
            return _repository.GetLast();
        }
        #endregion

        #region Insert
        public Task<bool> Insert(T entity)
        {
            return _repository.Insert(entity);
        }
        #endregion

        #region Update
        public Task<bool> Update(T entity)
        {
            return _repository.Update(entity);
        }
        #endregion

        #region Delete
        public Task<bool> Delete(T entity)
        {
            return _repository.Delete(entity);
        }
        #endregion

        #region Find
        public Task<T> Find(Expression<Func<T, bool>> match)
        {
            return _repository.Find(match);
        }
        #endregion

        #region FindAll
        public Task<ICollection<T>> FindAll(Expression<Func<T, bool>> match)
        {
            return _repository.FindAll(match);
        }
        #endregion

    }
}
