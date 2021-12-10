﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-rc.1.21452.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CaseSubCategory", b =>
                {
                    b.Property<int>("CasesId")
                        .HasColumnType("int");

                    b.Property<int>("SubCategoriesId")
                        .HasColumnType("int");

                    b.HasKey("CasesId", "SubCategoriesId");

                    b.HasIndex("SubCategoriesId");

                    b.ToTable("CaseSubCategory");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.BaseCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Cases");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseCase");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.CaseImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.CaseUpdate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("SendToReporter")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("CaseUpdate");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("Miscellaneous")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.ReCaptchaAuthorization", b =>
                {
                    b.Property<Guid>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("DeviceId");

                    b.ToTable("PreAuthorizations");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Case", b =>
                {
                    b.HasBaseType("Commentor.GivEtPraj.Domain.Entities.BaseCase");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Case");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.MiscellaneousCase", b =>
                {
                    b.HasBaseType("Commentor.GivEtPraj.Domain.Entities.BaseCase");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("MiscellaneousCase");
                });

            modelBuilder.Entity("CaseSubCategory", b =>
                {
                    b.HasOne("Commentor.GivEtPraj.Domain.Entities.Case", null)
                        .WithMany()
                        .HasForeignKey("CasesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Commentor.GivEtPraj.Domain.Entities.SubCategory", null)
                        .WithMany()
                        .HasForeignKey("SubCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.BaseCase", b =>
                {
                    b.HasOne("Commentor.GivEtPraj.Domain.Entities.Category", "Category")
                        .WithMany("Cases")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Commentor.GivEtPraj.Domain.ValueObjects.GeographicLocation", "GeographicLocation", b1 =>
                        {
                            b1.Property<int>("BaseCaseId")
                                .HasColumnType("int");

                            b1.Property<double>("Latitude")
                                .HasColumnType("float");

                            b1.Property<double>("Longitude")
                                .HasColumnType("float");

                            b1.HasKey("BaseCaseId");

                            b1.ToTable("Cases");

                            b1.WithOwner()
                                .HasForeignKey("BaseCaseId");
                        });

                    b.Navigation("Category");

                    b.Navigation("GeographicLocation")
                        .IsRequired();
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.CaseImage", b =>
                {
                    b.HasOne("Commentor.GivEtPraj.Domain.Entities.BaseCase", "Case")
                        .WithMany("Images")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.CaseUpdate", b =>
                {
                    b.HasOne("Commentor.GivEtPraj.Domain.Entities.BaseCase", "BaseCase")
                        .WithMany("CaseUpdates")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseCase");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Category", b =>
                {
                    b.OwnsOne("Commentor.GivEtPraj.Domain.ValueObjects.LocalizedString", "Name", b1 =>
                        {
                            b1.Property<int>("CategoryId")
                                .HasColumnType("int");

                            b1.Property<string>("Danish")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.Property<string>("English")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.HasKey("CategoryId");

                            b1.ToTable("Categories");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.SubCategory", b =>
                {
                    b.HasOne("Commentor.GivEtPraj.Domain.Entities.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Commentor.GivEtPraj.Domain.ValueObjects.LocalizedString", "Name", b1 =>
                        {
                            b1.Property<int>("SubCategoryId")
                                .HasColumnType("int");

                            b1.Property<string>("Danish")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.Property<string>("English")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.HasKey("SubCategoryId");

                            b1.ToTable("SubCategories");

                            b1.WithOwner()
                                .HasForeignKey("SubCategoryId");
                        });

                    b.Navigation("Category");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.BaseCase", b =>
                {
                    b.Navigation("CaseUpdates");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("Commentor.GivEtPraj.Domain.Entities.Category", b =>
                {
                    b.Navigation("Cases");

                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
