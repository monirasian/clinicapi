using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinicapi.Models;

[Index("InvoiceNumber", Name = "InvoiceNumber", IsUnique = true)]
[Index("AppointmentId", Name = "fk_Invoice_Appointment")]
[Index("ClinicId", Name = "fk_Invoice_Clinic")]
[Index("PatientId", Name = "fk_Invoice_Patient")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class invoices
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int Id { get; set; }

    [Column(TypeName = "int(11)")]
    public int ClinicId { get; set; }

    [Column(TypeName = "int(11)")]
    public int PatientId { get; set; }

    [Column(TypeName = "int(11)")]
    public int? AppointmentId { get; set; }

    [StringLength(50)]
    public string InvoiceNumber { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime InvoiceDate { get; set; }

    [Precision(10, 2)]
    public decimal SubtotalAmount { get; set; }

    [Precision(10, 2)]
    public decimal TaxAmount { get; set; }

    [Precision(10, 2)]
    public decimal DiscountAmount { get; set; }

    [Precision(10, 2)]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "enum('Draft','Issued','Paid','Cancelled')")]
    public string Status { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("AppointmentId")]
    [InverseProperty("invoices")]
    public virtual appointments? Appointment { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("invoices")]
    public virtual clinics Clinic { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("invoices")]
    public virtual patients Patient { get; set; } = null!;

    [InverseProperty("Invoice")]
    public virtual ICollection<invoiceitems> invoiceitems { get; set; } = new List<invoiceitems>();

    [InverseProperty("Invoice")]
    public virtual ICollection<payments> payments { get; set; } = new List<payments>();
}
