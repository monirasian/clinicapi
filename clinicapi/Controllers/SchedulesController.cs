using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/schedules")]
public sealed class SchedulesController : CrudController<schedules>
{
    public SchedulesController(ClinicDbContext db) : base(db) { }

    [AllowAnonymous]
    [HttpGet]
    public override Task<ActionResult<IEnumerable<schedules>>> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
        => base.GetAll(skip, take);

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public override Task<ActionResult<schedules>> GetById(int id)
        => base.GetById(id);
}
