using System.Web;

namespace TrakeadorWeb.Services
{
    public class LinkTrackingService : ILinkTrackingService
    {
        public string ProcessarLinkEsportiva(string linkOriginal, string codigoAfiliado, string? parametrosAdicionais = null)
        {
            try
            {
                var uri = new Uri(linkOriginal);
                var query = HttpUtility.ParseQueryString(uri.Query);
                
                // Adiciona os parâmetros de rastreamento da Esportiva
                query["afp"] = "trafego";
                query["afp1"] = "14_10_25";
                query["afp2"] = "semana3out";
                query["afp6"] = "superodd";
                query["afp9"] = "SPODDBOTXFLAGP";
                query["home"] = "1";

                // Mantém o shareCode original se existir
                // Os outros parâmetros são adicionados

                var builder = new UriBuilder(uri)
                {
                    Query = query.ToString()
                };

                return builder.ToString();
            }
            catch (Exception)
            {
                return linkOriginal; // Retorna o link original em caso de erro
            }
        }

        public string ProcessarLinkNovibet(string linkOriginal, string codigoAfiliado, string? parametrosAdicionais = null)
        {
            try
            {
                // Para Novibet, precisamos construir um link completamente novo
                var encodedUrl = HttpUtility.UrlEncode(linkOriginal);
                
                return $"https://rt.novibet.partners/o/MVpiOM?lpage=jcBppl&site_id=1020436&redirect_url={encodedUrl}";
            }
            catch (Exception)
            {
                return linkOriginal; // Retorna o link original em caso de erro
            }
        }

        public string ProcessarLinkBetMgm(string linkOriginal, string codigoAfiliado, string? parametrosAdicionais = null)
        {
            try
            {
                // Para BetMGM, o linkOriginal contém apenas os IDs dos jogos
                // Exemplo: "3906784898,3906729211" ou "3906784898"
                
                return $"https://ntrfr.betmgm.bet.br/redirect.aspx?pid=3393&bid=1519&redirectURL=https://www.betmgm.bet.br/aposta-esportiva#featured?coupon=combination|{linkOriginal}|30|replace";
            }
            catch (Exception)
            {
                return linkOriginal; // Retorna o link original em caso de erro
            }
        }
    }
}