using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public abstract class EventHandler<TEvent> : IHandler
        where TEvent : class, IEvent

    {
        public abstract TEvent handle(TEvent eventtodo);

        public object handle(object evt)
        {
            return handle((TEvent)evt);
        }
    }
}
