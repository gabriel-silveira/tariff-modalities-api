using CTCEA_Modalidade_Tarifaria.Models.Base.DTO;
using CTCEA_Modalidade_Tarifaria.Models.Calculadora.DTO;
using CTCEA_Modalidade_Tarifaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CTCEA_Modalidade_Tarifaria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculadoraController : Controller
    {
        private ErrorResponseDTO error = new ErrorResponseDTO { Message = "" };

        private readonly ICalculadoraService _calculadoraService;

        public CalculadoraController(ICalculadoraService calculadoraService)
        {
            _calculadoraService = calculadoraService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CalculadoraRequestDTO request)
        {
            if (request.Grupo == "")
                return StatusCode(400, error.MissingField("Grupo"));

            if (request.Grupo != "Grupo I" && request.Grupo != "Grupo II")
                return StatusCode(400, error.Set("Grupo inválido"));

            if (request.Natureza == "")
                return StatusCode(400, error.MissingField("Natureza"));

            if (request.Origem == "")
                return StatusCode(400, error.MissingField("Origem"));

            if (request.Destino == "")
                return StatusCode(400, error.MissingField("Destino"));

            if (request.PMD == 0)
                return StatusCode(400, error.MissingField("PMD"));

            var response = await _calculadoraService.CalcularTarifas(request);

            if (response.Result.ERROR != "")
                return StatusCode(400, error.Set(response.Result.ERROR));

            if (response.Error) return BadRequest(response);

            return Ok(response);
        }
    }
}
