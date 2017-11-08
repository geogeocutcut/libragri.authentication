using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public interface IAggregateRoot<TId>
    {
        TId Id { get; set; }
        
    }
}
