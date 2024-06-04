using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Data;
using ExamenMusicaNetCoreMVC.Models;
using ExamenMusicaNetCoreMVC.ViewModels;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Security.Policy;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class GrupoesController : Controller
    {
        private readonly ExamenMusicaNetCoreMVCContext _context;
        public readonly IListaGruposConListaDeConciertos BuilderLista;

        public GrupoesController(ExamenMusicaNetCoreMVCContext context, IListaGruposConListaDeConciertos builderLista)
        {
            _context = context;
            this.BuilderLista = builderLista;
        }

        // GET: Grupoes
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["OrdenNombre"] = sortOrder == "Nombre" ? "Nombre_desc" : "Nombre";

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await _context.Grupos.Where(s => s.Nombre.Contains(searchString)).ToListAsync());
            }

            switch (sortOrder)
            {
                case "Nombre": return View(await _context.Grupos.OrderBy(s => s.Nombre).ToListAsync());
                case "Nombre_desc": return View(await _context.Grupos.OrderByDescending(s => s.Nombre).ToListAsync());
            }

            return View(await _context.Grupos.ToListAsync());
        }

        public async Task<IActionResult> ConciertosPorGrupos(string grupo = "", int page = 1, int size = 20, int total = 0)
        {
            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.Total = total;
            ViewBag.Grupo = grupo;
            return View(this.BuilderLista.DameGruposConListaConciertos());
        }

        // GET: Grupoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // GET: Grupoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grupoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grupo);
        }

        // GET: Grupoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }
            return View(grupo);
        }

        // POST: Grupoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Grupo grupo)
        {
            if (id != grupo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.Id))
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
            return View(grupo);
        }

        // GET: Grupoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // POST: Grupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo != null)
            {
                _context.Grupos.Remove(grupo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupoExists(int id)
        {
            return _context.Grupos.Any(e => e.Id == id);
        }
    }
}
