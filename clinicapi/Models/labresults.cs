using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("AppointmentId", Name = "fk_Lab_Appointment")]
[Index("DoctorId", Name = "fk_Lab_Doctor")]
[Index("PatientId", Name = "fk_Lab_Patient")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class labresults
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

    [StringLength(150)]
    public string TestName { get; set; } = null!;

    [StringLength(150)]
    public string? ResultValue { get; set; }

    [StringLength(50)]
    public string? Unit { get; set; }

    [StringLength(100)]
    public string? ReferenceRange { get; set; }

    [Column(TypeName = "enum('Ordered','InProgress','Completed','Cancelled')")]
    public string Status { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TakenAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReportedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("labresults")]
    public virtual appointments? Appointment { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("labresults")]
    public virtual doctors? Doctor { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("labresults")]
    public virtual patients Patient { get; set; } = null!;
}
