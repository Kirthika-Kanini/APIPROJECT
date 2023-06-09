﻿// <auto-generated />
using System;
using APIPROJECT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIPROJECT.Migrations
{
    [DbContext(typeof(FacultyCourseContext))]
    [Migration("20230509100838_coufac")]
    partial class coufac
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIPROJECT.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseDuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CourseEnrollment")
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("APIPROJECT.Models.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacultyId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("FacultyDept")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacultyDesignation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacultySalary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facultymail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FacultyId");

                    b.HasIndex("CourseId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("APIPROJECT.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("APIPROJECT.Models.Faculty", b =>
                {
                    b.HasOne("APIPROJECT.Models.Course", "Course")
                        .WithMany("Faculties")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("APIPROJECT.Models.Course", b =>
                {
                    b.Navigation("Faculties");
                });
#pragma warning restore 612, 618
        }
    }
}
