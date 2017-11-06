using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public abstract class BusMiddleware<T,M>:IHandler
    {
        public IHandler Next { get; set; }
        public abstract T handle(M message);

        public object handle(object cmd)
        {
            return handle((M)cmd);
        }
    }
}
