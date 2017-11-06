using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public class EventDispatcherMiddleware : BusMiddleware<IList<IEvent>, IMessage<IList<IEvent>>>
    {
        IEventBus eventBus;

        public EventDispatcherMiddleware(IHandler next, IEventBus evtBus)
        {
            this.Next = next;
            this.eventBus = evtBus;
        }
        public override IList<IEvent> handle(IMessage<IList<IEvent>> cmd)
        {
            IList<IEvent> events =(IList<IEvent>)this.Next.handle(cmd);
            foreach(IEvent e in events)
            {
                eventBus.Dispatch(e);
            }
            return events;
        }
    }
}
