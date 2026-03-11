using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("AppointmentId", Name = "fk_Medical_Record_Appointment")]
[Index("DoctorId", Name = "fk_Medical_Record_Doctor")]
[Index("PatientId", Name = "fk_Medical_Record_Patient")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class medicalrecords
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int PatientId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? DoctorId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? AppointmentId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime VisitDate { get; set; }

    [StringLength(255)]
    public string? ChiefComplaint { get; set; }

    [Column(TypeName = "text")]
    public string? Diagnosis { get; set; }

    [Column(TypeName = "text")]
    public string? TreatmentPlan { get; set; }

    [Column(TypeName = "text")]
    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("medicalrecords")]
    public virtual appointments? Appointment { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("medicalrecords")]
    public virtual doctors? Doctor { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("medicalrecords")]
    public virtual patients Patient { get; set; } = null!;
}
