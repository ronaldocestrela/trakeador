using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrakeadorWeb.Data;
using TrakeadorWeb.Models;

namespace TrakeadorWeb.Controllers
{
    [Authorize]
    public class CasasDeApostasController(ApplicationDbContext context) : Controller
    {
        // GET: CasasDeApostas
        public async Task<IActionResult> Index()
        {
            var casasDeApostas = await context.CasasDeApostas
                .Where(c => c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();
            
            return View(casasDeApostas);
        }

        // GET: CasasDeApostas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casaDeApostas = await context.CasasDeApostas
                .Include(c => c.Experts.Where(e => e.Ativo))
                    .ThenInclude(e => e.Expert)
                .FirstOrDefaultAsync(c => c.Id == id && c.Ativo);

            if (casaDeApostas == null)
            {
                return NotFound();
            }

            return View(casaDeApostas);
        }

        // GET: CasasDeApostas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CasasDeApostas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CasaDeApostas casaDeApostas)
        {
            ModelState.Remove(nameof(casaDeApostas.Id));
            ModelState.Remove(nameof(casaDeApostas.Experts));

            if (ModelState.IsValid)
            {
                // Verificar se já existe uma casa com o mesmo nome
                var casaExistente = await context.CasasDeApostas
                    // .FirstOrDefaultAsync(c => c.Nome.Equals(casaDeApostas.Nome, StringComparison.CurrentCultureIgnoreCase));
                    .FirstOrDefaultAsync(c => c.Nome.Equals(casaDeApostas.Nome.Equals(casaDeApostas.Nome, StringComparison.CurrentCultureIgnoreCase)));

                if (casaExistente != null)
                {
                    if (casaExistente.Ativo)
                    {
                        ModelState.AddModelError("Nome", "Já existe uma casa de apostas com este nome.");
                        return View(casaDeApostas);
                    }
                    else
                    {
                        // Reativar casa existente
                        casaExistente.Ativo = true;
                        casaExistente.Descricao = casaDeApostas.Descricao;
                        casaExistente.UrlBase = casaDeApostas.UrlBase;
                        casaExistente.DataCriacao = DateTime.Now;
                        
                        context.Update(casaExistente);
                        await context.SaveChangesAsync();
                        
                        TempData["SuccessMessage"] = "Casa de apostas reativada com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                }

                casaDeApostas.DataCriacao = DateTime.Now;
                casaDeApostas.Ativo = true;
                
                context.Add(casaDeApostas);
                await context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Casa de apostas criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(casaDeApostas);
        }

        // GET: CasasDeApostas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casaDeApostas = await context.CasasDeApostas.FindAsync(id);
            if (casaDeApostas == null || !casaDeApostas.Ativo)
            {
                return NotFound();
            }

            return View(casaDeApostas);
        }

        // POST: CasasDeApostas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CasaDeApostas casaDeApostas)
        {
            if (id != casaDeApostas.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(casaDeApostas.Experts));

            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar se já existe outra casa com o mesmo nome
                    var casaExistente = await context.CasasDeApostas
                        .FirstOrDefaultAsync(c => c.Nome.ToLower() == casaDeApostas.Nome.ToLower() && c.Id != id);

                    if (casaExistente != null)
                    {
                        ModelState.AddModelError("Nome", "Já existe uma casa de apostas com este nome.");
                        return View(casaDeApostas);
                    }

                    context.Update(casaDeApostas);
                    await context.SaveChangesAsync();
                    
                    TempData["SuccessMessage"] = "Casa de apostas atualizada com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CasaDeApostasExists(casaDeApostas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(casaDeApostas);
        }

        // GET: CasasDeApostas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casaDeApostas = await context.CasasDeApostas
                .Include(c => c.Experts.Where(e => e.Ativo))
                    .ThenInclude(e => e.Expert)
                .FirstOrDefaultAsync(c => c.Id == id && c.Ativo);

            if (casaDeApostas == null)
            {
                return NotFound();
            }

            return View(casaDeApostas);
        }

        // POST: CasasDeApostas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var casaDeApostas = await context.CasasDeApostas
                .Include(c => c.Experts.Where(e => e.Ativo))
                .FirstOrDefaultAsync(c => c.Id == id);

            if (casaDeApostas != null)
            {
                // Verificar se há experts associados
                if (casaDeApostas.Experts.Any())
                {
                    TempData["ErrorMessage"] = "Não é possível excluir esta casa de apostas pois ela possui experts associados. Desative as associações primeiro.";
                    return RedirectToAction(nameof(Index));
                }

                // Soft delete - marcar como inativo
                casaDeApostas.Ativo = false;
                context.Update(casaDeApostas);
                await context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Casa de apostas removida com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CasaDeApostasExists(int id)
        {
            return context.CasasDeApostas.Any(e => e.Id == id);
        }
    }
}