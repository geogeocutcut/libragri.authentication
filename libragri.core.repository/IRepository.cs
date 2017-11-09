
using System;
using System.Collections.Generic;
using System.Text;
using libragri.core.common;

namespace libragri.core.repository
{
    public interface IRepository<TId,TEntity> where TEntity : Entity<TId>
    {
        TEntity GetById(TId Id);
        void Upsert(TEntity entity);
        void Delete(TEntity entity);
        IList<TEntity> GetAll();
        
        IList<TEntity> FindWhere(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
    }
}
