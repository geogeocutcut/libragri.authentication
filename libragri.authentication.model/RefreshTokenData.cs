

using libragri.core.common;

namespace libragri.authentication.model
{
    public class RefreshTokenData<TId>:Entity<TId>
    {

        public string ClientId { get; set; }

        public string Token { get; set; }
        public int IsStop { get; set; }

        public string UserName { get; set; }
    }
}