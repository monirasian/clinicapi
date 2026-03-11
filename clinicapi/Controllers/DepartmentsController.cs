using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/departments")]
public sealed class DepartmentsController : CrudController<departments>
{
    public DepartmentsController(ClinicDbContext db) : base(db) { }

    [AllowAnonymous]
    [HttpGet]
    public override Task<ActionResult<IEnumerable<departments>>> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
        => base.GetAll(skip, take);

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public override Task<ActionResult<departments>> GetById(int id)
        => base.GetById(id);
}
