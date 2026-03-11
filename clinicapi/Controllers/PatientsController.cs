using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/patients")]
public sealed class PatientsController : CrudController<patients>
{
    public PatientsController(ClinicDbContext db) : base(db) { }
}
