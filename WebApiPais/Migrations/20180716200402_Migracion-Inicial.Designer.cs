﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApiPais.Models;

namespace WebApiPais.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20180716200402_Migracion-Inicial")]
    partial class MigracionInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiPais.Models.Pais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("WebApiPais.Models.Provincia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .HasMaxLength(30);

                    b.Property<int>("PaisId");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.ToTable("Provincias");
                });

            modelBuilder.Entity("WebApiPais.Models.Provincia", b =>
                {
                    b.HasOne("WebApiPais.Models.Pais", "Pais")
                        .WithMany("Provincias")
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
