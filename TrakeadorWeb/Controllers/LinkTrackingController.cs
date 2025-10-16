using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrakeadorWeb.Data;
using TrakeadorWeb.Services;
using TrakeadorWeb.ViewModels;

namespace TrakeadorWeb.Controllers
{
    public class LinkTrackingController(ApplicationDbContext context, ILinkTrackingService linkTrackingService) : Controller
    {
        // GET: LinkTracking/Expert/5
        public async Task<IActionResult> Expert(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expert = await context.Experts
                .Include(e => e.CasasDeApostas.Where(eca => eca.Ativo))
                    .ThenInclude(eca => eca.CasaDeApostas)
                .FirstOrDefaultAsync(e => e.Id == id && e.Ativo);

            if (expert == null)
            {
                return NotFound();
            }

            return View(expert);
        }

        // GET: LinkTracking/Casa/5/Expert/3
        public async Task<IActionResult> Casa(int? casaId, int? expertId)
        {
            if (casaId == null || expertId == null)
            {
                return NotFound();
            }

            var relacao = await context.ExpertCasaApostasAfiliados
                .Include(eca => eca.Expert)
                .Include(eca => eca.CasaDeApostas)
                .FirstOrDefaultAsync(eca => 
                    eca.CasaDeApostasId == casaId && 
                    eca.ExpertId == expertId && 
                    eca.Ativo);

            if (relacao == null)
            {
                return NotFound();
            }

            var viewModel = new LinkTrackingViewModel
            {
                ExpertCasaApostasAfiliado = relacao
            };

            return View(viewModel);
        }

        // POST: LinkTracking/ProcessarLink
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessarLink(LinkTrackingViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LinkOriginal))
            {
                ModelState.AddModelError("LinkOriginal", "O link original é obrigatório.");
                return View("Casa", model);
            }

            var relacao = await context.ExpertCasaApostasAfiliados
                .Include(eca => eca.Expert)
                .Include(eca => eca.CasaDeApostas)
                .FirstOrDefaultAsync(eca => eca.Id == model.ExpertCasaApostasAfiliadoId);

            if (relacao == null)
            {
                return NotFound();
            }

            model.ExpertCasaApostasAfiliado = relacao;

            // Processar o link baseado na casa de apostas
            var casaNome = relacao.CasaDeApostas.Nome.ToLower();
            
            try
            {
                model.LinkRastreado = casaNome switch
                {
                    "esportiva" or "esportiva.bet" => linkTrackingService.ProcessarLinkEsportiva(
                        model.LinkOriginal, 
                        relacao.CodigoAfiliado, 
                        relacao.ParametrosAdicionais),
                    
                    "novibet" => linkTrackingService.ProcessarLinkNovibet(
                        model.LinkOriginal, 
                        relacao.CodigoAfiliado, 
                        relacao.ParametrosAdicionais),
                    
                    "betmgm" => linkTrackingService.ProcessarLinkBetMgm(
                        model.LinkOriginal, 
                        relacao.CodigoAfiliado, 
                        relacao.ParametrosAdicionais),
                    
                    _ => model.LinkOriginal
                };

                ViewBag.Sucesso = "Link processado com sucesso!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao processar o link: {ex.Message}");
                model.LinkRastreado = model.LinkOriginal;
            }

            return View("Casa", model);
        }
    }
}