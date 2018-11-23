
using libragri.core.common;

namespace libragri.authentication.model
{
    public class UserData : Entity<string>
    {
        public virtual string Login { get; set; }
        public virtual string PwdSHA1 { get; set; }
        public virtual string Email { get; set; }
    }
}
