using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrakeadorWeb.Data;
using TrakeadorWeb.Models;
using TrakeadorWeb.ViewModels;

namespace TrakeadorWeb.Controllers
{
    [Authorize]
    public class ExpertCasaApostasController(ApplicationDbContext context) : Controller
    {

        // GET: ExpertCasaApostas/Index/5 (ExpertId)
        public async Task<IActionResult> Index(int? expertId)
        {
            if (expertId == null)
            {
                return NotFound();
            }

            var expert = await context.Experts
                .Include(e => e.CasasDeApostas)
                    .ThenInclude(eca => eca.CasaDeApostas)
                .FirstOrDefaultAsync(e => e.Id == expertId && e.Ativo);

            if (expert == null)
            {
                return NotFound();
            }

            return View(expert);
        }

        // GET: ExpertCasaApostas/Create/5 (ExpertId)
        public async Task<IActionResult> Create(int? expertId)
        {
            if (expertId == null)
            {
                return NotFound();
            }

            var expert = await context.Experts.FindAsync(expertId);
            if (expert == null || !expert.Ativo)
            {
                return NotFound();
            }

            // Buscar casas de apostas que ainda não estão associadas a este expert
            var casasJaAssociadas = await context.ExpertCasaApostasAfiliados
                .Where(eca => eca.ExpertId == expertId && eca.Ativo)
                .Select(eca => eca.CasaDeApostasId)
                .ToListAsync();

            var casasDisponiveis = await context.CasasDeApostas
                .Where(c => c.Ativo && !casasJaAssociadas.Contains(c.Id))
                .ToListAsync();

            if (casasDisponiveis.Count == 0)
            {
                TempData["ErrorMessage"] = "Todas as casas de apostas já estão associadas a este expert.";
                return RedirectToAction(nameof(Index), new { expertId });
            }

            var viewModel = new ExpertCasaApostasViewModel
            {
                ExpertId = expertId.Value,
                ExpertNome = expert.Nome,
                CasasDeApostas = new SelectList(casasDisponiveis, "Id", "Nome")
            };

            return View(viewModel);
        }

        // POST: ExpertCasaApostas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpertCasaApostasViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar se a associação já existe
                var associacaoExistente = await context.ExpertCasaApostasAfiliados
                    .FirstOrDefaultAsync(eca => 
                        eca.ExpertId == model.ExpertId && 
                        eca.CasaDeApostasId == model.CasaDeApostasId);

                if (associacaoExistente != null)
                {
                    if (associacaoExistente.Ativo)
                    {
                        ModelState.AddModelError("", "Esta casa de apostas já está associada ao expert.");
                    }
                    else
                    {
                        // Reativar associação existente
                        associacaoExistente.Ativo = true;
                        associacaoExistente.CodigoAfiliado = model.CodigoAfiliado;
                        associacaoExistente.ParametrosAdicionais = model.ParametrosAdicionais;
                        associacaoExistente.DataCriacao = DateTime.Now;
                        
                        context.Update(associacaoExistente);
                        await context.SaveChangesAsync();
                        
                        TempData["SuccessMessage"] = "Associação reativada com sucesso!";
                        return RedirectToAction(nameof(Index), new { expertId = model.ExpertId });
                    }
                }
                else
                {
                    var novaAssociacao = new ExpertCasaApostasAfiliado
                    {
                        ExpertId = model.ExpertId,
                        CasaDeApostasId = model.CasaDeApostasId,
                        CodigoAfiliado = model.CodigoAfiliado,
                        ParametrosAdicionais = model.ParametrosAdicionais,
                        Ativo = true,
                        DataCriacao = DateTime.Now
                    };

                    context.Add(novaAssociacao);
                    await context.SaveChangesAsync();
                    
                    TempData["SuccessMessage"] = "Casa de apostas associada com sucesso!";
                    return RedirectToAction(nameof(Index), new { expertId = model.ExpertId });
                }
            }

            // Recarregar dados para a view em caso de erro
            var expert = await context.Experts.FindAsync(model.ExpertId);
            model.ExpertNome = expert?.Nome ?? "";

            var casasJaAssociadas = await context.ExpertCasaApostasAfiliados
                .Where(eca => eca.ExpertId == model.ExpertId && eca.Ativo)
                .Select(eca => eca.CasaDeApostasId)
                .ToListAsync();

            var casasDisponiveis = await context.CasasDeApostas
                .Where(c => c.Ativo && !casasJaAssociadas.Contains(c.Id))
                .ToListAsync();

            model.CasasDeApostas = new SelectList(casasDisponiveis, "Id", "Nome");

            return View(model);
        }

        // GET: ExpertCasaApostas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var associacao = await context.ExpertCasaApostasAfiliados
                .Include(eca => eca.Expert)
                .Include(eca => eca.CasaDeApostas)
                .FirstOrDefaultAsync(eca => eca.Id == id && eca.Ativo);

            if (associacao == null)
            {
                return NotFound();
            }

            var viewModel = new ExpertCasaApostasViewModel
            {
                Id = associacao.Id,
                ExpertId = associacao.ExpertId,
                ExpertNome = associacao.Expert.Nome,
                CasaDeApostasId = associacao.CasaDeApostasId,
                CasaDeApostasNome = associacao.CasaDeApostas.Nome,
                CodigoAfiliado = associacao.CodigoAfiliado,
                ParametrosAdicionais = associacao.ParametrosAdicionais
            };

            return View(viewModel);
        }

        // POST: ExpertCasaApostas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpertCasaApostasViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var associacao = await context.ExpertCasaApostasAfiliados.FindAsync(id);
                    if (associacao == null || !associacao.Ativo)
                    {
                        return NotFound();
                    }

                    associacao.CodigoAfiliado = model.CodigoAfiliado;
                    associacao.ParametrosAdicionais = model.ParametrosAdicionais;

                    context.Update(associacao);
                    await context.SaveChangesAsync();
                    
                    TempData["SuccessMessage"] = "Associação atualizada com sucesso!";
                    return RedirectToAction(nameof(Index), new { expertId = associacao.ExpertId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Erro de concorrência. O registro pode ter sido alterado por outro usuário.");
                }
            }

            // Recarregar dados em caso de erro
            var associacaoReload = await context.ExpertCasaApostasAfiliados
                .Include(eca => eca.Expert)
                .Include(eca => eca.CasaDeApostas)
                .FirstOrDefaultAsync(eca => eca.Id == id);

            if (associacaoReload != null)
            {
                model.ExpertNome = associacaoReload.Expert.Nome;
                model.CasaDeApostasNome = associacaoReload.CasaDeApostas.Nome;
            }

            return View(model);
        }

        // GET: ExpertCasaApostas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var associacao = await context.ExpertCasaApostasAfiliados
                .Include(eca => eca.Expert)
                .Include(eca => eca.CasaDeApostas)
                .FirstOrDefaultAsync(eca => eca.Id == id && eca.Ativo);

            if (associacao == null)
            {
                return NotFound();
            }

            return View(associacao);
        }

        // POST: ExpertCasaApostas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var associacao = await context.ExpertCasaApostasAfiliados.FindAsync(id);
            if (associacao != null)
            {
                associacao.Ativo = false; // Soft delete
                context.Update(associacao);
                await context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Associação removida com sucesso!";
                return RedirectToAction(nameof(Index), new { expertId = associacao.ExpertId });
            }

            return NotFound();
        }
    }
}