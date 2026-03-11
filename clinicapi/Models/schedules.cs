using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("DoctorId", Name = "fk_Schedule_Doctor")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class schedules
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int DoctorId { get; set; }

    [Column(TypeName = "tinyint(4)")]
    public sbyte DayOfWeek { get; set; }

    [Column(TypeName = "time")]
    public TimeOnly StartTime { get; set; }

    [Column(TypeName = "time")]
    public TimeOnly EndTime { get; set; }

    [Required]
    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("schedules")]
    public virtual doctors Doctor { get; set; } = null!;
}
