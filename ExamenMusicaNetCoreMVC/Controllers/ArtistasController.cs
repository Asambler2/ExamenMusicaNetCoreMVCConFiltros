using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Models;
using ExamenMusicaNetCoreMVC.Servicios.RepositorioGenerico;
using Microsoft.Data.SqlClient;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class ArtistasController : Controller
    {
        private readonly IRepositorioGenerico<Artista> _context;

        public ArtistasController(IRepositorioGenerico<Artista> context)
        {
            _context = context;
        }

        // GET: Artistas
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            return View(_context.DameTodos().ToList());
        }

        public async Task<IActionResult> IndexArtista()
        {
            var LosArtistas =  _context.DameTodos().ToList();
            var filtrado = from artista in LosArtistas
                where ((DateOnly)artista.FechaNac).Year > 1950
                select artista;
            return View(filtrado.ToList());
        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = _context.DameUno((int)id);
     
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
                _context.Agregar(artista);
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

            var artista = _context.DameUno((int)id);
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
                    _context.Modificar((int)id, artista);
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

            var artista = _context.DameUno((int)id);
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
            var artista = _context.DameUno((int)id);
            if (artista != null)
            {
                _context.Borrar((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ArtistaExists(int id)
        {
            return _context.DameTodos().ToList().Any(e => e.Id == id);
        }
    }
}
