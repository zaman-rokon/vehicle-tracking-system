using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VehicleTrackingSystem.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(string id);


        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(string id);
        void RemoveQuery(Expression<Func<TEntity, bool>> predicate);
        void RemoveRange(IEnumerable<TEntity> entities);

    }
}
