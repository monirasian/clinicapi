using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("PaymentId", Name = "fk_Payment_Item_Payment")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class paymentitems
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int PaymentId { get; set; }

    [Column(TypeName = "enum('Cash','Card','BankTransfer','Insurance','Other')")]
    public string Method { get; set; } = null!;

    [Precision(10, 2)]
    public decimal Amount { get; set; }

    [StringLength(255)]
    public string? Details { get; set; }

    [ForeignKey("PaymentId")]
    [InverseProperty("paymentitems")]
    public virtual payments Payment { get; set; } = null!;
}
