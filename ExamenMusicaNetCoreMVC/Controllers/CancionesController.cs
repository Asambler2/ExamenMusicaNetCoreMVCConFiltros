using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Data;
using ExamenMusicaNetCoreMVC.Models;
using Microsoft.Data.SqlClient;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class CancionesController : Controller
    {
        private readonly ExamenMusicaNetCoreMVCContext _context;

        public CancionesController(ExamenMusicaNetCoreMVCContext context)
        {
            _context = context;
        }

        // GET: Canciones
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var examenMusicaNetCoreMVCContext = _context.Canciones.Include(c => c.Albumes);

            ViewData["OrdenFecha"] = sortOrder == "Fecha" ? "Fecha_desc" : "Fecha";
            ViewData["OrdenGenero"] = sortOrder == "Genero" ? "Genero_desc" : "Genero";
            ViewData["OrdenTitulo"] = sortOrder == "Titulo" ? "Titulo_desc" : "Titulo";

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await examenMusicaNetCoreMVCContext.Where(s => s.Titulo.Contains(searchString)).ToListAsync());
            }

            switch (sortOrder)
            {
                case "Fecha": return View(await examenMusicaNetCoreMVCContext.OrderBy(s => s.Fecha).ToListAsync());
                case "Fecha_desc": return View(await examenMusicaNetCoreMVCContext.OrderByDescending(s => s.Fecha).ToListAsync());
                case "Genero": return View(await examenMusicaNetCoreMVCContext.OrderBy(s => s.Genero).ToListAsync());
                case "Genero_desc": return View(await examenMusicaNetCoreMVCContext.OrderByDescending(s => s.Genero).ToListAsync());
                case "Titulo": return View(await examenMusicaNetCoreMVCContext.OrderBy(s => s.Titulo).ToListAsync());
                case "Titulo_desc": return View(await examenMusicaNetCoreMVCContext.OrderByDescending(s => s.Titulo).ToListAsync());
            }

            return View(await examenMusicaNetCoreMVCContext.ToListAsync());
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.Canciones
                .Include(c => c.Albumes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancione == null)
            {
                return NotFound();
            }

            return View(cancione);
        }

        // GET: Canciones/Create
        public IActionResult Create()
        {
            ViewData["AlbumesId"] = new SelectList(_context.Albumes, "Id", "Titulo");
            return View();
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Genero,Fecha,AlbumesId")] Cancione cancione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cancione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumesId"] = new SelectList(_context.Albumes, "Id", "Titulo", cancione.AlbumesId);
            return View(cancione);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.Canciones.FindAsync(id);
            if (cancione == null)
            {
                return NotFound();
            }
            ViewData["AlbumesId"] = new SelectList(_context.Albumes, "Id", "Titulo", cancione.AlbumesId);
            return View(cancione);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Genero,Fecha,AlbumesId")] Cancione cancione)
        {
            if (id != cancione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cancione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancioneExists(cancione.Id))
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
            ViewData["AlbumesId"] = new SelectList(_context.Albumes, "Id", "Titulo", cancione.AlbumesId);
            return View(cancione);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.Canciones
                .Include(c => c.Albumes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancione == null)
            {
                return NotFound();
            }

            return View(cancione);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancione = await _context.Canciones.FindAsync(id);
            if (cancione != null)
            {
                _context.Canciones.Remove(cancione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CancioneExists(int id)
        {
            return _context.Canciones.Any(e => e.Id == id);
        }
    }
}
