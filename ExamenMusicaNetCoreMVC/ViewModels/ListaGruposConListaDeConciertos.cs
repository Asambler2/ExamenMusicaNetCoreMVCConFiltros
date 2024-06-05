

using ExamenMusicaNetCoreMVC.Models;

namespace ExamenMusicaNetCoreMVC.ViewModels
{
    public class ListaGruposConListaDeConciertos : IListaGruposConListaDeConciertos
    {
        private readonly GrupoCContext context;
        private readonly IListaConciertosPorGrupo builder;
        public ListaGruposConListaDeConciertos(GrupoCContext context, IListaConciertosPorGrupo builder)
        {
            this.context = context;
            this.builder = builder;
        }

        public List<ConciertoGroupByGrupos> DameGruposConListaConciertos()
        {
            var GruposDistintos = from g in context.Grupos.ToList() group(g) by g.Nombre into g
            select g;

            List<ConciertoGroupByGrupos> coleccionADevolver = new();
            foreach (var grupo in GruposDistintos)
            {
            ConciertoGroupByGrupos ElementoAPoner =
                new()
                {
                NombreGrupo = grupo.Key,
                ListaConciertos = new ListaConciertosPorGrupo(this.context).DameListaConciertosPorGrupo(grupo.Key).ToList()
                };
            coleccionADevolver.Add(ElementoAPoner);
            }
            return coleccionADevolver;
        }
    }
}
