using System.Drawing;
using ExamenMusicaNetCoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenMusicaNetCoreMVC.Servicios
{
    public class EFUsuarioRepositorio : IRepositorioUsuarios
    {
        private readonly GrupoCContext _context = new();
        public List<Usuario> DameTodos()
        {
            return _context.Usuarios.AsNoTracking().ToList();

        }

        public Usuario? DameUno(int Id)
        {
            return _context.Usuarios.Find(Id);
        }

        public bool BorrarUsuario(int Id)
        {
            if (DameUno(Id) != null)
            {
                _context.Usuarios.Remove(DameUno(Id));
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Agregar(Usuario usuario)
        {
            if (DameUno(usuario.Id) != null)
            {

                return false;
            }
            else
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return true;
            }
        }

        public void Modificar(int Id, Usuario usuario)
        {
            var recupera = DameUno(Id);
            if (recupera != null)
            {
                BorrarUsuario(Id);
            }
            Agregar(usuario);
        }
    }
}  

