using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/lab-results")]
public sealed class LabResultsController : CrudController<labresults>
{
    public LabResultsController(ClinicDbContext db) : base(db) { }
}
