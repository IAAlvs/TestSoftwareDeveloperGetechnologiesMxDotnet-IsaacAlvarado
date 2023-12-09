
using SalesApi.Models;
using SalesApi.Repositories;

namespace SalesApi.Services;

class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    public InvoiceService(IInvoiceRepository invoiceRespository)
    {
        _invoiceRepository = invoiceRespository;
    }
    public async Task<GetInvoiceResponseDto> AddInvoice(AddInvoiceDto invoiceDto)
    {
        var invoice = Invoice.FromDto(invoiceDto);
        var addedInvoice = await _invoiceRepository.StoreInvoice(invoice);
        return addedInvoice.ToGetInvoiceDto();
    }

    public async Task<List<GetInvoiceResponseDto>> GetInvoicesByPersonId(Guid personId)
    {
        var invoices = await _invoiceRepository.FindInvoicesByPerson(personId);
        return invoices.Select(i => i.ToGetInvoiceDto()).ToList();
    }
}
