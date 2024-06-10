
using ExamenMusicaNetCoreMVC.Models;
using ExamenMusicaNetCoreMVC.Servicios.RepositorioGenerico;

namespace ExamenMusicaNetCoreMVC.ViewModels
{
    public class ListaConciertosPorGrupo : IListaConciertosPorGrupo
    {
        private readonly IRepositorioGenerico<Concierto> _contextConciertos;
        private readonly IRepositorioGenerico<Grupo> _contextGrupos;
        private readonly IRepositorioGenerico<ConciertosGrupo> _contextConciertoGrupo;

        public ListaConciertosPorGrupo(IRepositorioGenerico<Concierto> contextConciertos, IRepositorioGenerico<Grupo> contextGrupos, IRepositorioGenerico<ConciertosGrupo> contextConciertoGrupo)
        {
            this._contextConciertos = contextConciertos;
            this._contextGrupos = contextGrupos;
            this._contextConciertoGrupo= contextConciertoGrupo;
        }
        public async Task<List<Conciertos>> DameListaConciertosPorGrupo(string Grupo)
        {
            var resultado =
                from c in await _contextConciertos.DameTodos()
                join cg in await _contextConciertoGrupo.DameTodos()
                    on c.Id equals cg.ConciertosId
                    join g in await _contextGrupos.DameTodos()
                    on cg.GruposId equals g.Id
                where g.Nombre.Equals(Grupo)
                select new Conciertos()
                {
                    Id = c.Id,
                    Fecha = c.Fecha == null ? DateTime.MinValue : c.Fecha,
                    Grupo = g.Nombre,
                    Genero = c.Genero,
                    Lugar = c.Lugar,
                    Titulo = c.Titulo,
                    Precio = c.Precio == null ? 0 : c.Precio
                };
            return resultado.ToList();
        }
    }
}
