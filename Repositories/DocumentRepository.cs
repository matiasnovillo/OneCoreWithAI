using OneCore.Entities;

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
        public async Task<Document?> GetById(int documentId, CancellationToken cancellationToken)
        {
            return await _context.Document.FindAsync(new object[] { documentId }, cancellationToken: cancellationToken);
        }
        public async Task<bool> Add(Document document, CancellationToken cancellationToken)
        {
            await _context.Document.AddAsync(document, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> Update(Document document, CancellationToken cancellationToken)
        {
            _context.Document.Update(document);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> Remove(Document document, CancellationToken cancellationToken)
        {
            _context.Document.Remove(document);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
