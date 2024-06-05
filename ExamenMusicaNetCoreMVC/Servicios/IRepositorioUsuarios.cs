using ExamenMusicaNetCoreMVC.Models;
namespace ExamenMusicaNetCoreMVC.Servicios
{
    public interface IRepositorioUsuarios
    {
        List<Usuario> DameTodos();
        Usuario? DameUno(int Id);
        bool BorrarUsuario(int Id);
        bool Agregar(Usuario usuario);
        void Modificar(int Id, Usuario usuario);
    }
}
