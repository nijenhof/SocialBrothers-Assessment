using Microsoft.EntityFrameworkCore;
using SBAssessment.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SBAssessment.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public DbContextBase Context { get; }
        protected DbSet<TEntity> Entities { get; }
        public Repository(DbContextBase context)
        {
            Context = context;
            Entities = Context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Entities.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return Entities.AsQueryable();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            Entities.Add(entity);
            Context.SaveChanges();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
            Context.SaveChanges();
        }
        public void Remove(int id)
        {
            TEntity? entity = Entities.Find(id);

            if (entity != null)
            {
                Entities.Remove(entity);
                Context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            Entities.Update(entity);
            Context.SaveChanges();
        }
        public void Update(int id, TEntity entity)
        {
            TEntity? existingEntity = Entities.Find(id);
            if (existingEntity == null) return;

            Context.Entry(existingEntity).CurrentValues.SetValues(entity);

            Context.SaveChanges();
        }
    }
}
