
namespace GBChallenge.Core.Domain.Entities.ClientResponses
{
    public class ObterAcumuladoClientResponse
    {
        public ResponseBody Body { get; set; }
    }

    public class ResponseBody
    {
        public int Credit { get; set; }
    }
}
