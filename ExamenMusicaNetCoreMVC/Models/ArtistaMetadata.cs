using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.Models
{
    [ModelMetadataType(typeof(ArtistaMetadata))]
    public partial class Artista { }
    public class ArtistaMetadata
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string? Nombre { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string? Genero { get; set; }
        [Required]
        [RegularExpression("([0-9]{2})([/])([0-9]{2})([/])([0-9]{4})", ErrorMessage = "Fecha invalida")]
        public DateOnly? FechaNac { get; set; }
    }
}
