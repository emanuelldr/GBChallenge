using GBChallenge.Core.Domain.Entities.ClientResponses;
using GBChallenge.Core.Domain.Entities.Settings;
using GBChallenge.Core.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using GBChallenge.Core.Domain.SimpleTypes;

namespace GBChallenge.Infrastructure.CrossCutting.ExternalClients
{
    class CashBackClient : ICashBackClient
    {
        private readonly IEnumerable<ExternalClientSettings> _externalClientsSettings;
        private readonly ILogger<CashBackClient> _logger;

        public CashBackClient(IOptions<GBChallengeSettings> gbChallengeSettings, ILogger<CashBackClient> logger)
        {
            _externalClientsSettings = gbChallengeSettings.Value.ExternalClients;
            _logger = logger;
        }

        public async Task<ObterAcumuladoClientResponse> ObterAcumulado(string cpfRevendedor)
        {
            try
            {
                var configuracao = _externalClientsSettings.First(c => c.NomeServico == ExternalServices.ObterAcumuladoCashBack);

                string queryString;
                using (var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "cpf", cpfRevendedor },
                }))
                {
                    queryString = await content.ReadAsStringAsync();
                }

                using (var cancellationToken = new CancellationTokenSource(configuracao.TimeoutInMs))
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Accept.Clear();

                        using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get,
                            configuracao.BaseUrl + configuracao.RequestUri + "?" + queryString))
                        {
                            if (configuracao.Headers != null)
                            {
                                foreach (Header header in configuracao.Headers)
                                    requestMessage.Headers.Add(header.Chave, header.Valor);
                            }

                            requestMessage.Content = (HttpContent)new StringContent(string.Empty);
                            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            using (HttpResponseMessage response = await httpClient.SendAsync(requestMessage, cancellationToken.Token))
                            {
                                response.EnsureSuccessStatusCode();
                                var responseMessage = await response.Content.ReadAsStringAsync();
                                return JsonConvert.DeserializeObject<ObterAcumuladoClientResponse>(responseMessage);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao Obter Acumulado ", ex);
                throw ex;
            }
        }
    }
}

