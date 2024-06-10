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
        private readonly IRepositorioGenerico<VistaAlbume> _contextVista;
        private readonly IRepositorioGenerico<Grupo> _grupoContext;

        public AlbumesController(IRepositorioGenerico<Albume> context, IRepositorioGenerico<Grupo> grupoContext, IRepositorioGenerico<VistaAlbume> contextVista)
        {
            _context = context;
            _grupoContext = grupoContext;
            _contextVista = contextVista;

        }

        // GET: Albumes
        public async Task<IActionResult> Index()
        {

            var examenMusicaNetCoreMVCContext = await _contextVista.DameTodos();

            return View(examenMusicaNetCoreMVCContext);
        }

        public async Task<IActionResult> IndexAlbum()
        {

            var examenMusicaNetCoreMVCContext = await _contextVista.DameTodos();
            var filtrado = from album in examenMusicaNetCoreMVCContext
                where album.Genero.ToLower().Equals("heavy metal") && album.Titulo.Contains("u")
                select album;


            return View(filtrado);
        }

        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = await _contextVista.DameUno((int)id);
            if (albume == null)
            {
                return NotFound();
            }

            return View(albume);
        }

        // GET: Albumes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre");
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
                await _context.Agregar((Albume)albume);;
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre", albume.GruposId);
            return View(albume);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = await _context.DameUno((int)id);
            if (albume == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre", albume.GruposId);
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
                    if (!AlbumeExists(albume.Id).Result)
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
            ViewData["GruposId"] = new SelectList(await _grupoContext.DameTodos(), "Id", "Nombre", albume.GruposId);
            return View(albume);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albume = await _context.DameUno((int)id);
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
                await _context.Borrar((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AlbumeExists(int id)
        {
            var datos = await _context.DameTodos();
            return datos.Any(e => e.Id == id);

        }
    }
}
