using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.Models
{
    [ModelMetadataType(typeof(ListaMetadata))]
    public partial class Lista { }
    public class ListaMetadata
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string? Nombre { get; set; }
        [Required]
        public int? UsuarioId { get; set; }


    }
}
