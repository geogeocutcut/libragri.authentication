using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.core.cqrs
{
    public interface IHandler
    {
        object handle(object obj);
    }
}
