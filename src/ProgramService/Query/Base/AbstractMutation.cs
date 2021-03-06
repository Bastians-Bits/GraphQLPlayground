using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgramService.Database;

namespace ProgramService.Query.Base
{
    public abstract class AbstractMutation<TInput, TEntity> where TEntity : class
	{
        protected async Task<IList<TInput>> Add (
            ProgramDbContext context,
            IMapper mapper,
            IList<TInput> inputs)
        {
            var entities = mapper.Map<IList<TEntity>>(inputs);
            context.Set<TEntity>().AddRange(entities);
            await context.SaveChangesAsync();
            return mapper.Map<IList<TInput>>(entities);
        }

        protected async Task<IList<TInput>> Edit (
            ProgramDbContext context,
            IMapper mapper,
            IList<TInput> inputs)
        {
            var entities = mapper.Map<IList<TEntity>>(inputs);

            var pk = context.Model.FindEntityType(typeof(TEntity))?
                .FindPrimaryKey();

            if (pk == null || pk.Properties == null)
                throw new Exception($"Could not load pk of type {typeof(TEntity)}");

            foreach (var entity in entities)
            {
                int count = context.Set<TEntity>()
                    .WithPk(entity, pk.Properties, false)
                    .Count();

                if (count == 1)
                {
                    // update
                    context.Set<TEntity>().Update(entity);
                }
                else
                {
                    throw new Exception(
                        $"Entity does not exists or more than once"
                    );
                }
            }

            await context.SaveChangesAsync();
            return mapper.Map<IList<TInput>>(entities);
        }

        protected async Task<IList<TInput>> Delete (
            ProgramDbContext context,
            IMapper mapper,
            IList<TInput> inputs)
        {
            var entities = mapper.Map<IList<TEntity>>(inputs);

            var pk = context.Model.FindEntityType(typeof(TEntity))?
                .FindPrimaryKey();

            if (pk == null || pk.Properties == null)
                throw new Exception($"Could not load pk of type {typeof(TEntity)}");

            foreach (var entity in entities)
            {
                int count = context.Set<TEntity>()
                    .WithPk(entity, pk.Properties, false)
                    .Count();

                if (count == 1)
                {
                    // update
                    context.Set<TEntity>().Remove(entity);
                }
                else
                {
                    throw new Exception(
                        $"Entity does not exists or more than once"
                    );
                }
            }

            await context.SaveChangesAsync();
            return mapper.Map<IList<TInput>>(entities);
        }
    }

    public static class DbSetWithPk
    {
        /// <summary>
        /// Load a .where with all the primar keys for the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity. Has to be known by the databae context</typeparam>
        /// <param name="entities">The DbSet the query is loaded for</param>
        /// <param name="entity">The entity the query is loaded for</param>
        /// <param name="properties">The IProperty-List with the primary key fields. Can be loaded through the db context</param>
        /// <param name="queryNull">If null values should be added to the query</param>
        /// <returns></returns>
        public static IQueryable<TEntity> WithPk<TEntity> (
            this DbSet<TEntity> entities,
            TEntity entity,
            IReadOnlyList<Microsoft.EntityFrameworkCore.Metadata.IProperty> properties,
            bool queryNull)
            where TEntity : class
        {
            // We could use multiple .where(...), but that would have serious performance impacts
            string query = string.Empty;
            foreach (Microsoft.EntityFrameworkCore.Metadata.IProperty property in properties)
            {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                object id = entity.GetType().GetProperty(property.Name).GetValue(entity);
                #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                #pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (queryNull && id == null)
                    continue;

                if (query.Length == 0)
                    // Construct a new string
                    query = $"w.{property.Name} == \"{id}\"";
                else
                    // Append to an existing one
                    query += $" && w.{property.Name} == \"{id}\"";
            }
            return entities.Where($"w => {query}");
        }
    }
}

