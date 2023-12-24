using Microsoft.EntityFrameworkCore;
using OneCore.Entities;

namespace OneCore.Repositories
{
    public class UserRepository
    {
        protected readonly Entities.Configuration.EFCoreContext _context;

        public UserRepository(Entities.Configuration.EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<User> AsQueryable()
        {
            return _context.User.AsQueryable();
        }

        public User? GetByEmailAndPassword(string email, string password, CancellationToken cancellationToken)
        {
            return _context.User.AsQueryable()
                .Where(u => u.Password == password)
                .Where(u => u.Email == email)
                .FirstOrDefault();
        }
    }
}
