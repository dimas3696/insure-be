// <auto-generated />
using System;
using Insure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Insure.Migrations
{
    [DbContext(typeof(CrmContext))]
    [Migration("20210509102939_AddOffice")]
    partial class AddOffice
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.3.21201.2")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Insure.Models.CRM.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Logo")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Insure.Models.CRM.FAQ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("text");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Faqs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Answer = "22 years",
                            Question = "How old are you?"
                        },
                        new
                        {
                            Id = 2,
                            Answer = "Dmitriy",
                            Question = "What is your name?"
                        });
                });

            modelBuilder.Entity("Insure.Models.CRM.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("Insure.Models.CRM.Schedules.DaySchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<DateTime>("From")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("To")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("WeekScheduleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WeekScheduleId");

                    b.ToTable("DaySchedules");
                });

            modelBuilder.Entity("Insure.Models.CRM.Schedules.WeekSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("OfficeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId")
                        .IsUnique();

                    b.ToTable("WeekSchedules");
                });

            modelBuilder.Entity("Insure.Models.CRM.Schedules.DaySchedule", b =>
                {
                    b.HasOne("Insure.Models.CRM.Schedules.WeekSchedule", "WeekSchedule")
                        .WithMany("DaySchedules")
                        .HasForeignKey("WeekScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeekSchedule");
                });

            modelBuilder.Entity("Insure.Models.CRM.Schedules.WeekSchedule", b =>
                {
                    b.HasOne("Insure.Models.CRM.Office", "Office")
                        .WithOne("WeekSchedule")
                        .HasForeignKey("Insure.Models.CRM.Schedules.WeekSchedule", "OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("Insure.Models.CRM.Office", b =>
                {
                    b.Navigation("WeekSchedule");
                });

            modelBuilder.Entity("Insure.Models.CRM.Schedules.WeekSchedule", b =>
                {
                    b.Navigation("DaySchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
