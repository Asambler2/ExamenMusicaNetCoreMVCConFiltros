using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Models;
using Microsoft.Data.SqlClient;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class ConciertoesController : Controller
    {
        private readonly GrupoCContext _context;

        public ConciertoesController(GrupoCContext context)
        {
            _context = context;
        }

        // GET: Conciertoes
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["OrdenLugar"] = sortOrder == "Lugar" ? "Lugar_desc" : "Lugar";
            ViewData["OrdenGenero"] = sortOrder == "Genero" ? "Genero_desc" : "Genero";
            ViewData["OrdenFecha"] = sortOrder == "Fecha" ? "Fecha_desc" : "Fecha";
            ViewData["OrdenTitulo"] = sortOrder == "Titulo" ? "Titulo_desc" : "Titulo";
            ViewData["OrdenPrecio"] = sortOrder == "Precio" ? "Precio_desc" : "Precio";

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await _context.Conciertos.Where(s => s.Genero.Contains(searchString)).ToListAsync());
            }

            switch (sortOrder)
            {
                case "Fecha": return View(await _context.Conciertos.OrderBy(s => s.Fecha).ToListAsync());
                case "Fecha_desc": return View(await _context.Conciertos.OrderByDescending(s => s.Fecha).ToListAsync());
                case "Genero": return View(await _context.Conciertos.OrderBy(s => s.Genero).ToListAsync());
                case "Genero_desc": return View(await _context.Conciertos.OrderByDescending(s => s.Genero).ToListAsync());
                case "Lugar": return View(await _context.Conciertos.OrderBy(s => s.Lugar).ToListAsync());
                case "Lugar_desc": return View(await _context.Conciertos.OrderByDescending(s => s.Lugar).ToListAsync());
                case "Titulo": return View(await _context.Conciertos.OrderBy(s => s.Titulo).ToListAsync());
                case "Titulo_desc": return View(await _context.Conciertos.OrderByDescending(s => s.Titulo).ToListAsync());
                case "Precio": return View(await _context.Conciertos.OrderBy(s => s.Precio).ToListAsync());
                case "Precio_desc": return View(await _context.Conciertos.OrderByDescending(s => s.Precio).ToListAsync());
            }
            return View(await _context.Conciertos.ToListAsync());
        }

        public async Task<IActionResult> IndexConcierto()
        {
            var conciertos = _context.Conciertos;
            var filtrado = from concierto in conciertos
                where concierto.Precio > 30 && ((DateTime)concierto.Fecha).Year > 2015
                select concierto;

            return View(await filtrado.ToListAsync());
        }

        // GET: Conciertoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.Conciertos
                .FirstOrDefaultAsync(m => m.Id == id);
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
                _context.Add(concierto);
                await _context.SaveChangesAsync();
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

            var concierto = await _context.Conciertos.FindAsync(id);
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
                    _context.Update(concierto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertoExists(concierto.Id))
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

            var concierto = await _context.Conciertos
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var concierto = await _context.Conciertos.FindAsync(id);
            if (concierto != null)
            {
                _context.Conciertos.Remove(concierto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConciertoExists(int id)
        {
            return _context.Conciertos.Any(e => e.Id == id);
        }
    }
}
