using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Candidate_Management_System.Entities;

public partial class CandidateDBContext : DbContext
{
    public CandidateDBContext()
    {
    }

    public CandidateDBContext(DbContextOptions<CandidateDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CandidateInformation> CandidateInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FH2KN1N\\SQLEXPRESS;Database=janak_DB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CandidateInformation>(entity =>
        {
            entity.ToTable("Candidate_Information");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CandidateName)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Candidate_Name");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Dob)
                .HasMaxLength(100)
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.Resume).HasMaxLength(50);
            entity.Property(e => e.Technology).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
