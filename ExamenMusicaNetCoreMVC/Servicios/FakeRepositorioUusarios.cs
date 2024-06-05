using System.Xml.Linq;
using ExamenMusicaNetCoreMVC.Models;

namespace ExamenMusicaNetCoreMVC.Servicios
{
    public class FakeRepositorioUusarios : IRepositorioUsuarios
    {
  
        private List<Usuario> listaUsuarios = new();

        public FakeRepositorioUusarios()
        {
            Usuario miUsuario= new()
            {
                Id = 1,
                Nombre = "paco",
                Email = "paco@paco.com",
                Contraseña = "1234"
            };
            listaUsuarios.Add(miUsuario);
            miUsuario = new()
            {
                Id = 1,
                Nombre = "juan",
                Email = "juan@juan.com",
                Contraseña = "4567"
            };
            listaUsuarios.Add(miUsuario);
            miUsuario = new()
            {
                Id = 1,
                Nombre = "fernanda",
                Email = "fernanda@fernada.com",
                Contraseña = "13579"
            };
            listaUsuarios.Add(miUsuario);
            miUsuario = new()
            {
                Id = 1,
                Nombre = "manolo",
                Email = "manolo@manolo.com",
                Contraseña = "2468"
            };
            listaUsuarios.Add(miUsuario);
        }
        public List<Usuario> DameTodos()
        {
            return this.listaUsuarios;
        }

        public Usuario? DameUno(int Id)
        {
            return this.listaUsuarios.FirstOrDefault(x => x.Id == Id);
        }

        public bool BorrarUsuario(int Id)
        {
            return listaUsuarios.Remove(DameUno(Id));
        }

        public bool Agregar(Usuario product)
        {
            this.listaUsuarios.Add(product);
            return true;
        }

        public void Modificar(int Id, Usuario product)
        {
            BorrarUsuario(Id);
            Agregar(product);
        }
    }
}
