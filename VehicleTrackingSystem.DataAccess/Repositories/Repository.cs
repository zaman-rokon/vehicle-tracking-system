using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using VehicleTrackingSystem.DataAccess.DbContext;

namespace VehicleTrackingSystem.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _applicationDBContext;

        public Repository(ApplicationDbContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }


        public TEntity Get(string id)
        {
            return _applicationDBContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _applicationDBContext.Set<TEntity>().ToList();
        }


        public void Add(TEntity entity)
        {
            _applicationDBContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _applicationDBContext.Set<TEntity>().AddRange(entities);
        }


        public void Remove(string id)
        {
            _applicationDBContext.Set<TEntity>().Remove(Get(id));
        }

        public void RemoveQuery(Expression<Func<TEntity, bool>> expression)
        {
            _applicationDBContext.RemoveRange(expression);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _applicationDBContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
