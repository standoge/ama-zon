using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ama_zon;

public partial class AmaZonContext : DbContext
{
    public AmaZonContext(DbContextOptions<AmaZonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acuerdo> Acuerdos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<Plaza> Plazas { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acuerdo>(entity =>
        {
            entity.ToTable("ACUERDO", "rrhh");

            entity.Property(e => e.AcuerdoId).HasColumnName("acuerdo_id");
            entity.Property(e => e.CantidadSancion)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cantidad_sancion");
            entity.Property(e => e.DepartamentoId).HasColumnName("departamento_id");
            entity.Property(e => e.FechaEmision)
                .HasColumnType("date")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.FechaRenuncia)
                .HasColumnType("date")
                .HasColumnName("fecha_renuncia");
            entity.Property(e => e.PlazaId).HasColumnName("plaza_id");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Acuerdos)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACUERDO_DEPARTAMENTO");

            entity.HasOne(d => d.Plaza).WithMany(p => p.Acuerdos)
                .HasForeignKey(d => d.PlazaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACUERDO_PLAZA");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Acuerdos)
                .HasForeignKey(d => d.ProyectoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACUERDO_PROYECTO");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.ToTable("DEPARTAMENTO", "rrhh");

            entity.Property(e => e.DepartamentoId).HasColumnName("departamento_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("EMPLEADO", "rrhh");

            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.AcuerdoId).HasColumnName("acuerdo_id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PaisId).HasColumnName("pais_id");

            entity.HasOne(d => d.Acuerdo).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.AcuerdoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_ACUERDO");

            entity.HasOne(d => d.Pais).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_PAIS");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.PaisId);

            entity.ToTable("PAIS", "rrhh");

            entity.Property(e => e.PaisId).HasColumnName("pais_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Plaza>(entity =>
        {
            entity.ToTable("PLAZA", "rrhh");

            entity.Property(e => e.PlazaId).HasColumnName("plaza_id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.ToTable("PROYECTO", "rrhh");

            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}