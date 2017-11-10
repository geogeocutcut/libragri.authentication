using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public interface IEventHandler<TEvent> : IHandler
        where TEvent : class, IEvent

    {
        TEvent handle(TEvent eventtodo);
    }
}
