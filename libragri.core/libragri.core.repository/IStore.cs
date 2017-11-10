
using System;
using System.Collections.Generic;
using System.Text;
using libragri.core.common;

namespace libragri.core.repository
{
    public interface IStore <TId>
    {
        TEntity FindById<TEntity>(TId id) where TEntity : Entity<TId>;
        void Upsert<TEntity>(TEntity entity) where TEntity : Entity<TId>;
        void Remove<TEntity>(TEntity entity) where TEntity : Entity<TId>;
        IList<TEntity> FindAll<TEntity>() where TEntity : Entity<TId>;
        IList<TEntity> FindWhere<TEntity>(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) where TEntity : Entity<TId>;

    }
}
