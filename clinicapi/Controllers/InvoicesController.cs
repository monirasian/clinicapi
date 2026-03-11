using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/invoices")]
public sealed class InvoicesController : CrudController<invoices>
{
    public InvoicesController(ClinicDbContext db) : base(db) { }
}
