using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("Email", Name = "Email", IsUnique = true)]
[Index("UserName", Name = "UserName", IsUnique = true)]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class users
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<doctors> doctors { get; set; } = new List<doctors>();

    [InverseProperty("User")]
    public virtual ICollection<staff> staff { get; set; } = new List<staff>();

    [ForeignKey("UserId")]
    [InverseProperty("User")]
    public virtual ICollection<roles> Role { get; set; } = new List<roles>();
}
