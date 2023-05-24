using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto1Inventario;

public partial class InventarioContext : DbContext
{
    public InventarioContext()
    {
    }

    public InventarioContext(DbContextOptions<InventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Detalle> Detalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoSucursal> ProductoSucursals { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost; database=inventario; user=root; password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Detalle>(entity =>
        {
            entity.HasKey(e => new { e.ProductoId, e.VentaId, e.Id }).HasName("PRIMARY");

            entity.ToTable("detalle");

            entity.HasIndex(e => e.VentaId, "FKDetalle543139");

            entity.Property(e => e.ProductoId).HasColumnType("int(10)");
            entity.Property(e => e.VentaId).HasColumnType("int(10)");
            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Cantidad).HasColumnType("int(11)");
            entity.Property(e => e.Precio).HasColumnType("int(10)");

            entity.HasOne(d => d.Producto).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FKDetalle785427");

            entity.HasOne(d => d.Venta).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FKDetalle543139");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.Codigo, "Codigo").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(10)");
            entity.Property(e => e.Codigo).HasColumnType("int(10)");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Precio).HasColumnType("int(10)");
            entity.Property(e => e.VentaId).HasColumnType("int(10)");
        });

        modelBuilder.Entity<ProductoSucursal>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ProductoId, e.SucursalId }).HasName("PRIMARY");

            entity.ToTable("producto_sucursal");

            entity.HasIndex(e => e.ProductoId, "FKProducto_S153090");

            entity.HasIndex(e => e.SucursalId, "FKProducto_S392462");

            entity.Property(e => e.Id).HasColumnType("int(10)");
            entity.Property(e => e.ProductoId).HasColumnType("int(10)");
            entity.Property(e => e.SucursalId).HasColumnType("int(10)");
            entity.Property(e => e.Stock).HasColumnType("int(10)");

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductoSucursals)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FKProducto_S153090");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.ProductoSucursals)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FKProducto_S392462");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sucursal");

            entity.Property(e => e.Id).HasColumnType("int(10)");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("venta");

            entity.HasIndex(e => e.Correlativo, "Correlativo").IsUnique();

            entity.HasIndex(e => e.SucursalId, "FKVenta815054");

            entity.Property(e => e.Id).HasColumnType("int(10)");
            entity.Property(e => e.Correlativo).HasColumnType("int(11)");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Hora).HasColumnType("time");
            entity.Property(e => e.RutCliente).HasMaxLength(255);
            entity.Property(e => e.SucursalId).HasColumnType("int(10)");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Venta)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FKVenta815054");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
