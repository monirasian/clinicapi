using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/roles")]
public sealed class RolesController : CrudController<roles>
{
    public RolesController(ClinicDbContext db) : base(db) { }
}
