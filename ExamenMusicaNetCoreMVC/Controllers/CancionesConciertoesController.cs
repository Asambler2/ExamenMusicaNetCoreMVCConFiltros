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
    public class CancionesConciertoesController : Controller
    {
        private readonly IRepositorioGenerico<CancionesConcierto> _context;
        private readonly IRepositorioGenerico<Cancione> _cancionesContext;
        private readonly IRepositorioGenerico<Concierto> _conciertoContext;

        public CancionesConciertoesController(IRepositorioGenerico<CancionesConcierto> context, IRepositorioGenerico<Cancione> cancionesContext, IRepositorioGenerico<Concierto> conciertoContext)
        {
            _context = context;
            _cancionesContext = cancionesContext;
            _conciertoContext = conciertoContext;
        }

        // GET: CancionesConciertoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DameTodos());
        }

        // GET: CancionesConciertoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancionesConcierto = await _context.DameUno((int)id);
                
            if (cancionesConcierto == null)
            {
                return NotFound();
            }

            return View(cancionesConcierto);
        }

        // GET: CancionesConciertoes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CancionesId"] = new SelectList(await _cancionesContext.DameTodos(), "Id", "Titulo");
            ViewData["ConciertosId"] = new SelectList(await _conciertoContext.DameTodos(), "Id", "Lugar");
            return View();
        }

        // POST: CancionesConciertoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CancionesId,ConciertosId")] CancionesConcierto cancionesConcierto)
        {
            if (ModelState.IsValid)
            {
                await _context.Agregar(cancionesConcierto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CancionesId"] = new SelectList(await _cancionesContext.DameTodos(), "Id", "Titulo", cancionesConcierto.CancionesId);
            ViewData["ConciertosId"] = new SelectList(await _cancionesContext.DameTodos(), "Id", "Lugar", cancionesConcierto.ConciertosId);
            return View(cancionesConcierto);
        }

        // GET: CancionesConciertoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancionesConcierto = await _context.DameUno((int)id);
            if (cancionesConcierto == null)
            {
                return NotFound();
            }
            ViewData["CancionesId"] = new SelectList(await _cancionesContext.DameTodos(), "Id", "Titulo", cancionesConcierto.CancionesId);
            ViewData["ConciertosId"] = new SelectList(await _cancionesContext.DameTodos(), "Id", "Lugar", cancionesConcierto.ConciertosId);
            return View(cancionesConcierto);
        }

        // POST: CancionesConciertoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CancionesId,ConciertosId")] CancionesConcierto cancionesConcierto)
        {
            if (id != cancionesConcierto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Modificar((int)id, cancionesConcierto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancionesConciertoExists(cancionesConcierto.Id).Result)
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
            ViewData["CancionesId"] = new SelectList(await _cancionesContext.DameTodos(), "Id", "Titulo", cancionesConcierto.CancionesId);
            ViewData["ConciertosId"] = new SelectList(await _conciertoContext.DameTodos(), "Id", "Lugar", cancionesConcierto.ConciertosId);
            return View(cancionesConcierto);
        }

        // GET: CancionesConciertoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancionesConcierto = await _context.DameUno((int)id);
       
            if (cancionesConcierto == null)
            {
                return NotFound();
            }

            return View(cancionesConcierto);
        }

        // POST: CancionesConciertoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancionesConcierto = _context.DameUno((int)id);
            if (cancionesConcierto != null)
            {
                 await _context.Borrar((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CancionesConciertoExists(int id)
        {
            var datos = await _context.DameTodos();
             return  datos.Any(e => e.Id == id);
        }
    }
}
