using System;
using System.Text.RegularExpressions;
using libragri.authentication.model;
using libragri.core.specification;

namespace libragri.authentication.specification
{
    public class LoginSpecification : CompositeSpecification<UserData>
    {
        static Regex rx = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public LoginSpecification()
        {  
        }

        public override bool IsSatisfiedBy(UserData o)
        {
            return rx.IsMatch(o.Email);
        }
    }
}
