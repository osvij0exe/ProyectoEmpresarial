﻿using Clinica.AccesoADatos;
using Clinica.Entities;
using Clinica.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Implementacion
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private readonly ClinicaDbContext Context;

        public RepositoryBase(ClinicaDbContext context)
        {
            Context = context;
        }

        public async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {

                var query = Context.Set<TEntity>()
                    .AsQueryable();

            if (predicate is not null)
                query = query.Where(predicate);

            return await query
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<ICollection<TInfo>> ListAsync<TInfo>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TInfo>> selector,
            string? relationships = null)
        {
            var query = Context.Set<TEntity>()
                .Where(predicate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(relationships))
            {
                foreach (var tabla in relationships.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(tabla);
                }
            }
            return await query
                .Select(selector)
                .ToListAsync();
        }

        public async Task<TEntity?> FindByIdAsync(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);

            if (entity is not null)
            {
                return entity;
            }
            else
            {
                throw new InvalidOperationException($" No se econtro ningún registro con id: {id}");
            }

        }

        public async Task<TInfo> FindByIdAsync<TInfo>(
                  Expression<Func<TEntity, bool>> predicate,
                  Expression<Func<TEntity, TInfo>> selector,
                  string? relationships = null)
        {
            var query = Context.Set<TEntity>()
                .Where(predicate)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(relationships))
            {
                foreach (var tabla in relationships.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(tabla);
                }
            }

            var existecampo = await query.AnyAsync(predicate);
            if (!existecampo)
            {
                throw new InvalidOperationException($" No se econtro ningún registro ");

            }
            else
            {

                return await query
                    .Select(selector)
                    .FirstAsync();
            }

        }



        public async Task<int> AddAsync(TEntity entity)
        {

            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity.Id;
        }


        public async Task UpdateAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);

            if (entity != null)
            {
                entity.Estado = false;
                await UpdateAsync();
            }
            else
            {
                throw new InvalidOperationException($" No se econtro ningún registro con id: {id}");
            }
        }

        public async Task<ICollection<TInfo>> ListAsync<TInfo>(Expression<Func<TEntity, TInfo>> selector)
        {

            return await Context.Set<TEntity>()
                .AsQueryable()
                .Select(selector)
                .ToListAsync();

        }

        public async Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TInfo>> selector,
            Expression<Func<TEntity, TKey>> orderBy,
            string? relationships,
            int page, int rows)
        {
            var query = Context.Set<TEntity>()
                .Where(predicate)
                .AsQueryable();




            //relación entre tablas
            if (!string.IsNullOrWhiteSpace(relationships))
            {
                foreach (var tabla in relationships.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(tabla);
                }
            }

            var collection = await query.OrderBy(orderBy)
                .Skip((page - 1) * rows)
                .Take(rows)
                .Select(selector)
                .ToListAsync();

            //cantidad de registros listados
            var total = await Context.Set<TEntity>()
                .Where(predicate)
                .CountAsync();

            return (collection, total);
        }

        public async Task Reactivar(int id)
        {
            var registro = await Context.Set<TEntity>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);

            registro!.Estado = true;
            await UpdateAsync();

        }

        public async Task<ICollection<TEntity>> ListarEliminados()
        {
            var registro = await Context.Set<TEntity>()
                .IgnoreQueryFilters()
                .Where(p => !p.Estado)
                .AsNoTracking()
                .ToListAsync();

            if (registro.IsNullOrEmpty())
            {
                throw new InvalidOperationException("No se encontraron registros");
            }
            else
            {
                return registro;

            }

        }

        public async Task<int> AddAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            var query = Context.Set<TEntity>()
                .AsQueryable();

            var existeCampo = await query.AnyAsync(predicate);

            if (existeCampo)
            {
                throw new InvalidOperationException($"Ya se encuentra un registro");
            }
            else
            {
                await Context.Set<TEntity>().AddAsync(entity);
                await Context.SaveChangesAsync();
                return entity.Id;

            }


        }


    }
}
