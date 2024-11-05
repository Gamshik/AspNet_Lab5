using Entities;
using Entities.Models.DTOs.User;
namespace Contracts.Services
{
    public interface IAuthService
    {
        Task<Jwt?> AuthorizeAsync(UserAuthorizationDto userAuthorizationDto, CancellationToken cancellationToken = default);
        Task<bool> RegisterAsync(UserRegistrationDto userRegistrationDto, IEnumerable<string> roles, CancellationToken cancellationToken = default);
    }
}
