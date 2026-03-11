using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class clinics
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [StringLength(150)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

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

    [Required]
    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("Clinic")]
    public virtual ICollection<appointments> appointments { get; set; } = new List<appointments>();

    [InverseProperty("Clinic")]
    public virtual ICollection<departments> departments { get; set; } = new List<departments>();

    [InverseProperty("Clinic")]
    public virtual ICollection<doctors> doctors { get; set; } = new List<doctors>();

    [InverseProperty("Clinic")]
    public virtual ICollection<invoices> invoices { get; set; } = new List<invoices>();

    [InverseProperty("Clinic")]
    public virtual ICollection<patients> patients { get; set; } = new List<patients>();

    [InverseProperty("Clinic")]
    public virtual ICollection<rooms> rooms { get; set; } = new List<rooms>();

    [InverseProperty("Clinic")]
    public virtual ICollection<staff> staff { get; set; } = new List<staff>();
}
