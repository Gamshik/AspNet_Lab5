using Entities.Base;
using Entities.Pagination;

namespace Contracts.Services.Base
{
    public interface IBaseEntityService<TDb, TDto>
        where TDb : EntityBase
        where TDto : EntityBaseDto
    {
        PagedList<TDto> GetByPage(PaginationQueryParameters parameters);
        Task<TDto> GetByIdAsync(Guid id);
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto> UpdateAsync(TDto dto);
        Task DeleteByIdAsync(Guid id);
    }
}
