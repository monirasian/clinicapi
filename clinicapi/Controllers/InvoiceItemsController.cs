using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/invoice-items")]
public sealed class InvoiceItemsController : CrudController<invoiceitems>
{
    public InvoiceItemsController(ClinicDbContext db) : base(db) { }
}
