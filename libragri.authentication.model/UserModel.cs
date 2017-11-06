using libragri.core.cqrs;
using System;

namespace libragri.authentication.model
{
    public class UserModel : AggregateRoot<string>
    {
        public virtual string Login { get; set; }
        public virtual string PwdSHA1 { get; set; }
        public virtual string Email { get; set; }
        
    }
}
