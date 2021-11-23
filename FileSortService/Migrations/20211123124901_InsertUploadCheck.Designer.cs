﻿// <auto-generated />
using System;
using FileSortService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FileSortService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211123124901_InsertUploadCheck")]
    partial class InsertUploadCheck
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FileSortService.Model.DatabaseModel.ArchitectureFolder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("dateCreatedFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fileInFolder")
                        .HasColumnType("int");

                    b.Property<bool>("isFolder")
                        .HasColumnType("bit");

                    b.Property<string>("linkToOpen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nameFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pathfolder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sizeFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("typeCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("typeFile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("typeCategoryId");

                    b.ToTable("Architecture");
                });

            modelBuilder.Entity("FileSortService.Model.DatabaseModel.ExtensionCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("nameCategory")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("ExtenCategory");
                });

            modelBuilder.Entity("FileSortService.Model.DatabaseModel.ExtensionValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("extensionCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("extensionValue")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("extensionCategoryId");

                    b.ToTable("ExtenValue");
                });

            modelBuilder.Entity("FileSortService.Model.DatabaseModel.TypeFileFromUpload", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("hexSignature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("typeCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("typeFile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("typeCategoryId");

                    b.ToTable("UploadCheck");
                });

            modelBuilder.Entity("FileSortService.Model.DatabaseModel.ArchitectureFolder", b =>
                {
                    b.HasOne("FileSortService.Model.DatabaseModel.ExtensionCategory", "typeCategory")
                        .WithMany("architectureFolder")
                        .HasForeignKey("typeCategoryId");

                    b.Navigation("typeCategory");
                });

            modelBuilder.Entity("FileSortService.Model.DatabaseModel.ExtensionValue", b =>
                {
                    b.HasOne("FileSortService.Model.DatabaseModel.ExtensionCategory", "extensionCategory")
                        .WithMany("extensionValue")
                        .HasForeignKey("extensionCategoryId");

                    b.Navigation("extensionCategory");
                });

            modelBuilder.Entity("FileSortService.Model.DatabaseModel.TypeFileFromUpload", b =>
                {
                    b.HasOne("FileSortService.Model.DatabaseModel.ExtensionCategory", "extensionCategory")
                        .WithMany("typeFileFromUploads")
                        .HasForeignKey("typeCategoryId");

                    b.Navigation("extensionCategory");
                });

            modelBuilder.Entity("FileSortService.Model.DatabaseModel.ExtensionCategory", b =>
                {
                    b.Navigation("architectureFolder");

                    b.Navigation("extensionValue");

                    b.Navigation("typeFileFromUploads");
                });
#pragma warning restore 612, 618
        }
    }
}
