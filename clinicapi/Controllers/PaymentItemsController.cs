using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/payment-items")]
public sealed class PaymentItemsController : CrudController<paymentitems>
{
    public PaymentItemsController(ClinicDbContext db) : base(db) { }
}
