using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseReady.teabase;

public partial class MagazContext : DbContext
{
    public MagazContext()
    {
    }

    public MagazContext(DbContextOptions<MagazContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Заказ> Заказs { get; set; }

    public virtual DbSet<Корзина> Корзинаs { get; set; }

    public virtual DbSet<Пользователь> Пользовательs { get; set; }

    public virtual DbSet<Товар> Товарs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;port=3306;Database=magaz;Uid=root;pwd=fder22222222;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Заказ>(entity =>
        {
            entity.HasKey(e => e.IdЗаказ).HasName("PRIMARY");

            entity.ToTable("заказ");

            entity.HasIndex(e => e.IdПользователя, "id_пользователь_idx");

            entity.Property(e => e.IdЗаказ).HasColumnName("id_заказ");
            entity.Property(e => e.IdПользователя).HasColumnName("id_пользователя");
            entity.Property(e => e.Адрес).HasMaxLength(200);
            entity.Property(e => e.ОбщаяСтоимость).HasColumnName("Общая стоимость");
            entity.Property(e => e.СпособОплаты)
                .HasMaxLength(200)
                .HasColumnName("Способ оплаты");

            entity.HasOne(d => d.IdПользователяNavigation).WithMany(p => p.Заказs)
                .HasForeignKey(d => d.IdПользователя)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_пользователя");
        });

        modelBuilder.Entity<Корзина>(entity =>
        {
            entity.HasKey(e => e.IdКорзина).HasName("PRIMARY");

            entity.ToTable("корзина");

            entity.HasIndex(e => e.ПользовательIdПользователь, "id_корзина_idx");

            entity.Property(e => e.IdКорзина).HasColumnName("id_корзина");
            entity.Property(e => e.ОбщаяСтоимость).HasColumnName("Общая стоимость");
            entity.Property(e => e.ПользовательIdПользователь).HasColumnName("пользователь_Id_пользователь");

            entity.HasOne(d => d.ПользовательIdПользовательNavigation).WithMany(p => p.Корзинаs)
                .HasForeignKey(d => d.ПользовательIdПользователь)
                .HasConstraintName("id_корзина");

            entity.HasMany(d => d.ТоварIdТоварs).WithMany(p => p.КорзинаIdКорзинаs)
                .UsingEntity<Dictionary<string, object>>(
                    "КорзинаHasТовар",
                    r => r.HasOne<Товар>().WithMany()
                        .HasForeignKey("ТоварIdТовар")
                        .HasConstraintName("fk_корзина_has_товар_товар1"),
                    l => l.HasOne<Корзина>().WithMany()
                        .HasForeignKey("КорзинаIdКорзина")
                        .HasConstraintName("fk_корзина_has_товар_корзина1"),
                    j =>
                    {
                        j.HasKey("КорзинаIdКорзина", "ТоварIdТовар").HasName("PRIMARY");
                        j.ToTable("корзина_has_товар");
                        j.HasIndex(new[] { "КорзинаIdКорзина" }, "fk_корзина_has_товар_корзина1_idx");
                        j.HasIndex(new[] { "ТоварIdТовар" }, "fk_корзина_has_товар_товар1_idx");
                    });
        });

        modelBuilder.Entity<Пользователь>(entity =>
        {
            entity.HasKey(e => e.IdПользователь).HasName("PRIMARY");

            entity.ToTable("пользователь");

            entity.Property(e => e.IdПользователь).HasColumnName("Id_пользователь");
            entity.Property(e => e.Логин)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Пароль)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Роль)
                .IsRequired()
                .HasMaxLength(200);
        });

        modelBuilder.Entity<Товар>(entity =>
        {
            entity.HasKey(e => e.IdТовар).HasName("PRIMARY");

            entity.ToTable("товар");

            entity.Property(e => e.IdТовар).HasColumnName("id_товар");
            entity.Property(e => e.Категория).HasMaxLength(200);
            entity.Property(e => e.Название)
                .IsRequired()
                .HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
