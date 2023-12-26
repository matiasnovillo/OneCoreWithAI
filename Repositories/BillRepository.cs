using Microsoft.EntityFrameworkCore;
using OneCore.Entities;
using System.Text.RegularExpressions;

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

        #region Queries
        public Bill? GetById(int billId, 
            CancellationToken cancellationToken)
        {
            return _context.Bill
                .FirstOrDefault(b => b.BillId == billId);
        }

        public List<Bill?> GetAll(CancellationToken cancellationToken)
        {
            List<Bill?> lstBill = [];

            var GetAllQuery =
                    from x in _context.Bill
                    select new
                    {
                        x.BillId,
                        x.FileName,
                        x.ClientName,
                        x.ProviderName,
                    };

            foreach (var x in GetAllQuery)
            {
                Bill document = new()
                {
                    BillId = x.BillId,
                    FileName = x.FileName,
                    ClientName = x.ClientName,
                    ProviderName = x.ProviderName,
                };
                lstBill.Add(document);
            }

            return lstBill;
        }

        public List<Bill?> GetAllByFileName(string textToSearch,
            bool strictSearch,
            CancellationToken cancellationToken)
        {
            //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
            string[] words = Regex
                .Replace(textToSearch
                .Trim(), @"\s+", " ")
                .Split(" ");

            List<Bill?> lstBill = [];

            var GetAllQuery = AsQueryable()
                .Where(x => strictSearch ?
                    words.All(word => x.FileName.Contains(word)) :
                    words.Any(word => x.FileName.Contains(word)))
                .ToList();

            foreach (var x in GetAllQuery)
            {
                Bill bill = new()
                {
                    BillId = x.BillId,
                    FileName = x.FileName,
                    ClientName = x.ClientName,
                    ProviderName = x.ProviderName
                };
                lstBill.Add(bill);
            }

            return lstBill;
        }
        #endregion

        #region Non-Queries
        public async Task<bool> Add(Bill bill, 
            CancellationToken cancellationToken)
        {
            await _context.Bill.AddAsync(bill, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> Update(Bill bill, 
            CancellationToken cancellationToken)
        {
            _context.Bill.Update(bill);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteByBillId(int billId,
            CancellationToken cancellationToken)
        {
            AsQueryable()
                .Where(d => d.BillId == billId)
                .ExecuteDelete();

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        #endregion
    }
}
