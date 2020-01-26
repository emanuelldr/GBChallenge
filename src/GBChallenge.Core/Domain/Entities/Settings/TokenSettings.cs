
namespace GBChallenge.Core.Domain.Entities.Settings
{
    public class TokenSettings
    {
        public string ChaveAPI { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
        public int ExpiracaoMinutos { get; set; }
    }
}
