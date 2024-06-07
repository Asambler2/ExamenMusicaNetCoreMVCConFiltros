using System;
using System.Collections.Generic;

namespace ExamenMusicaNetCoreMVC.Models;

public partial class ListasCancione
{
    public int Id { get; set; }

    public int? ListasId { get; set; }

    public int? CancionesId { get; set; }

    public virtual Cancione? Canciones { get; set; }

    public virtual Lista? Listas { get; set; }
}
