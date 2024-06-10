using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Models;
using ExamenMusicaNetCoreMVC.ViewModels;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Security.Policy;
using ExamenMusicaNetCoreMVC.Servicios.RepositorioGenerico;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class GrupoesController : Controller
    {
        private readonly IRepositorioGenerico<Grupo> _contextGrupo;
        public readonly IListaGruposConListaDeConciertos BuilderLista;

        public GrupoesController(IRepositorioGenerico<Grupo> contextGrupo, IListaGruposConListaDeConciertos builderLista)
        {
            _contextGrupo = contextGrupo;
            this.BuilderLista = builderLista;
        }

        // GET: Grupoes
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["OrdenNombre"] = sortOrder == "Nombre" ? "Nombre_desc" : "Nombre";

            ViewData["CurrentFilter"] = searchString;

            var listaGrupoAsync = await _contextGrupo.DameTodos();
            if (!String.IsNullOrEmpty(searchString))
            {
                var listaGrupoAsyncFiltrada = listaGrupoAsync.Where(s => s.Nombre.Contains(searchString));
                return View(listaGrupoAsyncFiltrada);
            }

            switch (sortOrder)
            {
                case "Nombre": var listGrupoNomAsyncFiltrada = listaGrupoAsync.OrderBy(s => s.Nombre);
                                return View(listGrupoNomAsyncFiltrada);
                case "Nombre_desc": var listGrupoNomDescAsyncFiltrada = listaGrupoAsync.OrderBy(s => s.Nombre);
                                return View(listGrupoNomDescAsyncFiltrada);
            }

            return View(listaGrupoAsync);
        }

        public async Task<IActionResult> ConciertosPorGrupos(string grupo = "", int page = 1, int size = 20, int total = 0)
        {
            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.Total = total;
            ViewBag.Grupo = grupo;
            return View(await this.BuilderLista.DameGruposConListaConciertos());
        }

        // GET: Grupoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _contextGrupo.DameUno((int)id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // GET: Grupoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grupoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                await _contextGrupo.Agregar(grupo);
                return RedirectToAction(nameof(Index));
            }
            return View(grupo);
        }

        // GET: Grupoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _contextGrupo.DameUno((int)id);
            if (grupo == null)
            {
                return NotFound();
            }
            return View(grupo);
        }

        // POST: Grupoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Grupo grupo)
        {
            if (id != grupo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextGrupo.Modificar((int)id, grupo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await GrupoExists(grupo.Id)))
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
            return View(grupo);
        }

        // GET: Grupoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _contextGrupo.DameUno((int)id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // POST: Grupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await _contextGrupo.DameUno(id);
            if (grupo != null)
            {
                await _contextGrupo.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GrupoExists(int id)
        {
            var datos = await _contextGrupo.DameTodos();
            return datos.Any(e => e.Id == id);
        }
    }
}
