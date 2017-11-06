using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace libragri.core.cqrs
{
    public class EventBus: IEventBus
    {
        private Dictionary<Type, IEnumerable<IHandler>> handlers = new Dictionary<Type, IEnumerable<IHandler>>();

        public void Subscribe(Type t, IHandler h)
        {
            if(!handlers.TryGetValue(t, out var handlerList))
            {
                handlers.Add(t, new [] { h });
            }
            else
            {
                handlerList=handlerList.Concat(new []{ h});
                handlers[t] = handlerList;
            }
        }

        public void Dispatch<E>(E eventtodo) where E : IEvent
        {
            var handlersToExecute = handlers[eventtodo.GetType()];
            foreach(IHandler h in handlersToExecute)
            {
                h.handle(eventtodo);
            }
        }
    }
}
