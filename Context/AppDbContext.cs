using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using apisafeguardpro.Models;

namespace apisafeguardpro.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colaborador> Colaboradors { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<Epi> Epis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=safeguardpro;User Id=postgres;Password=senai901;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.ColaboradorCod).HasName("colaborador_pkey");

            entity.ToTable("colaborador");

            entity.HasIndex(e => e.Cpf, "cpf").IsUnique();

            entity.HasIndex(e => e.ColaboradorCod, "nome_colab");

            entity.HasIndex(e => e.Telefone, "telefone").IsUnique();

            entity.Property(e => e.ColaboradorCod)
                .ValueGeneratedNever()
                .HasColumnName("colaborador_cod");
            entity.Property(e => e.Cpf).HasColumnName("cpf");
            entity.Property(e => e.Ctps).HasColumnName("ctps");
            entity.Property(e => e.DataAdmissao).HasColumnName("data_admissao");
            entity.Property(e => e.Email).HasMaxLength(60).HasColumnName("email");
            entity.Property(e => e.NomeColab)
                .HasMaxLength(100)
                .HasColumnName("nome_colab");
            entity.Property(e => e.Telefone).HasMaxLength(20).HasColumnName("telefone");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.EntregaCod).HasName("entrega_pkey");

            entity.ToTable("entrega");

            entity.HasIndex(e => e.EntregaCod, "idx_entrega_cod");

            entity.Property(e => e.EntregaCod)
                .ValueGeneratedNever()
                .HasColumnName("entrega_cod");
            entity.Property(e => e.ColaboradorCod).HasColumnName("colaborador_cod");
            entity.Property(e => e.DataEntrega).HasColumnName("data_entrega");
            entity.Property(e => e.DataValidade).HasColumnName("data_validade");
            entity.Property(e => e.EpiCod).HasColumnName("epi_cod");

            entity.HasOne(d => d.ColaboradorCodNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.ColaboradorCod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("colaborador_cod");

            entity.HasOne(d => d.EpiCodNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.EpiCod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("epi_cod");
        });

        modelBuilder.Entity<Epi>(entity =>
        {
            entity.HasKey(e => e.EpiCod).HasName("epi_pkey");

            entity.ToTable("epi");

            entity.HasIndex(e => e.EpiCod, "idx_epi_cod");

            entity.Property(e => e.EpiCod)
                .ValueGeneratedNever()
                .HasColumnName("epi_cod");
            entity.Property(e => e.ColaboradorCod).HasColumnName("colaborador_cod");
            entity.Property(e => e.FormaAdequada)
                .HasMaxLength(200)
                .HasColumnName("forma_adequada");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");

            entity.HasOne(d => d.ColaboradorCodNavigation).WithMany(p => p.Epis)
                .HasForeignKey(d => d.ColaboradorCod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("colaborador_cod");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
