using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("InvoiceId", Name = "fk_Payment_Invoice")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class payments
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int InvoiceId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PaymentDate { get; set; }

    [Precision(10, 2)]
    public decimal Amount { get; set; }

    [Column(TypeName = "enum('Cash','Card','BankTransfer','Insurance','Other')")]
    public string Method { get; set; } = null!;

    [StringLength(100)]
    public string? ReferenceNumber { get; set; }

    [Column(TypeName = "text")]
    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("payments")]
    public virtual invoices Invoice { get; set; } = null!;

    [InverseProperty("Payment")]
    public virtual ICollection<paymentitems> paymentitems { get; set; } = new List<paymentitems>();
}
