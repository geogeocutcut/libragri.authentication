using libragri.core.cqrs;

namespace libragri.authentication.model
{
    public class RefreshTokenData:IAggregateRoot<string>
    {
        public string Id { get; set; }

        public string ClientId { get; set; }

        public string Token { get; set; }
        public int IsStop { get; set; }

        public string UserName { get; set; }
    }
}