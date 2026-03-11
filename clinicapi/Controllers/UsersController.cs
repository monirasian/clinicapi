using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/users")]
public sealed class UsersController : CrudController<users>
{
    public UsersController(ClinicDbContext db) : base(db) { }
}
