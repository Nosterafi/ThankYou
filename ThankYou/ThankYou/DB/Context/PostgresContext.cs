using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ThankYou.DB.Domain;

namespace ThankYou.DB.Context;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BankCard> BankCards { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Merchant> Merchants { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ThankYou;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.BankCode).HasName("banks_pk");

            entity.ToTable("banks");

            entity.HasIndex(e => e.BankName, "banks_unique").IsUnique();

            entity.Property(e => e.BankCode)
                .HasColumnType("character varying")
                .HasColumnName("bank_code");
            entity.Property(e => e.BankName)
                .HasColumnType("character varying")
                .HasColumnName("bank_name");
        });

        modelBuilder.Entity<BankCard>(entity =>
        {
            entity.HasKey(e => e.CardNumber).HasName("bank_cards_pk");

            entity.ToTable("bank_cards");

            entity.Property(e => e.CardNumber)
                .HasColumnType("character varying")
                .HasColumnName("card_number");
            entity.Property(e => e.BankNumber)
                .HasColumnType("character varying")
                .HasColumnName("bank_number");
            entity.Property(e => e.Owner).HasColumnName("owner");

            entity.HasOne(d => d.BankNumberNavigation).WithMany(p => p.BankCards)
                .HasForeignKey(d => d.BankNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bank_cards_banks_fk");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pk");

            entity.ToTable("clients");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('peoples_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pk");

            entity.ToTable("employees");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('peoples_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.EmployeeRating).HasColumnName("employee_rating");
            entity.Property(e => e.MerchantId).HasColumnName("merchant_id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.Position)
                .HasColumnType("character varying")
                .HasColumnName("position");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");

            entity.HasOne(d => d.Merchant).WithMany(p => p.Employees)
                .HasForeignKey(d => d.MerchantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_merchants_fk");

            entity.HasOne(d => d.PositionNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Position)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_positions_fk");
        });

        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.HasKey(e => e.Inn).HasName("merchants_pk");

            entity.ToTable("merchants");

            entity.Property(e => e.Inn)
                .ValueGeneratedNever()
                .HasColumnName("inn");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Menu)
                .HasColumnType("character varying")
                .HasColumnName("menu");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("positions_pk");

            entity.ToTable("positions");

            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tips_pk");

            entity.ToTable("tips");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.Review)
                .HasColumnType("character varying")
                .HasColumnName("review");
            entity.Property(e => e.Sum).HasColumnName("sum");

            entity.HasOne(d => d.Client).WithMany(p => p.Tips)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("tips_clients_fk");

            entity.HasOne(d => d.Employee).WithMany(p => p.Tips)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tips_employees_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
