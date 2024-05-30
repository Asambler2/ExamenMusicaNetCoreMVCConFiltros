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
    public class ArtistasController : Controller
    {
        private readonly ExamenMusicaNetCoreMVCContext _context;

        public ArtistasController(ExamenMusicaNetCoreMVCContext context)
        {
            _context = context;
        }

        // GET: Artistas
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["OrdenNombre"] = sortOrder == "Nombre" ? "Nombre_desc" : "Nombre";
            ViewData["OrdenGenero"] = sortOrder == "Genero" ? "Genero_desc" : "Genero";
            ViewData["OrdenFecha"] = sortOrder == "Fecha" ? "Fecha_desc" : "Fecha";

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await _context.Artistas.Where(s => s.Nombre.Contains(searchString)).ToListAsync());
            }

            switch (sortOrder)
            {
                case "Fecha": return View(await _context.Artistas.OrderBy(s => s.FechaNac).ToListAsync());
                case "Fecha_desc": return View(await _context.Artistas.OrderByDescending(s => s.FechaNac).ToListAsync());
                case "Genero": return View(await _context.Artistas.OrderBy(s => s.Genero).ToListAsync());
                case "Genero_desc": return View(await _context.Artistas.OrderByDescending(s => s.Genero).ToListAsync());
                case "Titulo": return View(await _context.Artistas.OrderBy(s => s.Nombre).ToListAsync());
                case "Titulo_desc": return View(await _context.Artistas.OrderByDescending(s => s.Nombre).ToListAsync());
            }
            return View(await _context.Artistas.ToListAsync());
        }

        public async Task<IActionResult> IndexArtista()
        {
            var LosArtistas =  _context.Artistas;
            var filtrado = from artista in LosArtistas
                where ((DateOnly)artista.FechaNac).Year > 1950
                select artista;
            return View(await filtrado.ToListAsync());
        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // GET: Artistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Genero,FechaNac")] Artista artista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas.FindAsync(id);
            if (artista == null)
            {
                return NotFound();
            }
            return View(artista);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Genero,FechaNac")] Artista artista)
        {
            if (id != artista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistaExists(artista.Id))
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
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await _context.Artistas.FindAsync(id);
            if (artista != null)
            {
                _context.Artistas.Remove(artista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistaExists(int id)
        {
            return _context.Artistas.Any(e => e.Id == id);
        }
    }
}
