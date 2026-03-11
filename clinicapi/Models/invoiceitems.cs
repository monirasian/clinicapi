using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("InvoiceId", Name = "fk_Invoice_Item_Invoice")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class invoiceitems
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int InvoiceId { get; set; }

    [StringLength(200)]
    public string Description { get; set; } = null!;

    [Precision(10, 2)]
    public decimal Quantity { get; set; }

    [Precision(10, 2)]
    public decimal UnitPrice { get; set; }

    [Precision(10, 2)]
    public decimal LineTotal { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("invoiceitems")]
    public virtual invoices Invoice { get; set; } = null!;
}
