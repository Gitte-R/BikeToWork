﻿// <auto-generated />
using System;
using BikeToWork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BikeToWork.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230210190558_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BikeToWork.Data.Models.BikeRide", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<byte>("Distance")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("participantId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("participantId");

                    b.ToTable("BikeRides");
                });

            modelBuilder.Entity("BikeToWork.Data.Models.Participant", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("bikeClass")
                        .HasColumnType("int");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("team")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("BikeToWork.Data.Models.BikeRide", b =>
                {
                    b.HasOne("BikeToWork.Data.Models.Participant", null)
                        .WithMany("listOfBikeRides")
                        .HasForeignKey("participantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BikeToWork.Data.Models.Participant", b =>
                {
                    b.Navigation("listOfBikeRides");
                });
#pragma warning restore 612, 618
        }
    }
}
