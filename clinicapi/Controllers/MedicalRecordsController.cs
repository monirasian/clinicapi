using clinicapi.Data;
using clinicapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicapi.Controllers;

[Route("api/medical-records")]
public sealed class MedicalRecordsController : CrudController<medicalrecords>
{
    public MedicalRecordsController(ClinicDbContext db) : base(db) { }
}
