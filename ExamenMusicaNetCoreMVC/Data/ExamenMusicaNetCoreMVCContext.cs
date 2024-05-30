using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExamenMusicaNetCoreMVC.Models;

namespace ExamenMusicaNetCoreMVC.Data
{
    public class ExamenMusicaNetCoreMVCContext : DbContext
    {
        public ExamenMusicaNetCoreMVCContext (DbContextOptions<ExamenMusicaNetCoreMVCContext> options)
            : base(options)
        {
        }

        public DbSet<ExamenMusicaNetCoreMVC.Models.Albume> Albumes { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.Artista> Artistas { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.Cancione> Canciones { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.CancionesConcierto> CancionesConciertos { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.Concierto> Conciertos { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.ConciertosGrupo> ConciertosGrupos { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.Grupo> Grupos { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.GruposArtista> GruposArtistas { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.Lista> Listas { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.ListasCancione> ListasCanciones { get; set; } = default!;
        public DbSet<ExamenMusicaNetCoreMVC.Models.Usuario> Usuarios { get; set; } = default!;
    }
}
