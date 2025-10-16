namespace TrakeadorWeb.Services
{
    public interface ILinkTrackingService
    {
        string ProcessarLinkEsportiva(string linkOriginal, string codigoAfiliado, string? parametrosAdicionais = null);
        string ProcessarLinkNovibet(string linkOriginal, string codigoAfiliado, string? parametrosAdicionais = null);
        string ProcessarLinkBetMgm(string linkOriginal, string codigoAfiliado, string? parametrosAdicionais = null);
    }
}