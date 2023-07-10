using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain.Models;

public partial class ProjectPrn221Context : DbContext
{
    public ProjectPrn221Context()
    {
    }

    public ProjectPrn221Context(DbContextOptions<ProjectPrn221Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<MarkReport> MarkReports { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<StudentDetail> StudentDetails { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectMark> SubjectMarks { get; set; }

    public virtual DbSet<TeacherDetail> TeacherDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project_PRN"));

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassId).ValueGeneratedNever();
            entity.Property(e => e.ClassName).HasMaxLength(50);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("Enrollment");

            entity.Property(e => e.EnrollmentId).ValueGeneratedNever();

            entity.HasOne(d => d.Class).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollment_Class");

            entity.HasOne(d => d.Semester).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollment_Semester");

            entity.HasOne(d => d.Student).WithMany(p => p.EnrollmentStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollment_Account1");

            entity.HasOne(d => d.Subject).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollment_Subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.EnrollmentTeachers)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollment_Account");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.ToTable("Major");

            entity.Property(e => e.MajorId).ValueGeneratedNever();
            entity.Property(e => e.MajorName).HasMaxLength(50);
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.ToTable("Mark");

            entity.Property(e => e.MarkId).ValueGeneratedNever();
            entity.Property(e => e.MarkName).HasMaxLength(50);
        });

        modelBuilder.Entity<MarkReport>(entity =>
        {
            entity.ToTable("MarkReport").HasKey(cc => new { cc.EnrollmentId, cc.MarkId });

            entity.HasOne(d => d.Enrollment).WithMany()
                .HasForeignKey(d => d.EnrollmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MarkReport_Enrollment");

            entity.HasOne(d => d.Mark).WithMany()
                .HasForeignKey(d => d.MarkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MarkReport_Mark");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.ToTable("Semester");

            entity.Property(e => e.SemesterId).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.SemesterName).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<StudentDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("StudentDetail");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.HasOne(d => d.Account).WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentDetail_Account");

            entity.HasOne(d => d.Major).WithMany()
                .HasForeignKey(d => d.MajorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentDetail_Major");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId).ValueGeneratedNever();
            entity.Property(e => e.SubjectCode).HasMaxLength(50);
            entity.Property(e => e.SubjectName).HasMaxLength(50);
        });

        modelBuilder.Entity<SubjectMark>(entity =>
        {
            /*entity
                .HasNoKey()
                .ToTable("SubjectMark");*/
            entity.ToTable("SubjectMark").HasKey(cc => new { cc.SubjectId, cc.MarkId });

            entity.HasOne(d => d.Mark).WithMany(m => m.SubjectMarks)
                .HasForeignKey(d => d.MarkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubjectMark_Mark");

            entity.HasOne(d => d.Subject).WithMany(s => s.SubjectMarks)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubjectMark_Subject");
        });

        modelBuilder.Entity<TeacherDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TeacherDetail");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherDetail_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
