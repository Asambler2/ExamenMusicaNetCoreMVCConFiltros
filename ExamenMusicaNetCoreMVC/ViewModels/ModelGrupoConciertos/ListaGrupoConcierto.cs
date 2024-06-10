
using ExamenMusicaNetCoreMVC.Models;
using ExamenMusicaNetCoreMVC.Servicios.RepositorioGenerico;

namespace ExamenMusicaNetCoreMVC.ViewModels.ModelGrupoConciertos
{

    public class ListaGrupoConcierto : IListaGruposConciertos
    {
        private readonly IRepositorioGenerico<ConciertosGrupo> _context;
        private readonly IRepositorioGenerico<Concierto> _conciertoContext;
        private readonly IRepositorioGenerico<Grupo> _grupoContext;

        public ListaGrupoConcierto(IRepositorioGenerico<ConciertosGrupo> context, IRepositorioGenerico<Concierto> conciertoContext, IRepositorioGenerico<Grupo> grupoContext)
        {
            this._context = context;
            this._conciertoContext = conciertoContext;
            this._grupoContext = grupoContext;
        }
        public async Task<List<GrupoConciertoUnion>> DevolverListaGrupoConcierto()
        {
            var Lista = from c in await _conciertoContext.DameTodos()
                join cg in await _context.DameTodos() on c.Id equals cg.ConciertosId
                join g in await _grupoContext.DameTodos() on cg.GruposId equals g.Id
                select new GrupoConciertoUnion()
                {
                    Id = cg.Id,
                    GruposId = cg.GruposId,
                    ConciertosId = cg.ConciertosId,
                    Titulo = c.Titulo,
                    Nombre = g.Nombre,
                };
                
            return Lista.ToList();
        }
    }
}
