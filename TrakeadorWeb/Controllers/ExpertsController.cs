using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrakeadorWeb.Data;
using TrakeadorWeb.Models;

namespace TrakeadorWeb.Controllers
{
    public class ExpertsController(ApplicationDbContext context) : Controller
    {

        // GET: Experts
        public async Task<IActionResult> Index()
        {
            var experts = await context.Experts
                .Where(e => e.Ativo)
                .OrderBy(e => e.Nome)
                .ToListAsync();
            
            return View(experts);
        }

        // GET: Experts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expert = await context.Experts
                .Include(e => e.CasasDeApostas)
                    .ThenInclude(eca => eca.CasaDeApostas)
                .FirstOrDefaultAsync(m => m.Id == id && m.Ativo);

            if (expert == null)
            {
                return NotFound();
            }

            return View(expert);
        }

        // GET: Experts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Experts/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao")] Expert expert)
        {
            if (ModelState.IsValid)
            {
                expert.DataCriacao = DateTime.Now;
                expert.Ativo = true;
                context.Add(expert);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expert);
        }

        // GET: Experts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expert = await context.Experts.FindAsync(id);
            if (expert == null || !expert.Ativo)
            {
                return NotFound();
            }
            return View(expert);
        }

        // POST: Experts/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,DataCriacao,Ativo")] Expert expert)
        {
            if (id != expert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(expert);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpertExists(expert.Id))
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
            return View(expert);
        }

        // GET: Experts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expert = await context.Experts
                .FirstOrDefaultAsync(m => m.Id == id && m.Ativo);
            if (expert == null)
            {
                return NotFound();
            }

            return View(expert);
        }

        // POST: Experts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expert = await context.Experts.FindAsync(id);
            if (expert != null)
            {
                expert.Ativo = false; // Soft delete
                context.Update(expert);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ExpertExists(int id)
        {
            return context.Experts.Any(e => e.Id == id && e.Ativo);
        }
    }
}