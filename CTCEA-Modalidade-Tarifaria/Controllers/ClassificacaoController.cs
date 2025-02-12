using CTCEA_Modalidade_Tarifaria.Models.Base.DTO;
using CTCEA_Modalidade_Tarifaria.Models.Classificacao.DTO;
using CTCEA_Modalidade_Tarifaria.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CTCEA_Modalidade_Tarifaria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassificacaoController : ControllerBase
    {
        private ErrorResponseDTO error = new ErrorResponseDTO { Message = "" };

        private readonly IClassificacaoService _classificacaoService;

        public ClassificacaoController(IClassificacaoService classificacaoService) {
            _classificacaoService = classificacaoService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ClassificacaoRequestDTO request)
        {
            if (request.IcaoLocalidade == "")
                return StatusCode(400, error.MissingField("ICAO da Localidade"));

            if (request.Identificacao == "")
                return StatusCode(400, error.MissingField("Identificação"));

            if (request.Origem == "")
                return StatusCode(400, error.MissingField("Origem"));

            if (request.Destino == "")
                return StatusCode(400, error.MissingField("Destino"));

            var response = await _classificacaoService.ClassificarVoo(request);

            if (response.Error) return BadRequest(response);

            return Ok(response);
        }
    }
}
