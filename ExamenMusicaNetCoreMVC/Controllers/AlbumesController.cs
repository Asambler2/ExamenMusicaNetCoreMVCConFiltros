using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using ExamenMusicaNetCoreMVC.Models;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class AlbumesController : Controller
    {
        private readonly GrupoCContext _context;

        public AlbumesController(GrupoCContext context)
        {
            _context = context;
        }

        // GET: Albumes
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {

            var examenMusicaNetCoreMVCContext = _context.Albumes.Include(a => a.Grupos);

            ViewData["OrdenFecha"] = sortOrder == "Fecha" ? "Fecha_desc" : "Fecha";
            ViewData["OrdenGenero"] = sortOrder == "Genero" ? "Genero_desc" : "Genero";
            ViewData["OrdenTitulo"] = sortOrder == "Titulo" ? "Titulo_desc" : "Titulo";
            ViewData["OrdenGrupo"] = sortOrder == "Grupo" ? "Grupo_desc" : "Grupo";

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await examenMusicaNetCoreMVCContext.Where(s => s.Titulo.Contains(searchString)).ToListAsync());
            }

            switch (sortOrder)
            {
                case "Fecha":  return View(await examenMusicaNetCoreMVCContext.OrderBy(s => s.Fecha).ToListAsync());
                case "Fecha_desc": return View(await examenMusicaNetCoreMVCContext.OrderByDescending(s => s.Fecha).ToListAsync());
                case "Genero": return View(await examenMusicaNetCoreMVCContext.OrderBy(s => s.Genero).ToListAsync());
                case "Genero_desc": return View(await examenMusicaNetCoreMVCContext.OrderByDescending(s => s.Genero).ToListAsync());
                case "Titulo": return View(await examenMusicaNetCoreMVCContext.OrderBy(s => s.Titulo).ToListAsync());
                case "Titulo_desc": return View(await examenMusicaNetCoreMVCContext.OrderByDescending(s => s.Titulo).ToListAsync());
                case "Grupo": return View(await examenMusicaNetCoreMVCContext.OrderBy(s => s.Grupos).ToListAsync());
                case "Grupo_desc": return View(await examenMusicaNetCoreMVCContext.OrderByDescending(s => s.Grupos).ToListAsync());
            }
            return View(await examenMusicaNetCoreMVCContext.ToListAsync());
        }

        public async Task<IActionResult> IndexAlbum()
        {

            var examenMusicaNetCoreMVCContext = _context.Albumes.Include(a => a.Grupos);
            var filtrado = from album in examenMusicaNetCoreMVCContext
                where album.Genero.ToLower().Equals("heavy metal") && album.Titulo.Contains("u")
                select album;


            return View(await filtrado.ToListAsync());
        }

        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = await _context.Albumes
                .Include(a => a.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (albume == null)
            {
                return NotFound();
            }

            return View(albume);
        }

        // GET: Albumes/Create
        public IActionResult Create()
        {
            ViewData["GruposId"] = new SelectList(_context.Set<Grupo>(), "Id", "Nombre");
            return View();
        }

        // POST: Albumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Genero,Titulo,GruposId")] Albume albume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(albume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(_context.Set<Grupo>(), "Id", "Nombre", albume.GruposId);
            return View(albume);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = await _context.Albumes.FindAsync(id);
            if (albume == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(_context.Set<Grupo>(), "Id", "Nombre", albume.GruposId);
            return View(albume);
        }

        // POST: Albumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Genero,Titulo,GruposId")] Albume albume)
        {
            if (id != albume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(albume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumeExists(albume.Id))
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
            ViewData["GruposId"] = new SelectList(_context.Set<Grupo>(), "Id", "Nombre", albume.GruposId);
            return View(albume);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = await _context.Albumes
                .Include(a => a.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (albume == null)
            {
                return NotFound();
            }

            return View(albume);
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albume = await _context.Albumes.FindAsync(id);
            if (albume != null)
            {
                _context.Albumes.Remove(albume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumeExists(int id)
        {
            return _context.Albumes.Any(e => e.Id == id);
        }
    }
}
