using Microsoft.EntityFrameworkCore;
using OneCore.Entities;
using System.Text.RegularExpressions;

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

        public List<User?> GetAll(CancellationToken cancellationToken)
        {
            List<User> lstUser = new();

            var GetAllQuery =
                    from x in _context.User
                    select new
                    {
                        x.UserId,
                        x.Email,
                        x.Password,
                    };

            foreach (var x in GetAllQuery)
            {
                User user = new User()
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    Password = x.Password
                };
                lstUser.Add(user);
            }

            return lstUser;
        }

        public User? GetByEmailAndPassword(string email, string password, CancellationToken cancellationToken)
        {
            return _context.User.AsQueryable()
                .Where(u => u.Password == password)
                .Where(u => u.Email == email)
                .FirstOrDefault();
        }

        public List<User?> GetAllByEmail(string textToSearch, 
            bool strictSearch, 
            CancellationToken cancellationToken)
        {
            //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
            string[] words = Regex.Replace(textToSearch.Trim(), @"\s+", " ").Split(" ");

            List<User> lstUser = [];

            var GetAllQuery = AsQueryable()
                .Where(x => strictSearch ?
                    words.All(word => x.Email.Contains(word)) :
                    words.Any(word => x.Email.Contains(word)))
                .ToList();

            foreach (var x in GetAllQuery)
            {
                User user = new User()
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    Password = x.Password
                };
                lstUser.Add(user);
            }

            return lstUser;
        }

        public async Task<bool> DeleteByUserId(int userId, CancellationToken cancellationToken)
        {
            AsQueryable()
                .Where(u => u.UserId == userId)
                .ExecuteDelete();

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
