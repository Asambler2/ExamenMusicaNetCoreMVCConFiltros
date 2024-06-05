using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenMusicaNetCoreMVC.Models
{
    [ModelMetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario { }
    public class UsuarioMetadata
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string? Nombre { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{2,5}", ErrorMessage = "Email invalida")]
        public string? Email { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string? Contraseña { get; set; }
    }
}
