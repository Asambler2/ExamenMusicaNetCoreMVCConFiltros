using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.Models;

public partial class GruposArtista
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int? ArtistasId { get; set; }
    [Required]
    public int? GruposId { get; set; }

    public virtual Artista? Artistas { get; set; }

    public virtual Grupo? Grupos { get; set; }
}
