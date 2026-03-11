using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/clinics")]
public sealed class ClinicsController : CrudController<clinics>
{
    public ClinicsController(ClinicDbContext db) : base(db) { }

    [AllowAnonymous]
    [HttpGet]
    public override Task<ActionResult<IEnumerable<clinics>>> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
        => base.GetAll(skip, take);

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public override Task<ActionResult<clinics>> GetById(int id)
        => base.GetById(id);
}
