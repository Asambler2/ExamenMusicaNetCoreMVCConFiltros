using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Models;
using ExamenMusicaNetCoreMVC.Servicios.RepositorioGenerico;
using ExamenMusicaNetCoreMVC.ViewModels.ModelGrupoConciertos;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class ConciertosGrupoesController : Controller
    {
        private readonly IRepositorioGenerico<ConciertosGrupo> _context;
        private readonly IRepositorioGenerico<Concierto> _conciertoContext;
        private readonly IRepositorioGenerico<Grupo> _grupoContext;

        public ConciertosGrupoesController(IRepositorioGenerico<ConciertosGrupo> context, IRepositorioGenerico<Concierto> conciertoContext, IRepositorioGenerico<Grupo> grupoContext)
        {
            this._context = context;
            this._conciertoContext = conciertoContext;
            this._grupoContext = grupoContext;
        }

        // GET: ConciertosGrupoes
        public async Task<IActionResult> Index()
        {
            IListaGruposConciertos ListaGrupoConcierto = new ListaGrupoConcierto(_context, _conciertoContext, _grupoContext);
            return View(ListaGrupoConcierto.DevolverListaGrupoConcierto());
        }

        // GET: ConciertosGrupoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupo = _context.DameUno((int)id);
  
            if (conciertosGrupo == null)
            {
                return NotFound();
            }

            return View(conciertosGrupo);
        }

        // GET: ConciertosGrupoes/Create
        public IActionResult Create()
        {
            ViewData["ConciertosId"] = new SelectList(_conciertoContext.DameTodos(), "Id", "Lugar");
            ViewData["GruposId"] = new SelectList(_grupoContext.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: ConciertosGrupoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GruposId,ConciertosId")] ConciertosGrupo conciertosGrupo)
        {
            if (ModelState.IsValid)
            {
                _context.Agregar(conciertosGrupo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConciertosId"] = new SelectList(_conciertoContext.DameTodos(), "Id", "Lugar", conciertosGrupo.ConciertosId);
            ViewData["GruposId"] = new SelectList(_grupoContext.DameTodos(), "Id", "Nombre", conciertosGrupo.GruposId);
            return View(conciertosGrupo);
        }

        // GET: ConciertosGrupoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupo = _context.DameUno((int)id);
            if (conciertosGrupo == null)
            {
                return NotFound();
            }
            ViewData["ConciertosId"] = new SelectList(_conciertoContext.DameTodos(), "Id", "Lugar", conciertosGrupo.ConciertosId);
            ViewData["GruposId"] = new SelectList(_grupoContext.DameTodos(), "Id", "Nombre", conciertosGrupo.GruposId);
            return View(conciertosGrupo);
        }

        // POST: ConciertosGrupoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GruposId,ConciertosId")] ConciertosGrupo conciertosGrupo)
        {
            if (id != conciertosGrupo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Modificar((int)id, conciertosGrupo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertosGrupoExists(conciertosGrupo.Id))
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
            ViewData["ConciertosId"] = new SelectList(_conciertoContext.DameTodos(), "Id", "Lugar", conciertosGrupo.ConciertosId);
            ViewData["GruposId"] = new SelectList(_grupoContext.DameTodos(), "Id", "Nombre", conciertosGrupo.GruposId);
            return View(conciertosGrupo);
        }

        // GET: ConciertosGrupoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupo = _context.DameUno((int)id);
            
            if (conciertosGrupo == null)
            {
                return NotFound();
            }

            return View(conciertosGrupo);
        }

        // POST: ConciertosGrupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conciertosGrupo = _context.DameUno((int)id);
            if (conciertosGrupo != null)
            {
                _context.Borrar((int)id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ConciertosGrupoExists(int id)
        {
            return _context.DameTodos().Any(e => e.Id == id);
        }
    }
}
