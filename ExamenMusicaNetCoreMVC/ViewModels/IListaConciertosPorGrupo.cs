namespace ExamenMusicaNetCoreMVC.ViewModels
{
    public interface IListaConciertosPorGrupo
    {
        public Task<List<Conciertos>> DameListaConciertosPorGrupo(string Grupo);
    }
}
