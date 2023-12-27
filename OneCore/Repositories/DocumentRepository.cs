using Microsoft.EntityFrameworkCore;
using OneCore.Entities;
using System.Text.RegularExpressions;

namespace OneCore.Repositories
{
    public class DocumentRepository
    {
        protected readonly Entities.Configuration.EFCoreContext _context;

        public DocumentRepository(Entities.Configuration.EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<Document> AsQueryable()
        {
            return _context.Document.AsQueryable();
        }

        #region Queries
        public Document? GetById(int documentId, 
            CancellationToken cancellationToken)
        {
            return _context.Document
                .FirstOrDefault(d => d.DocumentId == documentId);
        }

        public List<Document?> GetAll(CancellationToken cancellationToken)
        {
            List<Document?> lstDocument = [];

            var GetAllQuery =
                    from x in _context.Document
                    select new
                    {
                        x.DocumentId,
                        x.FileName,
                    };

            foreach (var x in GetAllQuery)
            {
                Document document = new()
                {
                    DocumentId = x.DocumentId,
                    FileName = x.FileName,
                };
                lstDocument.Add(document);
            }

            return lstDocument;
        }

        public List<Document?> GetAllByFileName(string textToSearch,
            bool strictSearch,
            CancellationToken cancellationToken)
        {
            //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
            string[] words = Regex
                .Replace(textToSearch
                .Trim(), @"\s+", " ")
                .Split(" ");

            List<Document?> lstDocument = [];

            var GetAllQuery = AsQueryable()
                .Where(x => strictSearch ?
                    words.All(word => x.FileName.Contains(word)) :
                    words.Any(word => x.FileName.Contains(word)))
                .ToList();

            foreach (var x in GetAllQuery)
            {
                Document document = new()
                {
                    DocumentId = x.DocumentId,
                    FileName = x.FileName
                };
                lstDocument.Add(document);
            }

            return lstDocument;
        }
        #endregion

        #region Non-Queries
        public async Task<bool> Add(Document document, 
            CancellationToken cancellationToken)
        {
            await _context.Document
                .AddAsync(document, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> Update(Document document, 
            CancellationToken cancellationToken)
        {
            _context.Document.Update(document);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteByDocumentId(int documentId, 
            CancellationToken cancellationToken)
        {
            await AsQueryable()
                .Where(d => d.DocumentId == documentId)
                .ExecuteDeleteAsync();

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        #endregion
    }
}
