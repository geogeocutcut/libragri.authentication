using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public abstract class Event<TId,TRoot> :IEvent where TRoot : AggregateRoot<TId>
    {
        public TId AggregateId { get; set; }
        public Type AggregateType { get; set; }
        public DateTime eventDateTime = DateTime.Now;

        public Event(TRoot entity)
        {
            this.AggregateId = entity.Id;
            this.AggregateType = entity.GetType();
        }
    }
}
