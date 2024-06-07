﻿
using ExamenMusicaNetCoreMVC.Models;

namespace ExamenMusicaNetCoreMVC.ViewModels
{
    public class ListaConciertosPorGrupo : IListaConciertosPorGrupo
    {
        private readonly GrupoCContext _context;

        public ListaConciertosPorGrupo(GrupoCContext _context)
        {
            this._context= _context;
        }
        public List<Conciertos> DameListaConciertosPorGrupo(string Grupo)
        {
            var resultado =
                from c in this._context.Conciertos
                join cg in this._context.ConciertosGrupos
                    on c.Id equals cg.ConciertosId
                    join g in _context.Grupos
                    on cg.GruposId equals g.Id
                where g.Nombre.Equals(Grupo)
                select new Conciertos()
                {
                    Id = c.Id,
                    Fecha = c.Fecha == null ? DateTime.MinValue : c.Fecha,
                    Grupo = g.Nombre,
                    Genero = c.Genero,
                    Lugar = c.Lugar,
                    Titulo = c.Titulo,
                    Precio = c.Precio == null ? 0 : c.Precio
                };
            return resultado.ToList();
        }
    }
}
