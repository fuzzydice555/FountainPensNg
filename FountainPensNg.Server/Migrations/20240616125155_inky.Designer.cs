﻿// <auto-generated />
using System;
using FountainPensNg.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FountainPensNg.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240616125155_inky")]
    partial class inky
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FountainPensNg.Server.Data.Models.FountainPen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("CurrentInkId")
                        .HasColumnType("integer");

                    b.Property<int?>("CurrentInkRating")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Maker")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nib")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurrentInkId");

                    b.ToTable("FountainPens");
                });

            modelBuilder.Entity("FountainPensNg.Server.Data.Models.Ink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("Color_CIELAB_L")
                        .HasColumnType("double precision");

                    b.Property<double?>("Color_CIELAB_a")
                        .HasColumnType("double precision");

                    b.Property<double?>("Color_CIELAB_b")
                        .HasColumnType("double precision");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("InkName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("InsertedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Maker")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Maker", "InkName")
                        .IsUnique();

                    b.ToTable("Inks");
                });

            modelBuilder.Entity("FountainPensNg.Server.Data.Models.InkedUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FountainPenId")
                        .HasColumnType("integer");

                    b.Property<int>("InkId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InkedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MatchRating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FountainPenId");

                    b.HasIndex("InkId");

                    b.ToTable("InkedUps");
                });

            modelBuilder.Entity("FountainPensNg.Server.Data.Models.FountainPen", b =>
                {
                    b.HasOne("FountainPensNg.Server.Data.Models.Ink", "CurrentInk")
                        .WithMany("CurrentPens")
                        .HasForeignKey("CurrentInkId");

                    b.Navigation("CurrentInk");
                });

            modelBuilder.Entity("FountainPensNg.Server.Data.Models.InkedUp", b =>
                {
                    b.HasOne("FountainPensNg.Server.Data.Models.FountainPen", "FountainPen")
                        .WithMany("InkedUps")
                        .HasForeignKey("FountainPenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FountainPensNg.Server.Data.Models.Ink", "Ink")
                        .WithMany("InkedUps")
                        .HasForeignKey("InkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FountainPen");

                    b.Navigation("Ink");
                });

            modelBuilder.Entity("FountainPensNg.Server.Data.Models.FountainPen", b =>
                {
                    b.Navigation("InkedUps");
                });

            modelBuilder.Entity("FountainPensNg.Server.Data.Models.Ink", b =>
                {
                    b.Navigation("CurrentPens");

                    b.Navigation("InkedUps");
                });
#pragma warning restore 612, 618
        }
    }
}
