using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.Models
{
    [ModelMetadataType(typeof(CanacioneMetadata))]
    public partial class Cancione { }
    public class CanacioneMetadata
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string? Titulo { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        public string? Genero { get; set; }
        [Required]
        [RegularExpression("([0-9]{2})([/])([0-9]{2})([/])([0-9]{4})", ErrorMessage = "Fecha invalida")]
        public DateOnly? Fecha { get; set; }
        [Required]
        public int? AlbumesId { get; set; }

    }
}
