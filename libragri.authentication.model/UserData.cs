
using System;
using libragri.core.common;

namespace libragri.authentication.model
{
    public class UserData : Entity<string>
    {
        public string Id { 
            get{
                if(string.IsNullOrEmpty(_id))
                {
                    _id=Guid.NewGuid().ToString();
                }
                return _id;
            }
            set{
                _id = value;
            }
        }
        public string Login { get; set; }
        public string PwdSHA1 { get; set; }
        public string Email { get; set; }

        public override string GetId()
        {
            return Id;
        }

    }
}
