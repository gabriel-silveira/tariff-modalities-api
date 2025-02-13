using CTCEA_Tariff_Modalities.Models.Base.DTO;
using CTCEA_Tariff_Modalities.Models.Classificacao.DTO;
using CTCEA_Tariff_Modalities.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CTCEA_Tariff_Modalities.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassificationController : ControllerBase
    {
        private readonly ErrorResponseDTO error = new() { Message = "" };

        private readonly IClassificationService _classificationService;

        public ClassificationController(IClassificationService classificationService) {
            _classificationService = classificationService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ClassificationRequestDTO request)
        {
            if (request.IcaoLocalidade == "")
                return StatusCode(400, error.MissingField("ICAO da Localidade"));

            if (request.Identificacao == "")
                return StatusCode(400, error.MissingField("Identificação"));

            if (request.Origem == "")
                return StatusCode(400, error.MissingField("Origem"));

            if (request.Destino == "")
                return StatusCode(400, error.MissingField("Destino"));

            var response = await _classificationService.ClassifyFlight(request);

            if (response.Error) return BadRequest(response);

            return Ok(response);
        }
    }
}
