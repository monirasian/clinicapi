using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("ClinicId", Name = "fk_Appointment_Clinic")]
[Index("DoctorId", Name = "fk_Appointment_Doctor")]
[Index("PatientId", Name = "fk_Appointment_Patient")]
[Index("RoomId", Name = "fk_Appointment_Room")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class appointments
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int ClinicId { get; set; }

    [Column(TypeName = "int(11)")]
    public int PatientId { get; set; }

    [Column(TypeName = "int(11)")]
    public int DoctorId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? RoomId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ScheduledAt { get; set; }

    [Column(TypeName = "int(11)")]
    public int DurationMinutes { get; set; }

    [Column(TypeName = "enum('Scheduled','Completed','Cancelled','NoShow')")]
    public string Status { get; set; } = null!;

    [StringLength(255)]
    public string? Reason { get; set; }

    [Column(TypeName = "text")]
    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("appointments")]
    public virtual clinics Clinic { get; set; } = null!;

    [ForeignKey("DoctorId")]
    [InverseProperty("appointments")]
    public virtual doctors Doctor { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("appointments")]
    public virtual patients Patient { get; set; } = null!;

    [ForeignKey("RoomId")]
    [InverseProperty("appointments")]
    public virtual rooms? Room { get; set; }

    [InverseProperty("Appointment")]
    public virtual ICollection<invoices> invoices { get; set; } = new List<invoices>();

    [InverseProperty("Appointment")]
    public virtual ICollection<labresults> labresults { get; set; } = new List<labresults>();

    [InverseProperty("Appointment")]
    public virtual ICollection<medicalrecords> medicalrecords { get; set; } = new List<medicalrecords>();

    [InverseProperty("Appointment")]
    public virtual ICollection<prescriptions> prescriptions { get; set; } = new List<prescriptions>();
}
