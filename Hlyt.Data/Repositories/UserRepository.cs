using Hlyt.Data.Infrastructure;
using Hlyt.Model.Security;


namespace Hlyt.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IUserRepository : IRepository<User>
    {
    }
}
