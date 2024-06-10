﻿using System.Linq.Expressions;

namespace ExamenMusicaNetCoreMVC.Servicios.RepositorioGenerico
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<List<T>> DameTodos();
        Task<T?> DameUno(int Id);
        Task<bool> Borrar(int Id);
        Task<bool> Agregar(T element);
        void Modificar(int Id, T element);
        Task<List<T>> Filtra(Expression<Func<T, bool>> predicado);
    
    }
}
