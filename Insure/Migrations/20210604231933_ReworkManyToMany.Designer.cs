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
    [Migration("20210604231933_ReworkManyToMany")]
    partial class ReworkManyToMany
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

            modelBuilder.Entity("Insure.Models.CRM.New", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FullText")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ShortText")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Insure.Models.CRM.News.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FatherName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Insure.Models.CRM.News.AuthorNew", b =>
                {
                    b.Property<int>("NewId")
                        .HasColumnType("integer");

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.HasKey("NewId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("AuthorNews");
                });

            modelBuilder.Entity("Insure.Models.CRM.News.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Insure.Models.CRM.News.TagNew", b =>
                {
                    b.Property<int>("NewId")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("NewId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("TagNews");
                });

            modelBuilder.Entity("Insure.Models.CRM.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

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

                    b.Property<int>("FromHour")
                        .HasColumnType("integer");

                    b.Property<int>("FromMinute")
                        .HasColumnType("integer");

                    b.Property<int>("OfficeId")
                        .HasColumnType("integer");

                    b.Property<int>("ToHour")
                        .HasColumnType("integer");

                    b.Property<int>("ToMinute")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("DaySchedules");
                });

            modelBuilder.Entity("Insure.Models.CRM.News.AuthorNew", b =>
                {
                    b.HasOne("Insure.Models.CRM.News.Author", "Author")
                        .WithMany("AuthorNews")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Insure.Models.CRM.New", "New")
                        .WithMany("AuthorNews")
                        .HasForeignKey("NewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("New");
                });

            modelBuilder.Entity("Insure.Models.CRM.News.TagNew", b =>
                {
                    b.HasOne("Insure.Models.CRM.New", "New")
                        .WithMany("TagNews")
                        .HasForeignKey("NewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Insure.Models.CRM.News.Tag", "Tag")
                        .WithMany("TagNews")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("New");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Insure.Models.CRM.Schedules.DaySchedule", b =>
                {
                    b.HasOne("Insure.Models.CRM.Office", "Office")
                        .WithMany("DaySchedules")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("Insure.Models.CRM.New", b =>
                {
                    b.Navigation("AuthorNews");

                    b.Navigation("TagNews");
                });

            modelBuilder.Entity("Insure.Models.CRM.News.Author", b =>
                {
                    b.Navigation("AuthorNews");
                });

            modelBuilder.Entity("Insure.Models.CRM.News.Tag", b =>
                {
                    b.Navigation("TagNews");
                });

            modelBuilder.Entity("Insure.Models.CRM.Office", b =>
                {
                    b.Navigation("DaySchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
