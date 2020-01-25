using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBChallenge.Core.Domain.Entities.Settings
{
    public class GBChallengeSettings
    {
        public TokenSettings TokenSettings { get; set; }
        public IEnumerable<ExternalClientSettings> ExternalClients { get; set; }
    }
}
