using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.ViewModels
{
    public class ConciertoGroupByGrupos
    {
        public string NombreGrupo { get; set; }
        public List<Concierto> ListaConciertos { get; set; }
    }

    public class Concierto
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string Grupo { get; set; }
        public string Genero { get; set; }
        public string Lugar { get; set; }
        public string Titulo { get; set; }
        public decimal? Precio { get; set; }
    }
}
