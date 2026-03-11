using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/staff")]
public sealed class StaffController : CrudController<staff>
{
    public StaffController(ClinicDbContext db) : base(db) { }

    [AllowAnonymous]
    [HttpGet]
    public override Task<ActionResult<IEnumerable<staff>>> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
        => base.GetAll(skip, take);

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public override Task<ActionResult<staff>> GetById(int id)
        => base.GetById(id);
}
