using libragri.core.cqrs;
using System;

namespace libragri.authentication.model
{
    public class UserData : IAggregateRoot<string>
    {
        public string Id { get ; set ; }
        public virtual string UserName { get; set; }
        public virtual string PwdSHA1 { get; set; }
        public virtual string Email { get; set; }
    }
}
