﻿using System;
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
    public class ListasController : Controller
    {
        private readonly IRepositorioGenerico<Lista> _contextLista;
        private readonly IRepositorioGenerico<Usuario> _contextUsuario;

        public ListasController(IRepositorioGenerico<Lista> contextLista, IRepositorioGenerico<Usuario> contextUsuario)
        {
            _contextLista = contextLista;
            _contextUsuario = contextUsuario;
        }

        // GET: Listas
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var examenMusicaNetCoreMVCContext = _contextLista;

            ViewData["OrdenNombre"] = sortOrder == "Nombre" ? "Nombre_desc" : "Nombre";


            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                return View((await examenMusicaNetCoreMVCContext.DameTodos()).Where(s => s.Nombre.Contains(searchString)));
            }

            switch (sortOrder)
            {
                case "Nombre": return View((await examenMusicaNetCoreMVCContext.DameTodos()).OrderBy(s => s.Nombre));
                case "Nombre_desc": return View((await examenMusicaNetCoreMVCContext.DameTodos()).OrderByDescending(s => s.Nombre));
            }
            return View(await examenMusicaNetCoreMVCContext.DameTodos());
        }

        // GET: Listas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _contextLista.DameUno((int)id);
            if (lista == null)
            {
                return NotFound();
            }

            return View(lista);
        }

        // GET: Listas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UsuarioId"] = new SelectList(await _contextUsuario.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Listas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,UsuarioId")] Lista lista)
        {
            if (ModelState.IsValid)
            {
                await _contextLista.Agregar(lista);
  
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(await _contextLista.DameTodos(), "Id", "Nombre", lista.UsuarioId);
            return View(lista);
        }

        // GET: Listas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _contextLista.DameUno((int)id);
            if (lista == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(await _contextUsuario.DameTodos(), "Id", "Nombre", lista.UsuarioId);
            return View(lista);
        }

        // POST: Listas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,UsuarioId")] Lista lista)
        {
            if (id != lista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextLista.Modificar((int)id, lista);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await ListaExists(lista.Id)))
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
            ViewData["UsuarioId"] = new SelectList(await _contextUsuario.DameTodos(), "Id", "Nombre", lista.UsuarioId);
            return View(lista);
        }

        // GET: Listas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lista = await _contextLista.DameUno((int)id);

            if (lista == null)
            {
                return NotFound();
            }

            return View(lista);
        }

        // POST: Listas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lista = await _contextLista.DameUno((int)id);
            if (lista != null)
            {
                await _contextLista.Borrar((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ListaExists(int id)
        {
            var datos = await _contextLista.DameTodos();
            return datos.Any(e => e.Id == id);
        }
    }
}
