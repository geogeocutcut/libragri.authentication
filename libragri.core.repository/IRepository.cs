﻿using libragri.core.cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.repository
{
    public interface IRepository<TId,TEntity> where TEntity : AggregateRoot<TId>
    {
        TEntity GetById(TId Id);
        void Upsert(TEntity entity);
        void Delete(TEntity entity);
        IList<TEntity> GetAll();
    }
}