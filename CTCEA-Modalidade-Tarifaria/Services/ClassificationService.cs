using CTCEA_Tariff_Modalities.Models.Base.DTO;
using CTCEA_Tariff_Modalities.Models.Classificacao.DTO;
using CTCEA_Tariff_Modalities.Services.Interfaces;
using System.Text.RegularExpressions;

namespace CTCEA_Tariff_Modalities.Services
{
    public class ClassificationService : IClassificationService
    {
        private IImmediateBillingService _cobrancaAVistaService;
        private readonly IFlightCompanySGTANService _flightCompanySGTANService;

        private const string A_VISTA = "À Vista";
        private const string A_POSTERIORI = "A Posteriori";
        private const string DOMESTICO = "Doméstico";
        private const string INTERNACIONAL = "Internacional";
        public const string GRUPO_1 = "Grupo I";
        public const string GRUPO_2 = "Grupo II";

        public string IcaoLocalidade = "";
        public string Identificacao = "";
        public string Origem = "";
        public string Destino = "";
        public DateTime DataDecolagem;

        public ClassificationService(
            IImmediateBillingService cobrancaAVistaService,
            IFlightCompanySGTANService companhiaAereaSGTANService
        )
        {
            _cobrancaAVistaService = cobrancaAVistaService;
            _flightCompanySGTANService = companhiaAereaSGTANService;
        }

        public async Task<ResponseBaseDTO<ClassificationResponseDTO>> ClassifyFlight(ClassificationRequestDTO request)
        {
            IcaoLocalidade = request.IcaoLocalidade;
            Identificacao = request.Identificacao;
            Origem = request.Origem;
            Destino = request.Destino;
            DataDecolagem = request.DataDecolagem;

            var response = new ResponseBaseDTO<ClassificationResponseDTO>()
            {
                Result = new ClassificationResponseDTO()
                {
                    Grupo = "",
                    Natureza = DOMESTICO,
                    TipoCobranca = ""
                }
            };

            response.Result.Grupo = ValidCallsign() ? GRUPO_1 : GRUPO_2;

            string siglaIcao = GetICAOAirpotCode();

            if (response.Result.Grupo == GRUPO_1)
            {
                var empresaVigenteCobrancaAVista = _cobrancaAVistaService.CompanyInForce(
                    siglaIcao,
                    request.IcaoLocalidade,
                    request.DataDecolagem
                );

                if (empresaVigenteCobrancaAVista.Cadastrada)
                {
                    response.Result.TipoCobranca = A_VISTA;
                    response.Result.Natureza = empresaVigenteCobrancaAVista.Nacionalidade == "E" ? INTERNACIONAL : DOMESTICO;
                }
                else
                {
                    var belongsToSGTAN = await _flightCompanySGTANService.GetCompany(siglaIcao);

                    response.Result.TipoCobranca = !belongsToSGTAN ? A_VISTA : A_POSTERIORI;
                }
            } else if (response.Result.Grupo == GRUPO_2)
            {
                if (ForeignRegistration(siglaIcao))
                {
                    response.Result.Natureza = INTERNACIONAL;

                    response.Result.TipoCobranca = A_VISTA;
                } else
                {
                    response.Result.TipoCobranca = A_POSTERIORI;
                }
            }

            if (response.Result.TipoCobranca == A_VISTA)
            {
                if (HasInternationalAirfields(Origem, Destino))
                {
                    response.Result.Natureza = INTERNACIONAL;
                }
            }

            if (response.Result.TipoCobranca == A_POSTERIORI)
            {
                response.Result.Natureza = "";
            }

            return response;
        }

        public string GetICAOAirpotCode()
        {
            return Identificacao.Length < 4 ? Identificacao : Identificacao.Substring(0, 3);
        }

        public bool ValidCallsign()
        {
            if (Identificacao.Length < 4) return false;

            return Regex.IsMatch(Identificacao.Substring(0, 3), @"^[a-zA-Z]+$")
                && Regex.IsMatch(Identificacao.Substring(3, Identificacao.Length - 3), @"^[0-9]+$");
        }

        public bool ForeignRegistration(string icaoAirpotCode)
        {
            var cc = icaoAirpotCode.Substring(0, 2);

            return cc != "PT" && cc != "PP" && cc != "PR" && cc != "PU" && cc != "PS";
        }

        public bool HasInternationalAirfields(string origin, string destination)
        {
            var or = origin.Substring(0, 2);
            var o = origin.Substring(0, 1);
            var de = destination.Substring(0, 2);
            var d = destination.Substring(0, 1);

            if (
                or != "SB"
                && or != "SD"
                && or != "SL"
                && or != "SJ"
                && or != "SN"
                && or != "SS"
                && or != "SW"
                && o != "0"
                && o != "9"
            ) return true;

            if (
                de != "SB"
                && de != "SD"
                && de != "SL"
                && de != "SJ"
                && de != "SN"
                && de != "SS"
                && de != "SW"
                && d != "0"
                && d != "9"
            ) return true;

            return false;
        }
    }
}
