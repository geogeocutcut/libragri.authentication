using System;
using System.Collections.Generic;

namespace libragri.core.cqrs
{
    public class CommandBus:ICommandBus
    {
        private IEventBus eventBus;
        private Dictionary<Type,IHandler> handlers = new Dictionary<Type, IHandler>();

        public CommandBus(IEventBus evtBus)
        {
            this.eventBus = evtBus;
        }

        public void Subscribe(Type t, IHandler h)
        {

            if (!handlers.TryGetValue(t, out var handler))
            {
                handlers.Add(t, new EventDispatcherMiddleware(h, this.eventBus));
            }
        }
        public R Dispatch<R,C>(C commandtodo) where C : ICommand<R>
        {
            return (R)handlers[typeof(C)].handle(commandtodo);
        }

    }
}