using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrakeadorWeb.Data;
using TrakeadorWeb.Models;

namespace TrakeadorWeb.Controllers
{
    [Authorize]
    public class DestinosController(ApplicationDbContext context) : Controller
    {

        // GET: Destinos
        public async Task<IActionResult> Index()
        {
            var destinos = await context.Destinos
                .Include(d => d.Canal)
                .OrderBy(d => d.Canal.Nome)
                .ThenBy(d => d.Nome)
                .ToListAsync();
            return View(destinos);
        }

        // GET: Destinos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destino = await context.Destinos
                .Include(d => d.Canal)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (destino == null)
            {
                return NotFound();
            }

            return View(destino);
        }

        // GET: Destinos/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CanalId"] = new SelectList(await context.Canais.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome");
            return View();
        }

        // POST: Destinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,CanalId")] Destino destino)
        {
            // Remove validação do Id que não é necessária
            ModelState.Remove("Id");
            ModelState.Remove("Canal");
            
            if (ModelState.IsValid)
            {
                // Verificar se já existe um destino com o mesmo nome no mesmo canal
                var existingDestino = await context.Destinos
                    .FirstOrDefaultAsync(d => d.Nome.ToLower() == destino.Nome.ToLower() && d.CanalId == destino.CanalId);
                
                if (existingDestino != null)
                {
                    ModelState.AddModelError("Nome", "Já existe um destino com este nome neste canal.");
                    ViewData["CanalId"] = new SelectList(await context.Canais.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome", destino.CanalId);
                    return View(destino);
                }

                context.Add(destino);
                await context.SaveChangesAsync();
                TempData["Success"] = "Destino criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CanalId"] = new SelectList(await context.Canais.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome", destino.CanalId);
            return View(destino);
        }

        // GET: Destinos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destino = await context.Destinos.FindAsync(id);
            if (destino == null)
            {
                return NotFound();
            }
            ViewData["CanalId"] = new SelectList(await context.Canais.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome", destino.CanalId);
            return View(destino);
        }

        // POST: Destinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CanalId")] Destino destino)
        {
            if (id != destino.Id)
            {
                return NotFound();
            }

            // Remove validação do Canal que não é necessária
            ModelState.Remove("Canal");

            if (ModelState.IsValid)
            {
                // Verificar se já existe outro destino com o mesmo nome no mesmo canal
                var existingDestino = await context.Destinos
                    .FirstOrDefaultAsync(d => d.Nome.ToLower() == destino.Nome.ToLower() && d.CanalId == destino.CanalId && d.Id != id);
                
                if (existingDestino != null)
                {
                    ModelState.AddModelError("Nome", "Já existe outro destino com este nome neste canal.");
                    ViewData["CanalId"] = new SelectList(await context.Canais.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome", destino.CanalId);
                    return View(destino);
                }

                try
                {
                    context.Update(destino);
                    await context.SaveChangesAsync();
                    TempData["Success"] = "Destino atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinoExists(destino.Id))
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
            ViewData["CanalId"] = new SelectList(await context.Canais.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome", destino.CanalId);
            return View(destino);
        }

        // GET: Destinos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destino = await context.Destinos
                .Include(d => d.Canal)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (destino == null)
            {
                return NotFound();
            }

            return View(destino);
        }

        // POST: Destinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destino = await context.Destinos.FindAsync(id);
            if (destino != null)
            {
                context.Destinos.Remove(destino);
                await context.SaveChangesAsync();
                TempData["Success"] = "Destino excluído com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DestinoExists(int id)
        {
            return context.Destinos.Any(e => e.Id == id);
        }
    }
}
