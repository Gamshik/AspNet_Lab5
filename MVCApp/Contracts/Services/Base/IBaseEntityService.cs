using Entities.Base;
using Entities.Pagination;

namespace Contracts.Services.Base
{
    public interface IBaseEntityService<TDb, TDto>
        where TDb : EntityBase
        where TDto : class
    {
        PagedList<TDto> GetByPage(PaginationQueryParameters parameters);
    }
}
