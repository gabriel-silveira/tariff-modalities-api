using System.Text.Json;
using System.Text;
using HtmlAgilityPack;

using CTCEA_Tariff_Modalities.Services.Interfaces;
using CTCEA_Tariff_Modalities.Models.Calculator.DTO;
using CTCEA_Tariff_Modalities.Models.Base.DTO;

namespace CTCEA_Tariff_Modalities.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly string _group1Url = "https://tarifas.decea.mil.br/Simuladortarifas/CalculoGrupoI/Calculo";

        private readonly string _group2Url = "https://tarifas.decea.mil.br/Simuladortarifas/CalculoGrupoII/Calcular";

        private CalculatorResponseDTO ResultData = new CalculatorResponseDTO()
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

        public async Task<ResponseBaseDTO<CalculatorResponseDTO>> CalcularTarifas(CalculatorRequestDTO request)
        {
            HttpClient client = new HttpClient();

            string url = GetGroupUrl(request.Grupo);

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

            string queryString = GetQueryString(request);

            var result = await client.PostAsync(queryString, jsonContent);

            var content = await result.Content.ReadAsStringAsync();

            return new ResponseBaseDTO<CalculatorResponseDTO>() { Result = ParseResults(content) };
        }

        private string GetGroupUrl(string group)
        {
            return group == ClassificationService.GRUPO_1 ? _group1Url : _group2Url;
        }

        private static string GetQueryString(CalculatorRequestDTO request)
        {
            string query = "";

            if (request.Grupo == ClassificationService.GRUPO_1)
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

        private CalculatorResponseDTO ParseResults(string results)
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
