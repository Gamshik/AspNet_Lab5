using Contracts.Repositories;
using DbAccess.Context;
using DbAccess.Repositories.Base;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DbAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(LogisticContext context) : base(context) { }

        public override IQueryable<User> GetAllWithDependencies() =>
            _context.Users
                .AsNoTracking();
    }
}
