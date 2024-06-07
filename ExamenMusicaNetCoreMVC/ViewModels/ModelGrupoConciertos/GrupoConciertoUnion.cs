namespace ExamenMusicaNetCoreMVC.ViewModels.ModelGrupoConciertos
{
    public class GrupoConciertoUnion
    {
        public int Id { get; set; }
        public int? GruposId { get; set; }
        public int? ConciertosId { get; set; }
        public string? Titulo { get; set; }
        public string? Nombre { get; set; }
    }
}
