using System.Text.Json;
using System.Text;
using HtmlAgilityPack;

using CTCEA_Modalidade_Tarifaria.Services.Interfaces;
using CTCEA_Modalidade_Tarifaria.Models.Calculadora.DTO;
using CTCEA_Modalidade_Tarifaria.Models.Base.DTO;

namespace CTCEA_Modalidade_Tarifaria.Services
{
    public class CalculadoraService : ICalculadoraService
    {
        private readonly string _group1Url = "https://tarifas.decea.mil.br/Simuladortarifas/CalculoGrupoI/Calculo";

        private readonly string _group2Url = "https://tarifas.decea.mil.br/Simuladortarifas/CalculoGrupoII/Calcular";

        private CalculadoraResponseDTO ResultData = new CalculadoraResponseDTO()
        {
            CotacaoDolar = "",
            DataCotacaoDolar = "",
            TAN = "",
            TATADR = "",
            TATAPP = "",
            TOTAL = "",
            TAN_DOLAR = "",
            TATADR_DOLAR = "",
            TATAPP_DOLAR = "",
            TOTAL_DOLAR = "",
            ERROR = ""
        };

        public async Task<ResponseBaseDTO<CalculadoraResponseDTO>> CalcularTarifas(CalculadoraRequestDTO request)
        {
            HttpClient client = new HttpClient();

            string url = getGroupUrl(request.Grupo);

            client.BaseAddress = new Uri(url);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    empresa = request.Natureza,
                    aerodecolagem = request.Origem,
                    aeropouso = request.Destino,
                    pmd = request.PMD,
                }),
                Encoding.UTF8,
                "application/json"
            );

            string queryString = getQueryString(request);

            var result = await client.PostAsync(queryString, jsonContent);

            var content = await result.Content.ReadAsStringAsync();

            return new ResponseBaseDTO<CalculadoraResponseDTO>() { Result = parseResults(content) };
        }

        private string getGroupUrl(string group)
        {
            return group == ClassificacaoService.GRUPO_1 ? _group1Url : _group2Url;
        }

        private string getQueryString(CalculadoraRequestDTO request)
        {
            string query = "";

            if (request.Grupo == ClassificacaoService.GRUPO_1)
            {
                query += "?empresa=" + request.Natureza;
            }
            else
            {
                query += "?aeronave=" + request.Natureza;
            }

            query += "&aerodecolagem=" + request.Origem;
            query += "&aeropouso=" + request.Destino;
            query += "&pmd=" + request.PMD;

            return query;
        }

        private CalculadoraResponseDTO parseResults(string results)
        {
            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(results);

            var tanDolar = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[3]/ul/li[1]/span/b");

            if (tanDolar != null)
            {
                ResultData.CotacaoDolar = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[1]/ul/li[2]/span/b").InnerHtml;
                ResultData.DataCotacaoDolar = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[1]/ul/li[1]/span/b").InnerHtml;

                ResultData.TAN = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[2]/ul/li[1]/span/b").InnerHtml;
                ResultData.TATAPP = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[2]/ul/li[2]/span/b").InnerHtml;
                ResultData.TATADR = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[2]/ul/li[3]/span/b").InnerHtml;
                ResultData.TOTAL = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[2]/ul/li[4]/span/b").InnerHtml;

                ResultData.TAN_DOLAR = tanDolar.InnerHtml;
                ResultData.TATAPP_DOLAR = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[3]/ul/li[2]/span/b").InnerHtml;
                ResultData.TATADR_DOLAR = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[3]/ul/li[3]/span/b").InnerHtml;
                ResultData.TOTAL_DOLAR = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[3]/ul/li[4]/span/b").InnerHtml;

                return ResultData;
            }

            var tan = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[1]/ul/li[1]/span/b");

            if (tan != null) {
                ResultData.TAN = tan.InnerHtml;
                ResultData.TATAPP = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[1]/ul/li[2]/span/b").InnerHtml;
                ResultData.TATADR = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[1]/ul/li[3]/span/b").InnerHtml;
                ResultData.TOTAL = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div[1]/ul/li[4]/span/b").InnerHtml;

                return ResultData;
            }

            var error = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section/div/div");

            if (error != null)
            {
                ResultData.ERROR = error.InnerHtml.Trim();

                return ResultData;
            }

            return ResultData;
        }
    }
}
