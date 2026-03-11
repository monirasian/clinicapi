using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/doctors")]
public sealed class DoctorsController : CrudController<doctors>
{
    public DoctorsController(ClinicDbContext db) : base(db) { }

    [AllowAnonymous]
    [HttpGet]
    public override Task<ActionResult<IEnumerable<doctors>>> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
        => base.GetAll(skip, take);

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public override Task<ActionResult<doctors>> GetById(int id)
        => base.GetById(id);
}
