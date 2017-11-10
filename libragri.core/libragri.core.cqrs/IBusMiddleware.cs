using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public interface IBusMiddleware<T,M>:IHandler
    {
        IHandler Next { get; set; }
        T handle(M message);
    }
}
