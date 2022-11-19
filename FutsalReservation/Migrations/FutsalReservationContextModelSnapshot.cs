﻿// <auto-generated />
using System;
using FutsalReservation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FutsalReservation.Migrations
{
    [DbContext(typeof(FutsalReservationContext))]
    partial class FutsalReservationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FutsalReservation.Data.Entities.Court", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Court");
                });

            modelBuilder.Entity("FutsalReservation.Data.Entities.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"), 1L, 1);

                    b.Property<int>("CourtId")
                        .HasColumnType("int");

                    b.Property<string>("ReservationDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReservationTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("CourtId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("FutsalReservation.Data.Entities.Timing", b =>
                {
                    b.Property<int>("TimingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TimingId"), 1L, 1);

                    b.Property<int?>("CourtId")
                        .HasColumnType("int");

                    b.Property<string>("endTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("startTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TimingId");

                    b.HasIndex("CourtId");

                    b.ToTable("Timing");
                });

            modelBuilder.Entity("FutsalReservation.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FutsalReservation.Data.Entities.Reservation", b =>
                {
                    b.HasOne("FutsalReservation.Data.Entities.Court", "Court")
                        .WithMany("Reservations")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FutsalReservation.Data.Entities.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Court");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FutsalReservation.Data.Entities.Timing", b =>
                {
                    b.HasOne("FutsalReservation.Data.Entities.Court", "Court")
                        .WithMany("Timings")
                        .HasForeignKey("CourtId");

                    b.Navigation("Court");
                });

            modelBuilder.Entity("FutsalReservation.Data.Entities.Court", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Timings");
                });

            modelBuilder.Entity("FutsalReservation.Data.Entities.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}