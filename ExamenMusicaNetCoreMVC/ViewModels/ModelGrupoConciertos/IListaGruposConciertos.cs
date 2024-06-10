namespace ExamenMusicaNetCoreMVC.ViewModels.ModelGrupoConciertos
{
    public interface IListaGruposConciertos
    {
        public Task<List<GrupoConciertoUnion>> DevolverListaGrupoConcierto();
    }
}
