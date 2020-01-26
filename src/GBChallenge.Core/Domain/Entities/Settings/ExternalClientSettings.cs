using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Settings
{
    public class ExternalClientSettings
    {
        public string NomeServico { get; set; }
        public string BaseUrl { get; set; }
        public string RequestUri { get; set; }
        public int TimeoutInMs { get; set; }
        public IEnumerable<Header> Headers { get; set; }
    }

    public class Header 
    {
        public string Chave { get; set; }
        public string Valor { get; set; }
    }
}
