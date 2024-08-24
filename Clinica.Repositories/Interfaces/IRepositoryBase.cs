using Clinica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// ListarObjetos basados en el EntityBase
        /// </summary>
        Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? predicate = null);

        /// <summary>
        /// Lista de objetos del EntityBase con un Selector
        /// Predicate: filtros
        /// </summary>
        Task<ICollection<TInfo>> ListAsync<TInfo>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TInfo>> selector,
            string? relationships = null);

        /// <summary>
        /// Tupla Listar varios parametros con paginación
        /// </summary>
        /// <returns></returns>
        Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TInfo>> selector,
            Expression<Func<TEntity, TKey>> orderBy,
            string? relationships,
            int page, int rows);


        /// <summary>
        /// Lista de objetos del EntityBase con un Selector y sin predicado
        /// </summary>
        Task<ICollection<TInfo>> ListAsync<TInfo>(
            Expression<Func<TEntity, TInfo>> selector);

        /// <summary>
        /// Lista de objetos del EntityBase con un Selector
        /// Predicate: filtros
        /// </summary>
        Task<int> AddAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Crear un registro
        /// </summary>
        Task<int> AddAsync(TEntity entity);

        /// <summary>
        /// Buscar un registro por ID
        /// </summary>
        Task<TEntity?> FindByIdAsync(int id);

        /// <summary>
        /// Buscar un registro por ID con relacion de tablas
        /// </summary>
        Task<TInfo> FindByIdAsync<TInfo>(
                  Expression<Func<TEntity, bool>> predicate,
                  Expression<Func<TEntity, TInfo>> selector,
                  string? relationships = null);



        /// <summary>
        /// Actualizar cambios en la DB
        /// </summary>
        /// <returns></returns>
        Task UpdateAsync();



        /// <summary>
        /// Eliminar un registro de la DB
        /// </summary>
        /// 
        Task DeleteAsync(int id);

        /// <summary>
        /// Reactivar un registro en la DB
        /// </summary>
        /// <returns></returns>
        Task Reactivar(int id);

        /// <summary>
        /// Listar registros eliminados
        /// </summary>

        Task<ICollection<TEntity>> ListarEliminados();


    }
}
