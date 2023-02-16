using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyectoprogra5.DataAccess.Models;

public partial class ProyectoProgra5Context : DbContext
{
    public ProyectoProgra5Context()
    {
    }

    public ProyectoProgra5Context(DbContextOptions<ProyectoProgra5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Componente> Componentes { get; set; }

    public virtual DbSet<DashboardServicio> DashboardServicios { get; set; }

    public virtual DbSet<DashboardServidore> DashboardServidores { get; set; }

    public virtual DbSet<EncargadoServicio> EncargadoServicios { get; set; }

    public virtual DbSet<EncargadoServidore> EncargadoServidores { get; set; }

    public virtual DbSet<Parametro> Parametros { get; set; }

    public virtual DbSet<ParametrosSensibilidad> ParametrosSensibilidads { get; set; }

    public virtual DbSet<ParametrosServicio> ParametrosServicios { get; set; }

    public virtual DbSet<ParametrosServidore> ParametrosServidores { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Servidor> Servidors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Proyecto");

        modelBuilder.Entity<Componente>(entity =>
        {
            entity.HasKey(e => e.CodigoComponente);

            entity.ToTable("Componentes", "dbo");

            entity.Property(e => e.CodigoComponente).ValueGeneratedNever();
            entity.Property(e => e.CuerpoCorreo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DashboardServicio>(entity =>
        {
            entity.HasKey(e => e.IdDashboard);

            entity.ToTable("DashboardServicios", "dbo");

            entity.Property(e => e.IdDashboard)
                .ValueGeneratedNever()
                .HasColumnName("idDashboard");
            entity.Property(e => e.Disponibilidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaMonitoreo).HasColumnType("date");
            entity.Property(e => e.InformacionParametros)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Timeout).HasColumnName("timeout");

            entity.HasOne(d => d.CodigoServicioNavigation).WithMany(p => p.DashboardServicios)
                .HasForeignKey(d => d.CodigoServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DashboardServicios_Servicios");
        });

        modelBuilder.Entity<DashboardServidore>(entity =>
        {
            entity.HasKey(e => e.Iddashboard);

            entity.ToTable("DashboardServidores", "dbo");

            entity.Property(e => e.Iddashboard)
                .ValueGeneratedNever()
                .HasColumnName("IDdashboard");
            entity.Property(e => e.FechaUltimomonitoreo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsoCpu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsoCPU");
            entity.Property(e => e.UsoDisco)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsoMemoria)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CodigoServidorNavigation).WithMany(p => p.DashboardServidores)
                .HasForeignKey(d => d.CodigoServidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DashboardServidores_Servidor");
        });

        modelBuilder.Entity<EncargadoServicio>(entity =>
        {
            entity.HasKey(e => new { e.Usuario, e.CodigoServicio }).HasName("PK__Encargad__EAAFC2C1F8B36376");

            entity.ToTable("EncargadoServicios", "dbo");

            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CodigoServicioNavigation).WithMany(p => p.EncargadoServicios)
                .HasForeignKey(d => d.CodigoServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EncargadoServicios_Servicios");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.EncargadoServicios)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EncargadoServicios_Usuarios");
        });

        modelBuilder.Entity<EncargadoServidore>(entity =>
        {
            entity.HasKey(e => new { e.Usuario, e.CodigoServidor });

            entity.ToTable("EncargadoServidores", "dbo");

            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CodigoServidorNavigation).WithMany(p => p.EncargadoServidores)
                .HasForeignKey(d => d.CodigoServidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EncargadoServidores_Servidor");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.EncargadoServidores)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EncargadoServidores_Usuarios");
        });

        modelBuilder.Entity<Parametro>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Parametr__06370DADAD40573A");

            entity.ToTable("Parametros", "dbo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ParametrosSensibilidad>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_ParametrosSensibilidad_1");

            entity.ToTable("ParametrosSensibilidad", "dbo");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ParametrosServicio>(entity =>
        {
            entity.HasKey(e => new { e.IdParametro, e.IdServicios });

            entity.ToTable("ParametrosServicios", "dbo");

            entity.HasOne(d => d.IdParametroNavigation).WithMany(p => p.ParametrosServicios)
                .HasForeignKey(d => d.IdParametro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosServicios_Parametros");

            entity.HasOne(d => d.IdServiciosNavigation).WithMany(p => p.ParametrosServicios)
                .HasForeignKey(d => d.IdServicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosServicios_Servicios");
        });

        modelBuilder.Entity<ParametrosServidore>(entity =>
        {
            entity.HasKey(e => new { e.ParametroSensibilidad, e.Componente, e.IdServidor }).HasName("PK_ParametrosSensibilidad");

            entity.ToTable("ParametrosServidores", "dbo");

            entity.Property(e => e.Porcentaje)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.ComponenteNavigation).WithMany(p => p.ParametrosServidores)
                .HasForeignKey(d => d.Componente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosServidores_Componentes");

            entity.HasOne(d => d.IdServidorNavigation).WithMany(p => p.ParametrosServidores)
                .HasForeignKey(d => d.IdServidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosServidores_Servidor");

            entity.HasOne(d => d.ParametroSensibilidadNavigation).WithMany(p => p.ParametrosServidores)
                .HasForeignKey(d => d.ParametroSensibilidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosServidores_ParametrosSensibilidad");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Servicios", "dbo");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servidor>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Servidor", "dbo");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Administrador)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuario1);

            entity.ToTable("Usuarios", "dbo");

            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
