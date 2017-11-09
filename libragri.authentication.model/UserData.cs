
using libragri.core.common;

namespace libragri.authentication.model
{
    public class UserData<TId> : Entity<TId>
    {
        public virtual string UserName { get; set; }
        public virtual string PwdSHA1 { get; set; }
        public virtual string Email { get; set; }
    }
}
