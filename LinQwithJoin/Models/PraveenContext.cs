using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LinQwithJoin.Models;

public partial class PraveenContext : DbContext
{
    public PraveenContext()
    {
    }

    public PraveenContext(DbContextOptions<PraveenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<SchoolStudent> SchoolStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__person__543848DFF508A975");

            entity.ToTable("person");

            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.PersonAge).HasColumnName("person_age");
            entity.Property(e => e.PersonCity)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("person_city");
            entity.Property(e => e.PersonGender)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("person_gender");
            entity.Property(e => e.PersonName)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("person_name");
        });

        modelBuilder.Entity<SchoolStudent>(entity =>
        {
            entity.HasKey(e => e.RollNumber).HasName("PK__school_s__E9F06F1721162CC9");

            entity.ToTable("school_student");

            entity.Property(e => e.SchoolName)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("school_Name");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.StudentMark).HasColumnName("student_Mark");

            entity.HasOne(d => d.Student).WithMany(p => p.SchoolStudents)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("fk_schoolstudent");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
