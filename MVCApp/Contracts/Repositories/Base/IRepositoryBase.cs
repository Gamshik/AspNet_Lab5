﻿using Entities.Base;
using System.Linq.Expressions;

namespace Contracts.Repositories.Base
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        int Count();
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition);
        Task<T> FindByIdAsync(Guid id);
        IQueryable<T> GetAllWithDependencies();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(Guid id);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
