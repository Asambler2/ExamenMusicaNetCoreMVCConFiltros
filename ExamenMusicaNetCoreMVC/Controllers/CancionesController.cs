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
    public class CancionesController : Controller
    {
        private readonly IRepositorioGenerico<Cancione> _context;
        private readonly IRepositorioGenerico<Albume> _albunescontext;

        public CancionesController(IRepositorioGenerico<Cancione> context, IRepositorioGenerico<Albume> Albunescontext)
        {
            _context = context;
            _albunescontext = Albunescontext;
        }

        // GET: Canciones
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var examenMusicaNetCoreMVCContext = _context.DameTodos().ToList();

            return View(examenMusicaNetCoreMVCContext);
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = _context.DameUno((int)id)
    
            if (cancione == null)
            {
                return NotFound();
            }

            return View(cancione);
        }

        // GET: Canciones/Create
        public IActionResult Create()
        {
            ViewData["AlbumesId"] = new SelectList(_albunescontext.DameTodos().ToList(), "Id", "Titulo");
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
                _context.Agregar(cancione);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumesId"] = new SelectList(_albunescontext.DameTodos().ToList(), "Id", "Titulo", cancione.AlbumesId);
            return View(cancione);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = _context.DameUno((int)id);
            if (cancione == null)
            {
                return NotFound();
            }
            ViewData["AlbumesId"] = new SelectList(_albunescontext.DameTodos().ToList(), "Id", "Titulo", cancione.AlbumesId);
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
                    _context.Modificar((int)id, cancione);
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
            ViewData["AlbumesId"] = new SelectList(_albunescontext.DameTodos().ToList(), "Id", "Titulo", cancione.AlbumesId);
            return View(cancione);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = _context.DameUno((int)id);

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
            var cancione = _context.DameUno((int)id);
            if (cancione != null)
            {
                _context.Borrar((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CancioneExists(int id)
        {
            return _context.DameTodos().ToList().Any(e => e.Id == id);
        }
    }
}
