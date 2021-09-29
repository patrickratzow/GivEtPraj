﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210929111110_CaseCanOnlyHaveOneCategory")]
    partial class CaseCanOnlyHaveOneCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-rc.1.21452.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("nvarchar(127)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Case", b =>
                {
                    b.HasOne("Commentor.GivEtPraj.Domain.Entities.Category", "Category")
                        .WithMany("Cases")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Picture", b =>
                {
                    b.HasOne("Commentor.GivEtPraj.Domain.Entities.Case", "Case")
                        .WithMany("Pictures")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Case", b =>
                {
                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Category", b =>
                {
                    b.Navigation("Cases");
                });
#pragma warning restore 612, 618
        }
    }
}
