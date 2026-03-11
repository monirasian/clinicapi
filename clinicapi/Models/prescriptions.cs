using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("AppointmentId", Name = "fk_Prescription_Appointment")]
[Index("DoctorId", Name = "fk_Prescription_Doctor")]
[Index("PatientId", Name = "fk_Prescription_Patient")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class prescriptions
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
    public DateTime IssuedAt { get; set; }

    [Column(TypeName = "text")]
    public string? Notes { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("prescriptions")]
    public virtual appointments? Appointment { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("prescriptions")]
    public virtual doctors? Doctor { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("prescriptions")]
    public virtual patients Patient { get; set; } = null!;

    [InverseProperty("Prescription")]
    public virtual ICollection<prescriptionitems> prescriptionitems { get; set; } = new List<prescriptionitems>();
}
