using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.Models;

public partial class ConciertosGrupo
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int? GruposId { get; set; }
    [Required]
    public int? ConciertosId { get; set; }

    public virtual Concierto? Conciertos { get; set; }

    public virtual Grupo? Grupos { get; set; }
}
