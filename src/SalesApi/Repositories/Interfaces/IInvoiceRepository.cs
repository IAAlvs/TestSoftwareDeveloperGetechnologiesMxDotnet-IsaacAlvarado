using SalesApi.Models;
namespace SalesApi.Repositories;

interface IInvoiceRepository{
    Task<List<Invoice>> FindInvoicesByPerson(Guid personId);
    Task<Invoice> StoreInvoice(Invoice invoice);
}