using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/prescription-items")]
public sealed class PrescriptionItemsController : CrudController<prescriptionitems>
{
    public PrescriptionItemsController(ClinicDbContext db) : base(db) { }
}
