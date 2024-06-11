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
    public class ListasCancionesController : Controller
    {
        private readonly IRepositorioGenerico<ListasCancione> _contextListaCancion;
        private readonly IRepositorioGenerico<Lista> _contextLista;
        private readonly IRepositorioGenerico<Cancione> _contextCancion;
        private readonly IRepositorioGenerico<VistaListaCancione> _contexVistatListaCancion;

        public ListasCancionesController(IRepositorioGenerico<ListasCancione> contextListaCancion, IRepositorioGenerico<Lista> contextLista, 
            IRepositorioGenerico<Cancione> contextCancion, IRepositorioGenerico<VistaListaCancione> contexVistatListaCancion)
        {
            this._contextListaCancion = contextListaCancion;
            this._contextLista = contextLista;
            this._contextCancion = contextCancion;
            this._contexVistatListaCancion = contexVistatListaCancion;
        }

        // GET: ListasCanciones
        public async Task<IActionResult> Index()
        {
            return View(await _contexVistatListaCancion.DameTodos());
        }

        // GET: ListasCanciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listasCancione = await _contextListaCancion.DameUno((int)id)
 
            if (listasCancione == null)
            {
                return NotFound();
            }

            return View(listasCancione);
        }

        // GET: ListasCanciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CancionesId"] = new SelectList(await _contextCancion.DameTodos(), "Id", "Titulo");
            ViewData["ListasId"] = new SelectList(await _contextLista.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: ListasCanciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ListasId,CancionesId")] ListasCancione listasCancione)
        {
            if (ModelState.IsValid)
            {
                await _contextListaCancion.Agregar(listasCancione);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CancionesId"] = new SelectList(await _contextCancion.DameTodos(), "Id", "Titulo", listasCancione.CancionesId);
            ViewData["ListasId"] = new SelectList(await _contextLista.DameTodos(), "Id", "Nombre", listasCancione.ListasId);
            return View(listasCancione);
        }

        // GET: ListasCanciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listasCancione = await _contextListaCancion.DameUno((int)id);
            if (listasCancione == null)
            {
                return NotFound();
            }
            ViewData["CancionesId"] = new SelectList(await _contextCancion.DameTodos(), "Id", "Titulo", listasCancione.CancionesId);
            ViewData["ListasId"] = new SelectList(await _contextLista.DameTodos(), "Id", "Nombre", listasCancione.ListasId);
            return View(listasCancione);
        }

        // POST: ListasCanciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ListasId,CancionesId")] ListasCancione listasCancione)
        {
            if (id != listasCancione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextListaCancion.Modificar((int)id, listasCancione);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListasCancioneExists(listasCancione.Id))
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
            ViewData["CancionesId"] = new SelectList(await _contextCancion.DameTodos(), "Id", "Titulo", listasCancione.CancionesId);
            ViewData["ListasId"] = new SelectList(await _contextLista.DameTodos(), "Id", "Nombre", listasCancione.ListasId);
            return View(listasCancione);
        }

        // GET: ListasCanciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listasCancione = await _contextListaCancion.DameUno((int)id);

            if (listasCancione == null)
            {
                return NotFound();
            }

            return View(listasCancione);
        }

        // POST: ListasCanciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listasCancione = await _contextListaCancion.DameUno((int)id);
            if (listasCancione != null)
            {
                await _contextListaCancion.Borrar((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ListasCancioneExists(int id)
        {
            var datos = await _contextListaCancion.DameTodos();
            return datos.Any(e => e.Id == id);
        }
    }
}
