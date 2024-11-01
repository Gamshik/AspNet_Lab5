using Entities.Base;
using System.Linq.Expressions;

namespace Contracts.Repositories.Base
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition);
        IQueryable<T> GetAllWithDependencies();
        int Count();
    }
}
