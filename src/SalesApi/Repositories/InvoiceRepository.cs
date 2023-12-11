using Microsoft.EntityFrameworkCore;
using SalesApi.Models;

namespace SalesApi.Repositories;


public class InvoiceRepository : IInvoiceRepository
{
    private readonly SalesDb _db;
    public InvoiceRepository(SalesDb db) => _db = db;
    async Task<List<Invoice>> IInvoiceRepository.FindInvoicesByPerson(Guid personId)
    {
        var person = await _db.Persons.FirstOrDefaultAsync(p => p.Id == personId);
        if (person != null)
            return await _db.Invoices.Where(inv => inv.PersonId == personId).ToListAsync();
        throw new KeyNotFoundException($"Person with id : {personId} was not founded");
    }

    async Task<Invoice> IInvoiceRepository.StoreInvoice(Invoice invoice)
    {
        var person = await _db.Persons.FirstOrDefaultAsync(p => p.Id == invoice.PersonId);
        if (person != null)
        {
            await _db.Invoices.AddAsync(invoice);
            await _db.SaveChangesAsync();
        return invoice;
        }
        throw new KeyNotFoundException($"Not found person for id : {invoice.PersonId}");

    }
}