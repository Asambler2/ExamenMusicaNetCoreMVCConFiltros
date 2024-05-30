using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamenMusicaNetCoreMVC.Models;

public partial class ListasCancione
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int? ListasId { get; set; }
    [Required]
    public int? CancionesId { get; set; }

    public virtual Cancione? Canciones { get; set; }

    public virtual Lista? Listas { get; set; }
}
