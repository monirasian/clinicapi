using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using clinicapi.Models;

namespace clinicapi.Data;

public partial class ClinicDbContext : DbContext
{
    public ClinicDbContext()
    {
    }

    public ClinicDbContext(DbContextOptions<ClinicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<appointments> appointments { get; set; }

    public virtual DbSet<clinics> clinics { get; set; }

    public virtual DbSet<departments> departments { get; set; }

    public virtual DbSet<doctors> doctors { get; set; }

    public virtual DbSet<invoiceitems> invoiceitems { get; set; }

    public virtual DbSet<invoices> invoices { get; set; }

    public virtual DbSet<labresults> labresults { get; set; }

    public virtual DbSet<medicalrecords> medicalrecords { get; set; }

    public virtual DbSet<patients> patients { get; set; }

    public virtual DbSet<paymentitems> paymentitems { get; set; }

    public virtual DbSet<payments> payments { get; set; }

    public virtual DbSet<prescriptionitems> prescriptionitems { get; set; }

    public virtual DbSet<prescriptions> prescriptions { get; set; }

    public virtual DbSet<roles> roles { get; set; }

    public virtual DbSet<rooms> rooms { get; set; }

    public virtual DbSet<schedules> schedules { get; set; }

    public virtual DbSet<staff> staff { get; set; }

    public virtual DbSet<users> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Connection string is configured via DI in Program.cs.
        // This keeps the scaffolded DbContext safe to commit.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<appointments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DurationMinutes).HasDefaultValueSql("'30'");
            entity.Property(e => e.Status).HasDefaultValueSql("'Scheduled'");

            entity.HasOne(d => d.Clinic).WithMany(p => p.appointments).HasConstraintName("fk_Appointment_Clinic");

            entity.HasOne(d => d.Doctor).WithMany(p => p.appointments).HasConstraintName("fk_Appointment_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.appointments).HasConstraintName("fk_Appointment_Patient");

            entity.HasOne(d => d.Room).WithMany(p => p.appointments)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Appointment_Room");
        });

        modelBuilder.Entity<clinics>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<departments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Clinic).WithMany(p => p.departments)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Department_Clinic");
        });

        modelBuilder.Entity<doctors>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Clinic).WithMany(p => p.doctors).HasConstraintName("fk_Doctor_Clinic");

            entity.HasOne(d => d.Department).WithMany(p => p.doctors)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Doctor_Department");

            entity.HasOne(d => d.User).WithMany(p => p.doctors)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Doctor_User");
        });

        modelBuilder.Entity<invoiceitems>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Quantity).HasDefaultValueSql("'1.00'");

            entity.HasOne(d => d.Invoice).WithMany(p => p.invoiceitems).HasConstraintName("fk_Invoice_Item_Invoice");
        });

        modelBuilder.Entity<invoices>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.InvoiceDate).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.Status).HasDefaultValueSql("'Issued'");

            entity.HasOne(d => d.Appointment).WithMany(p => p.invoices)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Invoice_Appointment");

            entity.HasOne(d => d.Clinic).WithMany(p => p.invoices).HasConstraintName("fk_Invoice_Clinic");

            entity.HasOne(d => d.Patient).WithMany(p => p.invoices).HasConstraintName("fk_Invoice_Patient");
        });

        modelBuilder.Entity<labresults>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.Status).HasDefaultValueSql("'Ordered'");

            entity.HasOne(d => d.Appointment).WithMany(p => p.labresults)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Lab_Appointment");

            entity.HasOne(d => d.Doctor).WithMany(p => p.labresults)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Lab_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.labresults).HasConstraintName("fk_Lab_Patient");
        });

        modelBuilder.Entity<medicalrecords>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.VisitDate).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Appointment).WithMany(p => p.medicalrecords)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Medical_Record_Appointment");

            entity.HasOne(d => d.Doctor).WithMany(p => p.medicalrecords)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Medical_Record_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.medicalrecords).HasConstraintName("fk_Medical_Record_Patient");
        });

        modelBuilder.Entity<patients>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Clinic).WithMany(p => p.patients).HasConstraintName("fk_Patient_Clinic");
        });

        modelBuilder.Entity<paymentitems>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Payment).WithMany(p => p.paymentitems).HasConstraintName("fk_Payment_Item_Payment");
        });

        modelBuilder.Entity<payments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Invoice).WithMany(p => p.payments).HasConstraintName("fk_Payment_Invoice");
        });

        modelBuilder.Entity<prescriptionitems>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Prescription).WithMany(p => p.prescriptionitems).HasConstraintName("fk_Prescription_Item_Prescription");
        });

        modelBuilder.Entity<prescriptions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.IssuedAt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Appointment).WithMany(p => p.prescriptions)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Prescription_Appointment");

            entity.HasOne(d => d.Doctor).WithMany(p => p.prescriptions)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Prescription_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.prescriptions).HasConstraintName("fk_Prescription_Patient");
        });

        modelBuilder.Entity<roles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<rooms>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.RoomType).HasDefaultValueSql("'Consultation'");

            entity.HasOne(d => d.Clinic).WithMany(p => p.rooms).HasConstraintName("fk_Room_Clinic");
        });

        modelBuilder.Entity<schedules>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Doctor).WithMany(p => p.schedules).HasConstraintName("fk_Schedule_Doctor");
        });

        modelBuilder.Entity<staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Clinic).WithMany(p => p.staff).HasConstraintName("fk_Staff_Clinic");

            entity.HasOne(d => d.User).WithMany(p => p.staff)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_Staff_User");
        });

        modelBuilder.Entity<users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");

            entity.HasMany(d => d.Role).WithMany(p => p.User)
                .UsingEntity<Dictionary<string, object>>(
                    "userroles",
                    r => r.HasOne<roles>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_UserRoles_Role"),
                    l => l.HasOne<users>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_UserRoles_User"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .HasCharSet("utf8mb4")
                            .UseCollation("utf8mb4_unicode_ci");
                        j.HasIndex(new[] { "RoleId" }, "fk_UserRoles_Role");
                        j.IndexerProperty<int>("UserId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("RoleId").HasColumnType("int(11)");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
