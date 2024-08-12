using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GEBIntegrador.Dominio;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }
    public virtual DbSet<categorias_parqueadero> categorias_parqueaderos { get; set; }

    public virtual DbSet<menu> menus { get; set; }

    public virtual DbSet<menu_perfil> menu_perfils { get; set; }

    public virtual DbSet<niveles> niveles { get; set; }

    public virtual DbSet<parametro> parametros { get; set; }

    public virtual DbSet<perfile> perfiles { get; set; }

    public virtual DbSet<permiso> permisos { get; set; }

    public virtual DbSet<permisos_perfil> permisos_perfils { get; set; }

    public virtual DbSet<persona> personas { get; set; }

    public virtual DbSet<recurso> recursos { get; set; }

    public virtual DbSet<reserva> reservas { get; set; }

    public virtual DbSet<tipos_recurso> tipos_recursos { get; set; }

    public virtual DbSet<tipos_reserva> tipos_reservas { get; set; }

    public virtual DbSet<usuario> usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
  
        modelBuilder.Entity<categorias_parqueadero>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__tipos_mo__7371E14E3A5CC62D");

            entity.ToTable("categorias_parqueadero");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.v_descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<menu>(entity =>
        {
            entity.HasKey(e => e.n_id_menu).HasName("PK__Menu__68A1D9DBC8DACE31");

            entity.ToTable("menu");

            entity.HasIndex(e => e.n_id_menu_padre, "IX_Menu_id_menu_padre");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.v_descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.v_url)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.n_id_menu_padreNavigation).WithMany(p => p.Inversen_id_menu_padreNavigation)
                .HasForeignKey(d => d.n_id_menu_padre)
                .HasConstraintName("FK__Menu__id_menu_pa__48CFD27E");
        });

        modelBuilder.Entity<menu_perfil>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__menu_per__3213E83F7A911AF2");

            entity.ToTable("menu_perfil");

            entity.HasIndex(e => e.n_id_menu, "IX_menu_perfil_id_menu");

            entity.HasIndex(e => e.n_id_perfil, "IX_menu_perfil_id_perfil");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.n_id_menuNavigation).WithMany(p => p.menu_perfils)
                .HasForeignKey(d => d.n_id_menu)
                .HasConstraintName("FK__menu_perf__id_me__5441852A");

            entity.HasOne(d => d.n_id_perfilNavigation).WithMany(p => p.menu_perfils)
                .HasForeignKey(d => d.n_id_perfil)
                .HasConstraintName("FK__menu_perf__id_pe__5535A963");
        });

        modelBuilder.Entity<niveles>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__niveles__7371E14E127273B8");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.v_descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<parametro>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__parametr__3213E83F70AE321F");

            entity.Property(e => e.d_fecha_actualiza)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.v_descripcion_parametro).IsUnicode(false);
            entity.Property(e => e.v_valor)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<perfile>(entity =>
        {
            entity.HasKey(e => e.n_id);

            entity.Property(e => e.n_estado)
                .HasDefaultValueSql("((1))")
                .HasComment("0-InActivo 1-Activo");
            entity.Property(e => e.v_descripcion).IsUnicode(false);
            entity.Property(e => e.v_nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<permiso>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__permisos__3213E83FD9756A20");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.v_descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.v_nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<permisos_perfil>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__permisos__3213E83FF964A87E");

            entity.ToTable("permisos_perfil");

            entity.HasIndex(e => e.n_id_perfil, "IX_permisos_perfil_id_perfil");

            entity.HasIndex(e => e.n_id_permiso, "IX_permisos_perfil_id_permiso");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.n_id_perfilNavigation).WithMany(p => p.permisos_perfils)
                .HasForeignKey(d => d.n_id_perfil)
                .HasConstraintName("FK__permisos___id_pe__534D60F1");

            entity.HasOne(d => d.n_id_permisoNavigation).WithMany(p => p.permisos_perfils)
                .HasForeignKey(d => d.n_id_permiso)
                .HasConstraintName("FK__permisos___id_pe__5441852A");
        });

        modelBuilder.Entity<persona>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__asistent__7371E14EC1BE642C");

            entity.ToTable("persona");

            entity.Property(e => e.v_apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.v_documento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.v_nombres)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<recurso>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__recursos__7371E14E64B9DD21");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.v_nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.v_observaciones).IsUnicode(false);

            entity.HasOne(d => d.n_categoria_parqueaderoNavigation).WithMany(p => p.recursos)
                .HasForeignKey(d => d.n_categoria_parqueadero)
                .HasConstraintName("FK__recursos__n_moda__4E53A1AA");

            entity.HasOne(d => d.n_nivel_recursoNavigation).WithMany(p => p.recursos)
                .HasForeignKey(d => d.n_nivel_recurso)
                .HasConstraintName("FK__recursos__n_nive__5224328E");

            entity.HasOne(d => d.n_tipo_recursoNavigation).WithMany(p => p.recursos)
                .HasForeignKey(d => d.n_tipo_recurso)
                .HasConstraintName("FK__recursos__n_tipo__4D5F7D71");
        });

        modelBuilder.Entity<reserva>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__reservas__7371E14EA0F7393F");

            entity.Property(e => e.d_fecha_cancela).HasColumnType("datetime");
            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.v_observaciones).IsUnicode(false);
            entity.Property(e => e.v_placa_vehiculo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.n_id_recursoNavigation).WithMany(p => p.reservas)
                .HasForeignKey(d => d.n_id_recurso)
                .HasConstraintName("FK__reservas__n_id_r__3C34F16F");

            entity.HasOne(d => d.n_id_usuarioNavigation).WithMany(p => p.reservas)
                .HasForeignKey(d => d.n_id_usuario)
                .HasConstraintName("FK__reservas__n_id_u__3D2915A8");

            entity.HasOne(d => d.n_tipo_reservaNavigation).WithMany(p => p.reservas)
                .HasForeignKey(d => d.n_tipo_reserva)
                .HasConstraintName("FK__reservas__n_tipo__5F7E2DAC");
        });

        modelBuilder.Entity<tipos_recurso>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__tipos_re__7371E14E979742CC");

            entity.ToTable("tipos_recurso");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.v_descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tipos_reserva>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__tipos_re__7371E14E017050C2");

            entity.ToTable("tipos_reserva");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.v_descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.n_id).HasName("PK__usuarios__3213E83FC8D31053");

            entity.HasIndex(e => e.n_id_perfil, "IX_usuarios_id_perfil");

            entity.Property(e => e.n_estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.n_id_contrato).HasComment("Se asigna contrato cuando es contratista");
           // entity.Property(e => e.n_id_subestacion).HasComment("Se asigna subestación cuando es colaborador");
            entity.Property(e => e.v_clave).IsUnicode(false);
            entity.Property(e => e.v_correo).IsUnicode(false);
            entity.Property(e => e.v_usuario)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.n_id_perfilNavigation).WithMany(p => p.usuarios)
                .HasForeignKey(d => d.n_id_perfil)
                .HasConstraintName("FK__usuarios__id_per__5EBF139D");

            entity.HasOne(d => d.n_id_personaNavigation).WithMany(p => p.usuarios)
                .HasForeignKey(d => d.n_id_persona)
                .HasConstraintName("FK__usuarios__n_id_p__2022C2A6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
