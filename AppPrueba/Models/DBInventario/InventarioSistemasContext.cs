using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppPrueba.Models.DBInventario;

public partial class InventarioSistemasContext : DbContext
{
    public InventarioSistemasContext()
    {
    }

    public InventarioSistemasContext(DbContextOptions<InventarioSistemasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Consumible> Consumibles { get; set; }

    public virtual DbSet<Hardware> Hardwares { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<ReparacionConsumible> ReparacionConsumibles { get; set; }

    public virtual DbSet<Reparacione> Reparaciones { get; set; }

    public virtual DbSet<Responsable> Responsables { get; set; }

    public virtual DbSet<TipoHardware> TipoHardwares { get; set; }

    public virtual DbSet<Ubicacione> Ubicaciones { get; set; }

    public virtual DbSet<VwHardwareCompleto> VwHardwareCompletos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:MiConexion");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Consumible>(entity =>
        {
            entity.HasKey(e => e.IdConsumible).HasName("PK__consumib__07574559EA061BCA");

            entity.ToTable("consumibles");

            entity.Property(e => e.IdConsumible).HasColumnName("id_consumible");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.StockActual).HasColumnName("stock_actual");
            entity.Property(e => e.StockMinimo).HasColumnName("stock_minimo");
        });

        modelBuilder.Entity<Hardware>(entity =>
        {
            entity.HasKey(e => e.IdHardware).HasName("PK__hardware__10F926A8A31C619F");

            entity.ToTable("hardware");

            entity.HasIndex(e => e.CodigoInventario, "UQ__hardware__2C4D9A17DE99919D").IsUnique();

            entity.HasIndex(e => e.NroSerie, "UQ__hardware__AD64A16181450E31").IsUnique();

            entity.Property(e => e.IdHardware).HasColumnName("id_hardware");
            entity.Property(e => e.CodigoInventario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo_inventario");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha_alta");
            entity.Property(e => e.FechaBaja).HasColumnName("fecha_baja");
            entity.Property(e => e.IdModelo).HasColumnName("id_modelo");
            entity.Property(e => e.NroSerie)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nro_serie");

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Hardwares)
                .HasForeignKey(d => d.IdModelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hardware_Modelo");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__marca__7E43E99E0E6352F8");

            entity.ToTable("marca");

            entity.HasIndex(e => e.Nombre, "UQ__marca__72AFBCC6CDD99D55").IsUnique();

            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.IdModelo).HasName("PK__modelo__B3BFCFF1DE636ABB");

            entity.ToTable("modelo");

            entity.HasIndex(e => new { e.IdMarca, e.IdTipo, e.NombreModelo }, "UQ__modelo__E6B68663A9161678").IsUnique();

            entity.Property(e => e.IdModelo).HasColumnName("id_modelo");
            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.IdTipo).HasColumnName("id_tipo");
            entity.Property(e => e.NombreModelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_modelo");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modelo_Marca");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modelo_Tipo");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__movimien__2A071C24B1BBE449");

            entity.ToTable("movimientos");

            entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");
            entity.Property(e => e.IdHardware).HasColumnName("id_hardware");
            entity.Property(e => e.IdResponsable).HasColumnName("id_responsable");
            entity.Property(e => e.IdUbicacion).HasColumnName("id_ubicacion");
            entity.Property(e => e.Observaciones)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_movimiento");

            entity.HasOne(d => d.IdHardwareNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdHardware)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mov_Hardware");

            entity.HasOne(d => d.IdResponsableNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdResponsable)
                .HasConstraintName("FK_Mov_Responsable");

            entity.HasOne(d => d.IdUbicacionNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdUbicacion)
                .HasConstraintName("FK_Mov_Ubicacion");
        });

        modelBuilder.Entity<ReparacionConsumible>(entity =>
        {
            entity.HasKey(e => e.IdReparacionConsumible).HasName("PK__reparaci__38AB6E9C9E80C8D3");

            entity.ToTable("reparacion_consumibles");

            entity.HasIndex(e => new { e.IdReparacion, e.IdConsumible }, "UQ_Rep_Cons").IsUnique();

            entity.Property(e => e.IdReparacionConsumible).HasColumnName("id_reparacion_consumible");
            entity.Property(e => e.Cantidad)
                .HasDefaultValue(1)
                .HasColumnName("cantidad");
            entity.Property(e => e.IdConsumible).HasColumnName("id_consumible");
            entity.Property(e => e.IdReparacion).HasColumnName("id_reparacion");

            entity.HasOne(d => d.IdConsumibleNavigation).WithMany(p => p.ReparacionConsumibles)
                .HasForeignKey(d => d.IdConsumible)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RepCons_Consumible");

            entity.HasOne(d => d.IdReparacionNavigation).WithMany(p => p.ReparacionConsumibles)
                .HasForeignKey(d => d.IdReparacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RepCons_Reparacion");
        });

        modelBuilder.Entity<Reparacione>(entity =>
        {
            entity.HasKey(e => e.IdReparacion).HasName("PK__reparaci__5253371F55814038");

            entity.ToTable("reparaciones");

            entity.Property(e => e.IdReparacion).HasColumnName("id_reparacion");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaFinalizacion).HasColumnName("fecha_finalizacion");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdHardware).HasColumnName("id_hardware");
            entity.Property(e => e.IdResponsable).HasColumnName("id_responsable");
            entity.Property(e => e.Observaciones)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.TipoReparacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipo_reparacion");

            entity.HasOne(d => d.IdHardwareNavigation).WithMany(p => p.Reparaciones)
                .HasForeignKey(d => d.IdHardware)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rep_Hardware");

            entity.HasOne(d => d.IdResponsableNavigation).WithMany(p => p.Reparaciones)
                .HasForeignKey(d => d.IdResponsable)
                .HasConstraintName("FK_Rep_Responsable");
        });

        modelBuilder.Entity<Responsable>(entity =>
        {
            entity.HasKey(e => e.IdResponsable).HasName("PK__responsa__99B1C6CE2CE54236");

            entity.ToTable("responsables");

            entity.Property(e => e.IdResponsable).HasColumnName("id_responsable");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Sector)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sector");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<TipoHardware>(entity =>
        {
            entity.HasKey(e => e.IdTipo).HasName("PK__tipo_har__CF9010890752D791");

            entity.ToTable("tipo_hardware");

            entity.HasIndex(e => e.Descripcion, "UQ__tipo_har__298336B6C7977D90").IsUnique();

            entity.Property(e => e.IdTipo).HasColumnName("id_tipo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Ubicacione>(entity =>
        {
            entity.HasKey(e => e.IdUbicacion).HasName("PK__ubicacio__81BAA591A352C871");

            entity.ToTable("ubicaciones");

            entity.Property(e => e.IdUbicacion).HasColumnName("id_ubicacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<VwHardwareCompleto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_HardwareCompleto");

            entity.Property(e => e.CodigoInventario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo_inventario");
            entity.Property(e => e.DescripcionHardware)
                .IsUnicode(false)
                .HasColumnName("descripcion_hardware");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaAlta).HasColumnName("fecha_alta");
            entity.Property(e => e.FechaBaja).HasColumnName("fecha_baja");
            entity.Property(e => e.IdHardware).HasColumnName("id_hardware");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.NombreModelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_modelo");
            entity.Property(e => e.NroSerie)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nro_serie");
            entity.Property(e => e.TipoHardware)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_hardware");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
