using GBChallenge.Core.Domain.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GBChallenge.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected ActionResult TratarRetorno<T>(T response, string methodName)
        {
            BaseResponse baseResponse =  response as BaseResponse;
            
            switch (baseResponse.CodigoRetorno)
            {
                case 200:
                    return Ok(response);
                case 201:
                    return CreatedAtAction(methodName, response);
                case 400:
                    _logger.LogInformation($"Erro em {methodName} - " + baseResponse.Messagem);
                    return BadRequest(baseResponse.Messagem);
                case 404:
                    _logger.LogInformation($"Erro em {methodName} - " + baseResponse.Messagem);
                    return NotFound(baseResponse.Messagem);

                default:
                    if (!baseResponse.Successo)
                    {
                        _logger.LogInformation($"Erro em {methodName} - " + baseResponse.Messagem);
                        return BadRequest(baseResponse.Messagem);
                    }                      
                    else return Ok(response);

            }
        }
    }
}
