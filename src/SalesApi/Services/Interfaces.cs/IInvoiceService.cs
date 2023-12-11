namespace SalesApi.Services;
public interface IInvoiceService{
    Task<List<GetInvoiceResponseDto>> GetInvoicesByPersonId(Guid personId);
    Task<GetInvoiceResponseDto> AddInvoice(AddInvoiceDto invoice);
}