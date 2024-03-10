using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseReady.teabase;

public partial class TeaContext : DbContext
{
    public TeaContext()
    {
    }

    public TeaContext(DbContextOptions<TeaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Заказ> Заказs { get; set; }

    public virtual DbSet<Корзина> Корзинаs { get; set; }

    public virtual DbSet<КорзинаHasТовар> КорзинаHasТоварs { get; set; }

    public virtual DbSet<Пользователь> Пользовательs { get; set; }

    public virtual DbSet<Товар> Товарs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;port=3306;Database=tea;Uid=root;pwd=87654321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Заказ>(entity =>
        {
            entity.HasKey(e => e.IdЗаказ).HasName("PRIMARY");

            entity.ToTable("заказ");

            entity.HasIndex(e => e.ПользовательIdПользователь, "fk_заказ_пользователь1_idx");

            entity.Property(e => e.IdЗаказ).HasColumnName("id_заказ");
            entity.Property(e => e.Адрес).HasMaxLength(200);
            entity.Property(e => e.ОбщаяСтоимость).HasColumnName("Общая стоимость");
            entity.Property(e => e.ПользовательIdПользователь).HasColumnName("пользователь_Id_Пользователь");
            entity.Property(e => e.СпособОплаты)
                .HasMaxLength(45)
                .HasColumnName("Способ оплаты");
            entity.Property(e => e.Товар).HasMaxLength(45);

            entity.HasOne(d => d.ПользовательIdПользовательNavigation).WithMany(p => p.Заказs)
                .HasForeignKey(d => d.ПользовательIdПользователь)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_заказ_пользователь1");
        });

        modelBuilder.Entity<Корзина>(entity =>
        {
            entity.HasKey(e => e.IdКорзина).HasName("PRIMARY");

            entity.ToTable("корзина");

            entity.HasIndex(e => e.ПользовательIdПользователь, "fk_корзина_пользователь1_idx");

            entity.Property(e => e.IdКорзина).HasColumnName("id_корзина");
            entity.Property(e => e.ПользовательIdПользователь).HasColumnName("пользователь_Id_Пользователь");

            entity.HasOne(d => d.ПользовательIdПользовательNavigation).WithMany(p => p.Корзинаs)
                .HasForeignKey(d => d.ПользовательIdПользователь)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_корзина_пользователь1");
        });

        modelBuilder.Entity<КорзинаHasТовар>(entity =>
        {
            entity.HasKey(e => new { e.КорзинаIdКорзина, e.ТоварIdТовар }).HasName("PRIMARY");

            entity.ToTable("корзина_has_товар");

            entity.HasIndex(e => e.КорзинаIdКорзина, "fk_корзина_has_товар_корзина1_idx");

            entity.HasIndex(e => e.ТоварIdТовар, "fk_корзина_has_товар_товар1_idx");

            entity.Property(e => e.ОбщаяСтоимость).HasColumnName("Общая стоимость");

            entity.HasOne(d => d.КорзинаIdКорзинаNavigation).WithMany(p => p.КорзинаHasТоварs)
                .HasForeignKey(d => d.КорзинаIdКорзина)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_корзина_has_товар_корзина1");

            entity.HasOne(d => d.ТоварIdТоварNavigation).WithMany(p => p.КорзинаHasТоварs)
                .HasForeignKey(d => d.ТоварIdТовар)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_корзина_has_товар_товар1");
        });

        modelBuilder.Entity<Пользователь>(entity =>
        {
            entity.HasKey(e => e.IdПользователь).HasName("PRIMARY");

            entity.ToTable("пользователь");

            entity.Property(e => e.IdПользователь).HasColumnName("Id_Пользователь");
            entity.Property(e => e.Логин).HasMaxLength(45);
            entity.Property(e => e.Пароль).HasMaxLength(45);
            entity.Property(e => e.Роль).HasMaxLength(45);
        });

        modelBuilder.Entity<Товар>(entity =>
        {
            entity.HasKey(e => e.IdТовар).HasName("PRIMARY");

            entity.ToTable("товар");

            entity.Property(e => e.IdТовар).HasColumnName("id_товар");
            entity.Property(e => e.Категория).HasMaxLength(45);
            entity.Property(e => e.Количество).HasMaxLength(45);
            entity.Property(e => e.Название).HasMaxLength(45);
            entity.Property(e => e.Стоимость).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
