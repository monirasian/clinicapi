using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("ClinicId", Name = "fk_Patient_Clinic")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class patients
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int ClinicId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    [Column(TypeName = "enum('Male','Female','Other')")]
    public string Gender { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(150)]
    public string? EmergencyContactName { get; set; }

    [StringLength(20)]
    public string? EmergencyContactPhone { get; set; }

    [StringLength(200)]
    public string? AddressLine1 { get; set; }

    [StringLength(200)]
    public string? AddressLine2 { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? State { get; set; }

    [StringLength(20)]
    public string? PostalCode { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    [StringLength(10)]
    public string? BloodGroup { get; set; }

    [Column(TypeName = "text")]
    public string? Allergies { get; set; }

    [Required]
    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("patients")]
    public virtual clinics Clinic { get; set; } = null!;

    [InverseProperty("Patient")]
    public virtual ICollection<appointments> appointments { get; set; } = new List<appointments>();

    [InverseProperty("Patient")]
    public virtual ICollection<invoices> invoices { get; set; } = new List<invoices>();

    [InverseProperty("Patient")]
    public virtual ICollection<labresults> labresults { get; set; } = new List<labresults>();

    [InverseProperty("Patient")]
    public virtual ICollection<medicalrecords> medicalrecords { get; set; } = new List<medicalrecords>();

    [InverseProperty("Patient")]
    public virtual ICollection<prescriptions> prescriptions { get; set; } = new List<prescriptions>();
}
