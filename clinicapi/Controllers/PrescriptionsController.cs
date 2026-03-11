using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/prescriptions")]
public sealed class PrescriptionsController : CrudController<prescriptions>
{
    public PrescriptionsController(ClinicDbContext db) : base(db) { }
}
