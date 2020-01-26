using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Settings
{
    public class GBChallengeSettings
    {
        public TokenSettings TokenSettings { get; set; }
        public IEnumerable<ExternalClientSettings> ExternalClients { get; set; }
    }
}
