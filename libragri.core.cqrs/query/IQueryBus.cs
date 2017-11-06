using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public interface IQueryBus
    {
        R Dispatch<R, C>(C querytodo) where C : IQuery<R>;
        void Subscribe(Type t, IHandler h);
    }
}
