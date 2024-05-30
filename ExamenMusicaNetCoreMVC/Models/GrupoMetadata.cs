using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.Models
{
    [ModelMetadataType(typeof(GrupoMetadata))]
    public partial class Grupo { }
    public class GrupoMetadata
    {
        [Required]
        public int Id { get; set; }
        [MinLength(4)]
        [MaxLength(15)]
        [Required]
        public string? Nombre { get; set; }


    }
}
