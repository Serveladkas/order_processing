using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace order_processing.Models;

public partial class MyDBContext : DbContext
{
    public MyDBContext()
    {
    }

    public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BasicSupplierInfo> BasicSupplierInfos { get; set; }

    public virtual DbSet<FullSupplierInfo> FullSupplierInfos { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductHasOrder> ProductHasOrders { get; set; }

    public virtual DbSet<ProductLog> ProductLogs { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=mydb;uid=root;pwd=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.42-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<BasicSupplierInfo>(entity =>
        {
            entity.ToView("basic_supplier_info");
        });

        modelBuilder.Entity<FullSupplierInfo>(entity =>
        {
            entity.ToView("full_supplier_info");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.Property(e => e.Status).HasDefaultValueSql("'ожидает оплаты'");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_supplier1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");
        });

        modelBuilder.Entity<ProductHasOrder>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.OrderId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.Quantity).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductHasOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_has_order_order1");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductHasOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_has_order_product");
        });

        modelBuilder.Entity<ProductLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_log_ibfk_1");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}