using CTCEA_Tariff_Modalities.Models.Base.DTO;
using CTCEA_Tariff_Modalities.Models.Calculator.DTO;
using CTCEA_Tariff_Modalities.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CTCEA_Tariff_Modalities.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : Controller
    {
        private ErrorResponseDTO error = new ErrorResponseDTO { Message = "" };

        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculadoraService)
        {
            _calculatorService = calculadoraService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CalculatorRequestDTO request)
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

            var response = await _calculatorService.CalcularTarifas(request);

            if (response.Result.ERROR != "")
                return StatusCode(400, error.Set(response.Result.ERROR));

            if (response.Error) return BadRequest(response);

            return Ok(response);
        }
    }
}
