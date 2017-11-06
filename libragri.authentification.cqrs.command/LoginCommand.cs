using libragri.authentication.model;
using libragri.core.cqrs;
using System;

namespace libragri.authentification.cqrs.command
{
    public class LoginCommand: ICommand<UserModel>
    {
        public string Login { get; set; }
        public string PwdSHA1 { get; set; }

        public LoginCommand(string login , string pwdsha1)
        {
            Login = login;
            PwdSHA1 = pwdsha1;
        }
    }
}
