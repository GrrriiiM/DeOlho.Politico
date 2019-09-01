﻿// <auto-generated />
using System;
using DeOlho.SeedWork.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeOlho.Politico.Migrations
{
    [DbContext(typeof(DeOlhoDbContext))]
    partial class DeOlhoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DeOlho.Politico.Domain.Cargo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abrangencia");

                    b.Property<long>("AbrangenciaId");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.Eleicao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Ano");

                    b.Property<DateTime>("Data");

                    b.Property<string>("Descricao");

                    b.Property<long>("TipoId");

                    b.HasKey("Id");

                    b.HasIndex("TipoId");

                    b.ToTable("Eleicao");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.GrauInstrucao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("GrauInstrucao");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.Mandato", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abrangencia");

                    b.Property<long>("CargoId");

                    b.Property<long>("EleicaoId");

                    b.Property<long>("PartidoId");

                    b.Property<long>("PoliticoId");

                    b.Property<long>("SituacaoId");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("EleicaoId");

                    b.HasIndex("PartidoId");

                    b.HasIndex("PoliticoId");

                    b.HasIndex("SituacaoId");

                    b.ToTable("Mandato");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.MandatoSituacao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("MandatoSituacao");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.Ocupacao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("Ocupacao");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.Partido", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<string>("Sigla");

                    b.HasKey("Id");

                    b.ToTable("Partido");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.Politico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apelido");

                    b.Property<long>("CPF");

                    b.Property<string>("CidadeNascimento");

                    b.Property<DateTime>("DataInformacao");

                    b.Property<DateTime>("DataNascimento");

                    b.Property<long>("GrauInstrucaoId");

                    b.Property<string>("Nome");

                    b.Property<long>("OcupacaoId");

                    b.Property<long>("SexoId");

                    b.Property<string>("TermoPesquisa");

                    b.Property<string>("UFNascimento");

                    b.HasKey("Id");

                    b.HasIndex("CPF");

                    b.HasIndex("GrauInstrucaoId");

                    b.HasIndex("OcupacaoId");

                    b.HasIndex("SexoId");

                    b.ToTable("Politico");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.Sexo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("Sexo");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.TipoEleicao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("TipoEleicao");
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.Eleicao", b =>
                {
                    b.HasOne("DeOlho.Politico.Domain.TipoEleicao", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.Mandato", b =>
                {
                    b.HasOne("DeOlho.Politico.Domain.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeOlho.Politico.Domain.Eleicao", "Eleicao")
                        .WithMany()
                        .HasForeignKey("EleicaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeOlho.Politico.Domain.Partido", "Partido")
                        .WithMany()
                        .HasForeignKey("PartidoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeOlho.Politico.Domain.Politico", "Politico")
                        .WithMany("Mandatos")
                        .HasForeignKey("PoliticoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeOlho.Politico.Domain.MandatoSituacao", "Situacao")
                        .WithMany()
                        .HasForeignKey("SituacaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DeOlho.Politico.Domain.Politico", b =>
                {
                    b.HasOne("DeOlho.Politico.Domain.GrauInstrucao", "GrauInstrucao")
                        .WithMany()
                        .HasForeignKey("GrauInstrucaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeOlho.Politico.Domain.Ocupacao", "Ocupacao")
                        .WithMany()
                        .HasForeignKey("OcupacaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DeOlho.Politico.Domain.Sexo", "Sexo")
                        .WithMany()
                        .HasForeignKey("SexoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
