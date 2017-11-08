using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public class EventDispatcherMiddleware : IBusMiddleware<IList<IEvent>, IMessage>
    {
        IEventBus eventBus;
        public IHandler Next { get; set; }
        
        public EventDispatcherMiddleware(IHandler next, IEventBus evtBus)
        {
            this.Next = next;
            this.eventBus = evtBus;
        }

         
        public IList<IEvent> handle(IMessage cmd)
        {
            IList<IEvent> events =(IList<IEvent>)this.Next.handle(cmd);
            foreach(IEvent e in events)
            {
                eventBus.Dispatch(e);
            }
            return events;
        }

        IList<IEvent> IBusMiddleware<IList<IEvent>, IMessage>.handle(IMessage message)
        {
            throw new NotImplementedException();
        }

        object IHandler.handle(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
