using OneCore.Entities;

namespace OneCore.Repositories
{
    public class BillRepository
    {
        protected readonly Entities.Configuration.EFCoreContext _context;

        public BillRepository(Entities.Configuration.EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<Bill> AsQueryable()
        {
            return _context.Bill.AsQueryable();
        }
        public async Task<Bill?> GetById(int billId, CancellationToken cancellationToken)
        {
            return await _context.Bill.FindAsync(new object[] { billId }, cancellationToken: cancellationToken);
        }
        public async Task<bool> Add(Bill bill, CancellationToken cancellationToken)
        {
            await _context.Bill.AddAsync(bill, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> Update(Bill bill, CancellationToken cancellationToken)
        {
            _context.Bill.Update(bill);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> Remove(Bill bill, CancellationToken cancellationToken)
        {
            _context.Bill.Remove(bill);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
