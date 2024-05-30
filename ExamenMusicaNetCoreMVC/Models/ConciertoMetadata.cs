using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.Models
{
    [ModelMetadataType(typeof(ConciertoMetadata))]
    public partial class Concierto { }
    public class ConciertoMetadata
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime? Fecha { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        public string? Genero { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string? Lugar { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(45)]
        public string? Titulo { get; set; }
        [Required]
        [Range(0, 500)]
        //[RegularExpression("^[0-9]+([.][0-9]{2})?$", ErrorMessage = "Precio invalido")]
        public decimal? Precio { get; set; }

    }
}
