using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Models;
using ExamenMusicaNetCoreMVC.Servicios.RepositorioGenerico;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class GruposArtistasController : Controller
    {
        private readonly IRepositorioGenerico<Grupo> _contextGrupo;
        private readonly IRepositorioGenerico<VistaGruposArtista> _contextVistaGrupoArtista;
        private readonly IRepositorioGenerico<Artista> _contextArtista;
        private readonly IRepositorioGenerico<GruposArtista> _contextGrupoArtista;

        public GruposArtistasController(IRepositorioGenerico<VistaGruposArtista> contextVistaGrupoArtista, IRepositorioGenerico<GruposArtista> contextGrupoArtista, IRepositorioGenerico<Grupo> contextGrupo, IRepositorioGenerico<Artista> contextArtista)
        {
            this._contextVistaGrupoArtista = contextVistaGrupoArtista;
            this._contextGrupo = contextGrupo;
            this._contextArtista = contextArtista;
            this._contextGrupoArtista = contextGrupoArtista;
        }

        // GET: GruposArtistas
        public async Task<IActionResult> Index()
        {
            return View(await _contextVistaGrupoArtista.DameTodos());
        }

        // GET: GruposArtistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gruposArtista = await _contextVistaGrupoArtista.DameUno((int)id);

            if (gruposArtista == null)
            {
                return NotFound();
            }

            return View(gruposArtista);
        }

        // GET: GruposArtistas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ArtistasId"] = new SelectList(await _contextArtista.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await _contextGrupo.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: GruposArtistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtistasId,GruposId")] GruposArtista gruposArtista)
        {
            if (ModelState.IsValid)
            {
                await _contextGrupoArtista.Agregar(gruposArtista);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistasId"] = new SelectList(await _contextArtista.DameTodos(), "Id", "Nombre", gruposArtista.ArtistasId);
            ViewData["GruposId"] = new SelectList(await _contextGrupo.DameTodos(), "Id", "Nombre", gruposArtista.GruposId);
            return View(gruposArtista);
        }

        // GET: GruposArtistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gruposArtista = await _context.GruposArtistas.FindAsync(id);
            if (gruposArtista == null)
            {
                return NotFound();
            }
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Nombre", gruposArtista.ArtistasId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", gruposArtista.GruposId);
            return View(gruposArtista);
        }

        // POST: GruposArtistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArtistasId,GruposId")] GruposArtista gruposArtista)
        {
            if (id != gruposArtista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gruposArtista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GruposArtistaExists(gruposArtista.Id))
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
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Nombre", gruposArtista.ArtistasId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", gruposArtista.GruposId);
            return View(gruposArtista);
        }

        // GET: GruposArtistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gruposArtista = await _context.GruposArtistas
                .Include(g => g.Artistas)
                .Include(g => g.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gruposArtista == null)
            {
                return NotFound();
            }

            return View(gruposArtista);
        }

        // POST: GruposArtistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gruposArtista = await _context.GruposArtistas.FindAsync(id);
            if (gruposArtista != null)
            {
                _context.GruposArtistas.Remove(gruposArtista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GruposArtistaExists(int id)
        {
            return _context.GruposArtistas.Any(e => e.Id == id);
        }
    }
}
