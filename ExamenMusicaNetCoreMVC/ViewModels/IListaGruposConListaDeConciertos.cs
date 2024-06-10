namespace ExamenMusicaNetCoreMVC.ViewModels
{
    public interface IListaGruposConListaDeConciertos
    {
        Task<List<ConciertoGroupByGrupos>> DameGruposConListaConciertos();
    }
}
