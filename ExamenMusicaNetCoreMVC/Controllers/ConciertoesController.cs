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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class ConciertoesController : Controller
    {
        private readonly IRepositorioGenerico<Concierto> _context;

        public ConciertoesController(IRepositorioGenerico<Concierto> context)
        {
            _context = context;
        }

        // GET: Conciertoes
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            return View(await _context.DameTodos());
        }

        public async Task<IActionResult> IndexConcierto()
        {
            var conciertos = await _context.DameTodos();
            var filtrado = from concierto in conciertos
                where concierto.Precio > 30 && ((DateTime)concierto.Fecha).Year > 2015
                select concierto;

            return View(filtrado);
        }

        // GET: Conciertoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.DameUno((int)id);
            if (concierto == null)
            {
                return NotFound();
            }

            return View(concierto);
        }

        // GET: Conciertoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conciertoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Genero,Lugar,Titulo,Precio")] Concierto concierto)
        {
            if (ModelState.IsValid)
            {
                await _context.Agregar(concierto);
                return RedirectToAction(nameof(Index));
            }
            return View(concierto);
        }

        // GET: Conciertoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.DameUno((int)id);
            if (concierto == null)
            {
                return NotFound();
            }
            return View(concierto);
        }

        // POST: Conciertoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Genero,Lugar,Titulo,Precio")] Concierto concierto)
        {
            if (id != concierto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Modificar((int)id, concierto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await ConciertoExists(concierto.Id)))
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
            return View(concierto);
        }

        // GET: Conciertoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.DameUno((int)id);
            if (concierto == null)
            {
                return NotFound();
            }

            return View(concierto);
        }

        // POST: Conciertoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concierto = _context.DameUno((int)id);
            if (concierto != null)
            {
                await _context.Borrar((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ConciertoExists(int id)
        {
            var datos = await _context.DameTodos();
             return datos.Any(e => e.Id == id);
        }
    }
}
