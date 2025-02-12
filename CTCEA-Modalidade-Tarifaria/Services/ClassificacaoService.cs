using CTCEA_Modalidade_Tarifaria.Models.Base.DTO;
using CTCEA_Modalidade_Tarifaria.Models.Classificacao.DTO;
using CTCEA_Modalidade_Tarifaria.Services.Interfaces;
using System.Text.RegularExpressions;

namespace CTCEA_Modalidade_Tarifaria.Services
{
    public class ClassificacaoService : IClassificacaoService
    {
        private ICobrancaAVistaService _cobrancaAVistaService;
        private ICompanhiaAereaSGTANService _companhiaAereaSGTANService;

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

        public ClassificacaoService(
            ICobrancaAVistaService cobrancaAVistaService,
            ICompanhiaAereaSGTANService companhiaAereaSGTANService
        )
        {
            _cobrancaAVistaService = cobrancaAVistaService;
            _companhiaAereaSGTANService = companhiaAereaSGTANService;
        }

        public async Task<ResponseBaseDTO<ClassificacaoResponseDTO>> ClassificarVoo(ClassificacaoRequestDTO request)
        {
            IcaoLocalidade = request.IcaoLocalidade;
            Identificacao = request.Identificacao;
            Origem = request.Origem;
            Destino = request.Destino;
            DataDecolagem = request.DataDecolagem;

            var response = new ResponseBaseDTO<ClassificacaoResponseDTO>()
            {
                Result = new ClassificacaoResponseDTO()
                {
                    Grupo = "",
                    Natureza = DOMESTICO,
                    TipoCobranca = ""
                }
            };

            response.Result.Grupo = CallsignValido() ? GRUPO_1 : GRUPO_2;

            string siglaIcao = GetSiglaICAO();

            if (response.Result.Grupo == GRUPO_1)
            {
                var empresaVigenteCobrancaAVista = _cobrancaAVistaService.EmpresaVigente(
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
                    var pertenceAoSGTAN = await _companhiaAereaSGTANService.ObterCompanhia(siglaIcao);

                    response.Result.TipoCobranca = !pertenceAoSGTAN ? A_VISTA : A_POSTERIORI;
                }
            } else if (response.Result.Grupo == GRUPO_2)
            {
                if (MatriculaEstrangeira(siglaIcao))
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
                if (temAerodromosInternacionais(Origem, Destino))
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

        public string GetSiglaICAO()
        {
            return Identificacao.Length < 4 ? Identificacao : Identificacao.Substring(0, 3);
        }

        public bool CallsignValido()
        {
            if (Identificacao.Length < 4) return false;

            return Regex.IsMatch(Identificacao.Substring(0, 3), @"^[a-zA-Z]+$")
                && Regex.IsMatch(Identificacao.Substring(3, Identificacao.Length - 3), @"^[0-9]+$");
        }

        public bool MatriculaEstrangeira(string siglaIcao)
        {
            var cc = siglaIcao.Substring(0, 2);

            return cc != "PT" && cc != "PP" && cc != "PR" && cc != "PU" && cc != "PS";
        }

        public bool temAerodromosInternacionais(string origem, string destino)
        {
            var or = origem.Substring(0, 2);
            var o = origem.Substring(0, 1);
            var de = destino.Substring(0, 2);
            var d = destino.Substring(0, 1);

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
