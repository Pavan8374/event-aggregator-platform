﻿// <auto-generated />
using System;
using Event.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Event.Infrastructure.Migrations
{
    [DbContext(typeof(EventDbContext))]
    [Migration("20250226124032_Add_Events")]
    partial class Add_Events
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Event.Domain.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Category");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("ImageUrl");

                    b.Property<bool>("IsFree")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("IsFree");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uuid")
                        .HasColumnName("OrganizerId");

                    b.Property<string>("OrganizerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("OrganizerName");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Status")
                        .HasDefaultValueSql("'Pending'");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Title");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Venue");

                    b.HasKey("Id");

                    b.ToTable("Events", (string)null);
                });

            modelBuilder.Entity("Event.Domain.Entities.Event", b =>
                {
                    b.OwnsOne("Event.Domain.ValueObjects.Events.Capacity", "Capacity", b1 =>
                        {
                            b1.Property<Guid>("EventId")
                                .HasColumnType("uuid");

                            b1.Property<int>("AvailableSeats")
                                .HasColumnType("integer")
                                .HasColumnName("AvailableSeats");

                            b1.Property<int>("TotalSeats")
                                .HasColumnType("integer")
                                .HasColumnName("TotalSeats");

                            b1.HasKey("EventId");

                            b1.ToTable("Events");

                            b1.WithOwner()
                                .HasForeignKey("EventId");
                        });

                    b.OwnsOne("Event.Domain.ValueObjects.Events.EventTimeRange", "TimeRange", b1 =>
                        {
                            b1.Property<Guid>("EventId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("EndTime")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("EndTime");

                            b1.Property<DateTime>("StartTime")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("StartTime");

                            b1.HasKey("EventId");

                            b1.ToTable("Events");

                            b1.WithOwner()
                                .HasForeignKey("EventId");
                        });

                    b.OwnsOne("Event.Domain.ValueObjects.Events.Money", "TicketPrice", b1 =>
                        {
                            b1.Property<Guid>("EventId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("decimal(10,2)")
                                .HasDefaultValue(0m)
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)")
                                .HasColumnName("Currency")
                                .HasDefaultValueSql("'INR'");

                            b1.HasKey("EventId");

                            b1.ToTable("Events");

                            b1.WithOwner()
                                .HasForeignKey("EventId");
                        });

                    b.Navigation("Capacity")
                        .IsRequired();

                    b.Navigation("TicketPrice")
                        .IsRequired();

                    b.Navigation("TimeRange")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
