using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.Models;

public partial class CancionesConcierto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int? CancionesId { get; set; }
    [Required]
    public int? ConciertosId { get; set; }

    public virtual Cancione? Canciones { get; set; }

    public virtual Concierto? Conciertos { get; set; }
}
