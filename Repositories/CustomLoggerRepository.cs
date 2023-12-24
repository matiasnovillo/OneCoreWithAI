using Microsoft.EntityFrameworkCore;
using OneCore.Entities;
using System.Text.RegularExpressions;

namespace OneCore.Repositories
{
    public class CustomLoggerRepository
    {
        protected readonly Entities.Configuration.EFCoreContext _context;

        public CustomLoggerRepository(Entities.Configuration.EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<CustomLogger> AsQueryable()
        {
            return _context.CustomLogger.AsQueryable();
        }
        public async Task<bool> Add(CustomLogger customLogger, CancellationToken cancellationToken)
        {
            await _context.CustomLogger.AddAsync(customLogger, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public List<CustomLogger?> GetAll(CancellationToken cancellationToken)
        {
            List<CustomLogger> lstCustomLogger = new();

            var GetAllQuery =
                    from x in _context.CustomLogger
                    select new
                    {
                        x.CustomLoggerId,
                        x.InteractionType,
                        x.Description,
                        x.DateTime,
                        x.UserId
                    };

            foreach (var x in GetAllQuery)
            {
                CustomLogger customLogger = new CustomLogger()
                {
                    CustomLoggerId = x.CustomLoggerId,
                    InteractionType = x.InteractionType,
                    Description = x.Description,
                    DateTime = x.DateTime,
                    UserId = x.UserId
                };
                lstCustomLogger.Add(customLogger);
            }

            return lstCustomLogger;
        }

        public List<CustomLogger?> GetAllByDescription(string textToSearch, bool strictSearch, CancellationToken cancellationToken)
        {
            //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
            string[] words = Regex.Replace(textToSearch.Trim(), @"\s+", " ").Split(" ");
            
            List<CustomLogger> lstCustomLogger = new();

            var GetAllQuery = AsQueryable()
                .Where(x => strictSearch ?
                    words.All(word => x.Description.Contains(word)) :
                    words.Any(word => x.Description.Contains(word)))
                .ToList();

            foreach (var x in GetAllQuery)
            {
                CustomLogger customLogger = new CustomLogger()
                {
                    CustomLoggerId = x.CustomLoggerId,
                    InteractionType = x.InteractionType,
                    Description = x.Description,
                    DateTime = x.DateTime,
                    UserId = x.UserId
                };
                lstCustomLogger.Add(customLogger);
            }

            return lstCustomLogger;
        }

        public List<CustomLogger?> GetAllByInteractionType(string textToSearch, bool strictSearch, CancellationToken cancellationToken)
        {
            //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
            string[] words = Regex.Replace(textToSearch.Trim(), @"\s+", " ").Split(" ");

            List<CustomLogger> lstCustomLogger = new();

            var GetAllQuery = AsQueryable()
                .Where(x => strictSearch ?
                    words.All(word => x.InteractionType.Contains(word)) :
                    words.Any(word => x.InteractionType.Contains(word)))
                .ToList();

            foreach (var x in GetAllQuery)
            {
                CustomLogger customLogger = new CustomLogger()
                {
                    CustomLoggerId = x.CustomLoggerId,
                    InteractionType = x.InteractionType,
                    Description = x.Description,
                    DateTime = x.DateTime,
                    UserId = x.UserId
                };
                lstCustomLogger.Add(customLogger);
            }

            return lstCustomLogger;
        }
    }
}
