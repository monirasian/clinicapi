using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("ClinicId", Name = "fk_Staff_Clinic")]
[Index("UserId", Name = "fk_Staff_User")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class staff
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int? UserId { get; set; }

    [Column(TypeName = "int(11)")]
    public int ClinicId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(100)]
    public string? Position { get; set; }

    [Required]
    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("staff")]
    public virtual clinics Clinic { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("staff")]
    public virtual users? User { get; set; }
}
