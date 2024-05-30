using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Data;
using ExamenMusicaNetCoreMVC.Models;
using Microsoft.Data.SqlClient;

namespace ExamenMusicaNetCoreMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ExamenMusicaNetCoreMVCContext _context;

        public UsuariosController(ExamenMusicaNetCoreMVCContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["OrdenNombre"] = sortOrder == "Nombre" ? "Nombre_desc" : "Nombre";
            ViewData["OrdenEmail"] = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewData["OrdenPass"] = sortOrder == "Pass" ? "Pass_desc" : "Pass";

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await _context.Usuarios.Where(s => s.Nombre.Contains(searchString)).ToListAsync());
            }

            switch (sortOrder)
            {
                case "Pass": return View(await _context.Usuarios.OrderBy(s => s.Contraseña).ToListAsync());
                case "Pass_desc": return View(await _context.Usuarios.OrderByDescending(s => s.Contraseña).ToListAsync());
                case "Email": return View(await _context.Usuarios.OrderBy(s => s.Email).ToListAsync());
                case "Email_desc": return View(await _context.Usuarios.OrderByDescending(s => s.Email).ToListAsync());
                case "Nombre": return View(await _context.Usuarios.OrderBy(s => s.Nombre).ToListAsync());
                case "Nombre_desc": return View(await _context.Usuarios.OrderByDescending(s => s.Nombre).ToListAsync());
            }

            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email,Contraseña")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
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
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
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

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
