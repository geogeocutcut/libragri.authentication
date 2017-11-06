using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace libragri.core.cqrs
{
    public abstract class QueryHandler<TData, TQuery> : IHandler 
        where TQuery : IQuery<TData>

    {

        public abstract TData handle(TQuery queryToDo);

        public object handle(object obj)
        {
            return handle((TQuery)obj);
        }
    }
}
