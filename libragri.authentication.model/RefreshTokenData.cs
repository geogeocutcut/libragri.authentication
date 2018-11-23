

using libragri.core.common;

namespace libragri.authentication.model
{
    public class RefreshTokenData:Entity<string>
    {
        
        public string ClientId { get; set; }

        public string Token { get; set; }
        public int IsStop { get; set; }

        public string Login { get; set; }
        public string UserId { get; set; }
    }
}