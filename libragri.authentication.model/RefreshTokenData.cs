

using System;
using libragri.core.common;

namespace libragri.authentication.model
{
    public class RefreshTokenData:Entity<string>
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

        public string ClientId { get; set; }

        public string Token { get; set; }
        public int IsStop { get; set; }

        public string Login { get; set; }
        public string UserId { get; set; }

        public override string GetId()
        {
            return Id;
        }
    }
}