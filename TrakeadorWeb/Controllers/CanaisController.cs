using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrakeadorWeb.Data;
using TrakeadorWeb.Models;

namespace TrakeadorWeb.Controllers
{
    [Authorize]
    public class CanaisController(ApplicationDbContext context) : Controller
    {

        // GET: Canais
        public async Task<IActionResult> Index()
        {
            var canais = await context.Canais
                .Include(c => c.Destinos)
                .OrderBy(c => c.Nome)
                .ToListAsync();
            return View(canais);
        }

        // GET: Canais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canal = await context.Canais
                .Include(c => c.Destinos)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (canal == null)
            {
                return NotFound();
            }

            return View(canal);
        }

        // GET: Canais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Canais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Canal canal)
        {
            // Remove validação de campos não necessários
            ModelState.Remove("Id");
            ModelState.Remove("Destinos");
            
            if (ModelState.IsValid)
            {
                // Verificar se já existe um canal com o mesmo nome
                var existingCanal = await context.Canais
                    .FirstOrDefaultAsync(c => c.Nome.ToLower() == canal.Nome.ToLower());
                
                if (existingCanal != null)
                {
                    ModelState.AddModelError("Nome", "Já existe um canal com este nome.");
                    return View(canal);
                }

                context.Add(canal);
                await context.SaveChangesAsync();
                TempData["Success"] = "Canal criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(canal);
        }

        // GET: Canais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canal = await context.Canais.FindAsync(id);
            if (canal == null)
            {
                return NotFound();
            }
            return View(canal);
        }

        // POST: Canais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Canal canal)
        {
            if (id != canal.Id)
            {
                return NotFound();
            }

            // Remove validação de campos não necessários
            ModelState.Remove("Destinos");

            if (ModelState.IsValid)
            {
                // Verificar se já existe outro canal com o mesmo nome
                var existingCanal = await context.Canais
                    .FirstOrDefaultAsync(c => c.Nome.ToLower() == canal.Nome.ToLower() && c.Id != id);
                
                if (existingCanal != null)
                {
                    ModelState.AddModelError("Nome", "Já existe outro canal com este nome.");
                    return View(canal);
                }

                try
                {
                    context.Update(canal);
                    await context.SaveChangesAsync();
                    TempData["Success"] = "Canal atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CanalExists(canal.Id))
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
            return View(canal);
        }

        // GET: Canais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canal = await context.Canais
                .Include(c => c.Destinos)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (canal == null)
            {
                return NotFound();
            }

            return View(canal);
        }

        // POST: Canais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var canal = await context.Canais
                .Include(c => c.Destinos)
                .FirstOrDefaultAsync(c => c.Id == id);
            
            if (canal == null)
            {
                return NotFound();
            }

            // Verificar se há destinos associados
            if (canal.Destinos.Any())
            {
                TempData["Error"] = $"Não é possível excluir este canal pois existem {canal.Destinos.Count} destino(s) associado(s).";
                return RedirectToAction(nameof(Index));
            }

            context.Canais.Remove(canal);
            await context.SaveChangesAsync();
            TempData["Success"] = "Canal excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool CanalExists(int id)
        {
            return context.Canais.Any(e => e.Id == id);
        }
    }
}
