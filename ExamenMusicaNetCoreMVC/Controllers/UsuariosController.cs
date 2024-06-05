using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Models;
using ExamenMusicaNetCoreMVC.Servicios;
using Microsoft.Data.SqlClient;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IRepositorioUsuarios _context;

        public UsuariosController(IRepositorioUsuarios context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            //ViewData["OrdenNombre"] = sortOrder == "Nombre" ? "Nombre_desc" : "Nombre";
            //ViewData["OrdenEmail"] = sortOrder == "Email" ? "Email_desc" : "Email";
            //ViewData["OrdenPass"] = sortOrder == "Pass" ? "Pass_desc" : "Pass";

            //ViewData["CurrentFilter"] = searchString;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    return View(_context.DameTodos().Where(s => s.Nombre.Contains(searchString)).ToList());
            //}

            //switch (sortOrder)
            //{
            //    case "Pass": return View( _context.DameTodos().OrderBy(s => s.Contraseña).ToList());
            //    case "Pass_desc": return View(_context.DameTodos().OrderByDescending(s => s.Contraseña).ToList());
            //    case "Email": return View(_context.DameTodos().OrderBy(s => s.Email).ToList());
            //    case "Email_desc": return View(_context.DameTodos().OrderByDescending(s => s.Email).ToList());
            //    case "Nombre": return View( _context.DameTodos().OrderBy(s => s.Nombre).ToList());
            //    case "Nombre_desc": return View(_context.DameTodos().OrderByDescending(s => s.Nombre).ToList());
            //}

            return View( _context.DameTodos().ToList());
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _context.DameUno((int)id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email,Contraseña")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Agregar(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _context.DameUno((int)id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Email,Contraseña")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Modificar((int)id, usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _context.DameUno((int)id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = _context.DameUno((int)id);
            if (usuario != null)
            {
                _context.BorrarUsuario((int)id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            if (_context.DameUno((int)id) == null)
                return false;
            else
            {
                return true;
            }
        }
    }
}
