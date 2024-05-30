﻿using System;
using System.Collections.Generic;

namespace ExamenMusicaNetCoreMVC.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public string? Contraseña { get; set; }

    public virtual ICollection<Lista> Lista { get; set; } = new List<Lista>();
}
