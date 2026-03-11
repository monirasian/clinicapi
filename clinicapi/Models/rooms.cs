using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("ClinicId", "Name", Name = "uq_Room_Clinic_Name", IsUnique = true)]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class rooms
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int ClinicId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "enum('Consultation','Surgery','Ward','Lab','Other')")]
    public string RoomType { get; set; } = null!;

    [StringLength(20)]
    public string? Floor { get; set; }

    [Required]
    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("rooms")]
    public virtual clinics Clinic { get; set; } = null!;

    [InverseProperty("Room")]
    public virtual ICollection<appointments> appointments { get; set; } = new List<appointments>();
}
