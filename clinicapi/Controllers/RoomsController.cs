using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/rooms")]
public sealed class RoomsController : CrudController<rooms>
{
    public RoomsController(ClinicDbContext db) : base(db) { }
}
