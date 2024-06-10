

using ExamenMusicaNetCoreMVC.Models;
using ExamenMusicaNetCoreMVC.Servicios.RepositorioGenerico;

namespace ExamenMusicaNetCoreMVC.ViewModels
{
    public class ListaGruposConListaDeConciertos : IListaGruposConListaDeConciertos
    {
        private readonly IRepositorioGenerico<Concierto> _contextoConciertos;
        private readonly IRepositorioGenerico<Grupo> _contexotGrupos;
        private readonly IRepositorioGenerico<ConciertosGrupo> _contextoConciertoGrupo;
        private readonly IListaConciertosPorGrupo builder;
        public ListaGruposConListaDeConciertos(IRepositorioGenerico<Grupo> contextoGrupos, IRepositorioGenerico<Concierto> contextoConciertos, IRepositorioGenerico<ConciertosGrupo> contextoConciertoGrupo, IListaConciertosPorGrupo builder)
        {
            this._contexotGrupos = contextoGrupos;
            this._contextoConciertos = contextoConciertos;
            this._contextoConciertoGrupo = contextoConciertoGrupo;
            this.builder = builder;
        }

        public async Task<List<ConciertoGroupByGrupos>> DameGruposConListaConciertos()
        {
            var GruposDistintos = from g in await _contexotGrupos.DameTodos() group(g) by g.Nombre into g
            select g;

            List<ConciertoGroupByGrupos> coleccionADevolver = new();
            foreach (var grupo in GruposDistintos)
            {
            ConciertoGroupByGrupos ElementoAPoner =
                new()
                {
                NombreGrupo = grupo.Key,
                ListaConciertos = new ListaConciertosPorGrupo(this._contextoConciertos, this._contexotGrupos, this._contextoConciertoGrupo).DameListaConciertosPorGrupo(grupo.Key)
                };
            coleccionADevolver.Add(ElementoAPoner);
            }
            return coleccionADevolver;
        }
    }
}
