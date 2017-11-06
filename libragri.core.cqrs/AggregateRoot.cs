using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public abstract class AggregateRoot<TId>
    {
        public TId Id { get; set; }
        
    }
}
