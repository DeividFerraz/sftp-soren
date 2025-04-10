using Microsoft.AspNetCore.Mvc;
using Soren.Custom.Domain.DTOs.Response;
using Soren.Custom.Service.Interfaces;

namespace Sorem.Custom.SFTP.Controllers
{
    /// <summary>
    /// Funções relacionadas a clientes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PullPaymentesController : ControllerBase
    {
        private readonly IServiceCustumer _iServiceCustumer;

        public PullPaymentesController(IServiceCustumer cargaService)
        {
            _iServiceCustumer = cargaService;
        }

        /// <summary>
        /// Consulta o cliente pelo CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpGet("{cpf}")]
        [ProducesResponseType(typeof(ResponseCostumer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute(Name = "cpf")] string cpf)
        {
            var client = await _iServiceCustumer.GetByAsyncCPF(cpf);
            if (client is null)
            {
                return NotFound();
            }

            return Ok(client);
        }
    }
}
