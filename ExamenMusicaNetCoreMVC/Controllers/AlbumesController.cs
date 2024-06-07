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
    public class AlbumesController : Controller
    {
        private readonly IRepositorioGenerico<Albume> _context;
        private readonly IRepositorioGenerico<Grupo> _grupoContext;

        public AlbumesController(IRepositorioGenerico<Albume> context, IRepositorioGenerico<Grupo> grupoContext)
        {
            _context = context;
            _grupoContext = grupoContext;

        }

        // GET: Albumes
        public async Task<IActionResult> Index()
        {

            var examenMusicaNetCoreMVCContext = _context.DameTodos();

            
            return View(examenMusicaNetCoreMVCContext.ToList());
        }

        public async Task<IActionResult> IndexAlbum()
        {

            var examenMusicaNetCoreMVCContext = _context.DameTodos();
            var filtrado = from album in examenMusicaNetCoreMVCContext
                where album.Genero.ToLower().Equals("heavy metal") && album.Titulo.Contains("u")
                select album;


            return View(filtrado.ToList());
        }

        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = _context.DameUno((int)id);
            if (albume == null)
            {
                return NotFound();
            }

            return View(albume);
        }

        // GET: Albumes/Create
        public IActionResult Create()
        {
            ViewData["GruposId"] = new SelectList(_grupoContext.DameTodos().ToList(), "Id", "Nombre");
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
                _context.Agregar(albume);;
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(_grupoContext.DameTodos().ToList(), "Id", "Nombre", albume.GruposId);
            return View(albume);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = _context.DameUno((int)id);
            if (albume == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(_grupoContext.DameTodos().ToList(), "Id", "Nombre", albume.GruposId);
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
                    _context.Modificar(id, albume);
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
            ViewData["GruposId"] = new SelectList(_grupoContext.DameTodos().ToList(), "Id", "Nombre", albume.GruposId);
            return View(albume);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = _context.DameUno((int)id);
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
            var albume = _context.DameUno((int)id);
            if (albume != null)
            {
                _context.Borrar((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlbumeExists(int id)
        {
            return _context.DameTodos().ToList().Any(e => e.Id == id);
        }
    }
}
