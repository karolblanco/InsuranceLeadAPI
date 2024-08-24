using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace insuranceLeadApi.Models;

public partial class InsuranceDBContext : DbContext
{
    public InsuranceDBContext()
    {
    }

    public InsuranceDBContext(DbContextOptions<InsuranceDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PolicyHolder> PolicyHolders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
// => optionsBuilder.UseNpgsql("Host=localhost;Database=AtlanticTest;Username=top;Password=top;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PolicyHolder>(entity =>
        {
            entity.HasKey(e => e.IdentificationNumber).HasName("policy_holder_pkey");

            entity.ToTable("policy_holder");

            entity.Property(e => e.IdentificationNumber).HasColumnName("identification_number");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EstimatedInsuranceValue)
                .HasColumnType("money")
                .HasColumnName("estimated_insurance_value");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(255)
                .HasColumnName("middle_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Remark).HasColumnName("remark");
            entity.Property(e => e.SecondLastName)
                .HasMaxLength(255)
                .HasColumnName("second_last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
