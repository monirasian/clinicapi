using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/payments")]
public sealed class PaymentsController : CrudController<payments>
{
    public PaymentsController(ClinicDbContext db) : base(db) { }
}
