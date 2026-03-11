using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/appointments")]
public sealed class AppointmentsController : CrudController<appointments>
{
    public AppointmentsController(ClinicDbContext db) : base(db) { }
}
