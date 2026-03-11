using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("Email", Name = "Email", IsUnique = true)]
[Index("LicenseNumber", Name = "LicenseNumber", IsUnique = true)]
[Index("ClinicId", Name = "fk_Doctor_Clinic")]
[Index("DepartmentId", Name = "fk_Doctor_Department")]
[Index("UserId", Name = "fk_Doctor_User")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class doctors
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? UserId { get; set; }

    [Column(TypeName = "int(11)")]
    public int ClinicId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? DepartmentId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string LicenseNumber { get; set; } = null!;

    [StringLength(150)]
    public string? Specialization { get; set; }

    [Required]
    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("doctors")]
    public virtual clinics Clinic { get; set; } = null!;

    [ForeignKey("DepartmentId")]
    [InverseProperty("doctors")]
    public virtual departments? Department { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("doctors")]
    public virtual users? User { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<appointments> appointments { get; set; } = new List<appointments>();

    [InverseProperty("Doctor")]
    public virtual ICollection<labresults> labresults { get; set; } = new List<labresults>();

    [InverseProperty("Doctor")]
    public virtual ICollection<medicalrecords> medicalrecords { get; set; } = new List<medicalrecords>();

    [InverseProperty("Doctor")]
    public virtual ICollection<prescriptions> prescriptions { get; set; } = new List<prescriptions>();

    [InverseProperty("Doctor")]
    public virtual ICollection<schedules> schedules { get; set; } = new List<schedules>();
}
