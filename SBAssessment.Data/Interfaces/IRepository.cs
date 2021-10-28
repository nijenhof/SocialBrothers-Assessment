using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SBAssessment.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        Task AddAsync(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);
    }
}
