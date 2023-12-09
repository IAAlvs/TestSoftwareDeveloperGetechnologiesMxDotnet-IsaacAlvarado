namespace SalesApi.Services;
public record GetInvoiceResponseDto(Guid Id, DateTime Date, decimal Amount);
public record AddInvoiceDto(Guid PersonId, DateTime Date, decimal Amount);