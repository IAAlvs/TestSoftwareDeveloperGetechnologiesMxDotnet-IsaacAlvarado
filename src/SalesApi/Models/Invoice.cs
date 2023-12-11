using SalesApi.Services;

namespace SalesApi.Models;


public class Invoice
{

    public Guid Id { get; init; }
    // Propiedad de navegación
    public Person? Person { get; set; }
    public Guid PersonId { get; init; }

    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public Invoice(Guid id, Guid personId, DateTime date, decimal amount)
    {
        Id = id;
        PersonId = personId;
        Date = date;
        Amount = amount;
    }
    public static Invoice FromDto(AddInvoiceDto dto) => new(Guid.NewGuid(), dto.PersonId, dto.Date, dto.Amount);
    public GetInvoiceResponseDto ToGetInvoiceDto() => new(Id, Date, Amount);

}