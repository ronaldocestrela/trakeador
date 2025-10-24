namespace TrakeadorWeb.Services
{
    public interface ILinkTrackingService
    {
        string ProcessarLinkEsportiva(string linkOriginal, string codigoAfiliado, string? canal = null, string? destino = null, string? detalhesAdicionais = null, string? parametrosAdicionais = null);
        string ProcessarLinkNovibet(string linkOriginal, string codigoAfiliado, string? canal = null, string? destino = null, string? detalhesAdicionais = null, string? parametrosAdicionais = null);
        string ProcessarLinkBetMgm(string linkOriginal, string codigoAfiliado, string? canal = null, string? destino = null, string? detalhesAdicionais = null, string? parametrosAdicionais = null);
        string ProcessarLinkBetsson(string linkOriginal, string codigoAfiliado, string? canal = null, string? destino = null, string? detalhesAdicionais = null, string? parametrosAdicionais = null);
    }
}