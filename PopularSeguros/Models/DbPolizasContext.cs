using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PopularSeguros.Models;

public partial class DbPolizasContext : DbContext
{
    public DbPolizasContext()
    {
    }

    public DbPolizasContext(DbContextOptions<DbPolizasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aseguradora> Aseguradoras { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cobertura> Coberturas { get; set; }

    public virtual DbSet<EstadoPoliza> EstadoPolizas { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<TipoPoliza> TipoPolizas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aseguradora>(entity =>
        {
            entity.HasKey(e => e.IdAseguradora).HasName("PK__asegurad__3D8FCDE4B77FE58F");

            entity.ToTable("aseguradoras");

            entity.HasIndex(e => e.Aseguradora1, "UQ__asegurad__4BC464587379F095").IsUnique();

            entity.Property(e => e.IdAseguradora).HasColumnName("idAseguradora");
            entity.Property(e => e.Aseguradora1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("aseguradora");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CedulaAsegurado).HasName("PK__cliente__825232B03B831A2C");

            entity.ToTable("cliente");

            entity.Property(e => e.CedulaAsegurado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedulaAsegurado");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primerApellido");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("segundoApellido");
            entity.Property(e => e.TipoPersona)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipoPersona");
        });

        modelBuilder.Entity<Cobertura>(entity =>
        {
            entity.HasKey(e => e.IdCobertura).HasName("PK__cobertur__D10E344E56997A52");

            entity.ToTable("coberturas");

            entity.HasIndex(e => e.Cobertura1, "UQ__cobertur__E171AF43F57861D9").IsUnique();

            entity.Property(e => e.IdCobertura).HasColumnName("idCobertura");
            entity.Property(e => e.Cobertura1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cobertura");
        });

        modelBuilder.Entity<EstadoPoliza>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPoliza).HasName("PK__estadoPo__10CEFBED819267E0");

            entity.ToTable("estadoPoliza");

            entity.HasIndex(e => e.EstadoPoliza1, "UQ__estadoPo__34DA31ACD6878D04").IsUnique();

            entity.Property(e => e.IdEstadoPoliza).HasColumnName("idEstadoPoliza");
            entity.Property(e => e.EstadoPoliza1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("estadoPoliza");
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.NumeroPoliza).HasName("PK__polizas__641BF7CA1A3CDBCE");

            entity.ToTable("polizas");

            entity.Property(e => e.NumeroPoliza)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroPoliza");
            entity.Property(e => e.CedulaAsegurado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedulaAsegurado");
            entity.Property(e => e.FechaEmision).HasColumnName("fechaEmision");
            entity.Property(e => e.FechaInclusion).HasColumnName("fechaInclusion");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fechaVencimiento");
            entity.Property(e => e.IdAseguradora).HasColumnName("idAseguradora");
            entity.Property(e => e.IdCobertura).HasColumnName("idCobertura");
            entity.Property(e => e.IdEstadoPoliza).HasColumnName("idEstadoPoliza");
            entity.Property(e => e.IdTipoPoliza).HasColumnName("idTipoPoliza");
            entity.Property(e => e.MontoAsegurado)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("montoAsegurado");
            entity.Property(e => e.Periodo).HasColumnName("periodo");
            entity.Property(e => e.Prima)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("prima");

            entity.HasOne(d => d.CedulaAseguradoNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.CedulaAsegurado)
                .HasConstraintName("FK__polizas__cedulaA__44FF419A");

            entity.HasOne(d => d.IdAseguradoraNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdAseguradora)
                .HasConstraintName("FK__polizas__idAsegu__46E78A0C");

            entity.HasOne(d => d.IdCoberturaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdCobertura)
                .HasConstraintName("FK__polizas__idCober__47DBAE45");

            entity.HasOne(d => d.IdEstadoPolizaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdEstadoPoliza)
                .HasConstraintName("FK__polizas__idEstad__48CFD27E");

            entity.HasOne(d => d.IdTipoPolizaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdTipoPoliza)
                .HasConstraintName("FK__polizas__idTipoP__45F365D3");
        });

        modelBuilder.Entity<TipoPoliza>(entity =>
        {
            entity.HasKey(e => e.IdTipoPoliza).HasName("PK__tipoPoli__E307665A5412EE5E");

            entity.ToTable("tipoPoliza");

            entity.HasIndex(e => e.TipoPoliza1, "UQ__tipoPoli__739B2B16527807B6").IsUnique();

            entity.Property(e => e.IdTipoPoliza).HasColumnName("idTipoPoliza");
            entity.Property(e => e.TipoPoliza1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipoPoliza");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuarios__645723A6670BDBD2");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Usuario1, "UQ__usuarios__9AFF8FC63FB8887F").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
