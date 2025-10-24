using System.Web;
using TrakeadorWeb.Migrations;

namespace TrakeadorWeb.Services
{
    public class LinkTrackingService : ILinkTrackingService
    {
        public string ProcessarLinkEsportiva(string linkOriginal, string codigoAfiliado, string? canal = null, string? destino = null, string? detalhesAdicionais = null, string? parametrosAdicionais = null)
        {
            try
            {
                var uri = new Uri(linkOriginal);

                var shareCode = HttpUtility.ParseQueryString(uri.Query).Get("shareCode") ?? string.Empty;

                var query = HttpUtility.ParseQueryString(uri.Query);
                // Use a valid time zone ID for Bahia, Brazil
                // On Windows: "E. South America Standard Time"
                // On Linux: "America/Bahia"
                TimeZoneInfo bahiaTimeZone;
                try
                {
                    bahiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                }
                catch (TimeZoneNotFoundException)
                {
                    bahiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Bahia");
                }
                var dataBahia = TimeZoneInfo.ConvertTime(DateTime.UtcNow, bahiaTimeZone);
                
                // Adiciona os parâmetros de rastreamento da Esportiva
                query["afp"] = canal;
                query["afp1"] = dataBahia.ToString("dd_MM_yy");
                var numeroSemana = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                    dataBahia,
                    System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Sunday
                );
                var nomeMesEncurtado = dataBahia.ToString("MMM", System.Globalization.CultureInfo.InvariantCulture).ToLower();
                query["afp2"] = $"semana{numeroSemana}{nomeMesEncurtado}";
                query["afp6"] = destino;
                query["afp9"] = detalhesAdicionais;
                query["home"] = "1";

                // Mantém o shareCode original se existir
                // Os outros parâmetros são adicionados

                var afpParam = string.IsNullOrEmpty(query["afp"]) ? "" : $"&afp={query["afp"]}";
                var afp6Param = string.IsNullOrEmpty(query["afp6"]) ? "" : $"&afp6={query["afp6"]}";
                var afp9Param = string.IsNullOrEmpty(query["afp9"]) ? "" : $"&afp9={query["afp9"]}";
                var queryFinal = $"https://go.aff.esportiva.bet/{codigoAfiliado}?shareCode={shareCode}{afpParam}&afp1={query["afp1"]}&afp2={query["afp2"]}{afp6Param}{afp9Param}&home={query["home"]}";

                return queryFinal;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar o link da Esportiva.");
                Console.WriteLine($"Erro detalhes: {ex.Message}");
                return linkOriginal; // Retorna o link original em caso de erro
            }
        }

        public string ProcessarLinkNovibet(string linkOriginal, string codigoAfiliado, string? canal = null, string? destino = null, string? detalhesAdicionais = null, string? parametrosAdicionais = null)
        {
            try
            {
                // Para Novibet, precisamos construir um link completamente novo
                var encodedUrl = HttpUtility.UrlEncode(linkOriginal);
                var uri = new Uri(linkOriginal);

                var query = HttpUtility.ParseQueryString(uri.Query);
                // Use a valid time zone ID for Bahia, Brazil
                // On Windows: "E. South America Standard Time"
                // On Linux: "America/Bahia"
                TimeZoneInfo bahiaTimeZone;
                try
                {
                    bahiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                }
                catch (TimeZoneNotFoundException)
                {
                    bahiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Bahia");
                }
                var dataBahia = TimeZoneInfo.ConvertTime(DateTime.UtcNow, bahiaTimeZone);

                query["afp"] = canal;
                query["afp1"] = dataBahia.ToString("dd_MM_yy");
                var numeroSemana = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                    dataBahia,
                    System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Sunday
                );
                var nomeMesEncurtado = dataBahia.ToString("MMM", System.Globalization.CultureInfo.InvariantCulture).ToLower();
                query["afp2"] = $"semana{numeroSemana}{nomeMesEncurtado}";
                query["afp6"] = destino;
                query["afp9"] = detalhesAdicionais;
                query["home"] = "1";

                var afpParam = string.IsNullOrEmpty(query["afp"]) ? "" : $"&afp={query["afp"]}";
                var afp6Param = string.IsNullOrEmpty(query["afp6"]) ? "" : $"&afp6={query["afp6"]}";
                var afp9Param = string.IsNullOrEmpty(query["afp9"]) ? "" : $"&afp9={query["afp9"]}";
                
                // https://rt.novibet.partners/o/MVpiOM?lpage=jcBppl&site_id=1020436&redirect_url=https%3A%2F%2Fwww.novibet.bet.br%2Fsports%2Fshared-bet%2F5db6d066ae07ae05329d5893911848a80560fc07e5968dce5fc16f758845d85b-0
                return $"https://rt.novibet.partners/o/{parametrosAdicionais}?lpage=jcBppl&site_id={codigoAfiliado}&redirect_url={encodedUrl}{afpParam}&afp1={query["afp1"]}&afp2={query["afp2"]}{afp6Param}{afp9Param}&home={query["home"]}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar o link da Novibet.");
                Console.WriteLine($"Erro detalhes: {ex.Message}");
                return linkOriginal; // Retorna o link original em caso de erro
            }
        }

        public string ProcessarLinkBetMgm(string linkOriginal, string codigoAfiliado, string? canal = null, string? destino = null, string? detalhesAdicionais = null, string? parametrosAdicionais = null)
        {
            try
            {
                // Para BetMGM, o linkOriginal contém apenas os IDs dos jogos
                // Exemplo: "3906784898,3906729211" ou "3906784898"

                return $"https://ntrfr.betmgm.bet.br/redirect.aspx?pid={codigoAfiliado}&bid=1519&redirectURL=https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|{linkOriginal}|30|replace";
            }
            catch (Exception)
            {
                return linkOriginal; // Retorna o link original em caso de erro
            }
        }
        
        public string ProcessarLinkBetsson(string linkOriginal, string codigoAfiliado, string? canal = null, string? destino = null, string? detalhesAdicionais = null, string? parametrosAdicionais = null)
        {
            try
            {
                var uri = new Uri(linkOriginal);
                // var betCode = linkOriginal.Split("betslip=")[1];

                var query = HttpUtility.ParseQueryString(uri.Query);
                // Use a valid time zone ID for Bahia, Brazil
                // On Windows: "E. South America Standard Time"
                // On Linux: "America/Bahia"
                TimeZoneInfo bahiaTimeZone;
                try
                {
                    bahiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                }
                catch (TimeZoneNotFoundException)
                {
                    bahiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Bahia");
                }
                var dataBahia = TimeZoneInfo.ConvertTime(DateTime.UtcNow, bahiaTimeZone);
                
                // Adiciona os parâmetros de rastreamento da Betsson
                query["afp"] = canal;
                query["afp1"] = dataBahia.ToString("dd_MM_yy");
                var numeroSemana = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                    dataBahia,
                    System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Sunday
                );
                var nomeMesEncurtado = dataBahia.ToString("MMM", System.Globalization.CultureInfo.InvariantCulture).ToLower();
                query["afp2"] = $"semana{numeroSemana}{nomeMesEncurtado}";
                query["afp6"] = destino;
                query["afp9"] = detalhesAdicionais;
                query["home"] = "1";

                var afpParam = string.IsNullOrEmpty(query["afp"]) ? "" : $"&afp={query["afp"]}";
                var afp6Param = string.IsNullOrEmpty(query["afp6"]) ? "" : $"&afp6={query["afp6"]}";
                var afp9Param = string.IsNullOrEmpty(query["afp9"]) ? "" : $"&afp9={query["afp9"]}";
                // var queryFinal = $"https://track.betsson.com/click?pid={codigoAfiliado}&redirect={HttpUtility.UrlEncode(linkOriginal)}{afpParam}&afp1={query["afp1"]}&afp2={query["afp2"]}{afp6Param}{afp9Param}&home={query["home"]}";
                var queryFinal = $"{uri}?{afpParam}{afp6Param}{afp9Param}&afp1={query["afp1"]}&afp2={query["afp2"]}{codigoAfiliado}";

                return queryFinal;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar o link da Betsson.");
                Console.WriteLine($"Erro detalhes: {ex.Message}");
                return linkOriginal; // Retorna o link original em caso de erro
            }
        }
    }
}