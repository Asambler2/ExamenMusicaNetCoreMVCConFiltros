using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Composition;

namespace ExamenMusicaNetCoreMVC.Models
{
    [ModelMetadataType(typeof(AlbumeMetaData))]
    public partial class Albume {  }
 
    public class AlbumeMetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression("([0-9]{2})([/])([0-9]{2})([/])([0-9]{4})", ErrorMessage = "Fecha invalida")]
        public DateOnly? Fecha { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        public string? Genero { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string? Titulo { get; set; }
       
        public int? GruposId { get; set; }

    }
}
