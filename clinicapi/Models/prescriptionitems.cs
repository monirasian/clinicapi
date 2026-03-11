using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("PrescriptionId", Name = "fk_Prescription_Item_Prescription")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class prescriptionitems
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int PrescriptionId { get; set; }

    [StringLength(200)]
    public string MedicationName { get; set; } = null!;

    [StringLength(100)]
    public string Dosage { get; set; } = null!;

    [StringLength(100)]
    public string Frequency { get; set; } = null!;

    [StringLength(50)]
    public string? Route { get; set; }

    [Column(TypeName = "int(11)")]
    public int? DurationDays { get; set; }

    [Column(TypeName = "text")]
    public string? Instructions { get; set; }

    [ForeignKey("PrescriptionId")]
    [InverseProperty("prescriptionitems")]
    public virtual prescriptions Prescription { get; set; } = null!;
}
