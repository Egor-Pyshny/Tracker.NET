﻿// <auto-generated />
using System;
using HelpApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HelpApp.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240503182821_Add_m2m")]
    partial class Add_m2m
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DiagnosisPatients", b =>
                {
                    b.Property<int>("diagnosesid")
                        .HasColumnType("int");

                    b.Property<int>("patientsid")
                        .HasColumnType("int");

                    b.HasKey("diagnosesid", "patientsid");

                    b.HasIndex("patientsid");

                    b.ToTable("DiagnosisPatients");
                });

            modelBuilder.Entity("DoctorsSurgeries_graphic", b =>
                {
                    b.Property<int>("Doctorsid")
                        .HasColumnType("int");

                    b.Property<int>("surgeries_Graphicsid")
                        .HasColumnType("int");

                    b.HasKey("Doctorsid", "surgeries_Graphicsid");

                    b.HasIndex("surgeries_Graphicsid");

                    b.ToTable("DoctorsSurgeries_graphic");
                });

            modelBuilder.Entity("DoctorsSurgeries_history", b =>
                {
                    b.Property<int>("doctorsid")
                        .HasColumnType("int");

                    b.Property<int>("surgeries_Historyid")
                        .HasColumnType("int");

                    b.HasKey("doctorsid", "surgeries_Historyid");

                    b.HasIndex("surgeries_Historyid");

                    b.ToTable("DoctorsSurgeries_history");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Address", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("apartment")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("building")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<int>("cityid")
                        .HasColumnType("int");

                    b.Property<int>("countryid")
                        .HasColumnType("int");

                    b.Property<int>("streetid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("cityid");

                    b.HasIndex("countryid");

                    b.HasIndex("streetid");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Appointment_records", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("doctorid")
                        .HasColumnType("int");

                    b.Property<int>("hospitalid")
                        .HasColumnType("int");

                    b.Property<int>("patientid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("doctorid");

                    b.HasIndex("hospitalid");

                    b.HasIndex("patientid");

                    b.ToTable("Appointment_records");
                });

            modelBuilder.Entity("HelpApp.Data.Models.City", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("id");

                    b.ToTable("City");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Country", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Diagnosis", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("id");

                    b.ToTable("Diagnosis");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Doctors", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("hospitalid")
                        .HasColumnType("int");

                    b.Property<string>("second_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("third_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("hospitalid");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Doctors_recommendations", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("doctorid")
                        .HasColumnType("int");

                    b.Property<int>("patientid")
                        .HasColumnType("int");

                    b.Property<string>("recommendations")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.HasKey("id");

                    b.HasIndex("doctorid");

                    b.HasIndex("patientid");

                    b.ToTable("Doctors_recommendations");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Game_history", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("file_path")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("levelid")
                        .HasColumnType("int");

                    b.Property<int>("patientid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("levelid");

                    b.HasIndex("patientid");

                    b.ToTable("Game_history");
                });

            modelBuilder.Entity("HelpApp.Data.Models.General_blood_analysis", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("file_path")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("patientid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("patientid");

                    b.ToTable("General_blood_analysis");
                });

            modelBuilder.Entity("HelpApp.Data.Models.General_urine_analysis", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("file_path")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("patientid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("patientid");

                    b.ToTable("General_urine_analysis");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Hospitals", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("addressid")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("id");

                    b.HasIndex("addressid");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Levels", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("file_path")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("level_name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Passport", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("addressid")
                        .HasColumnType("int");

                    b.Property<DateTime>("birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("second_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("third_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("addressid");

                    b.ToTable("Passport");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Patients", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("doctorid")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<int>("passportid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("doctorid");

                    b.HasIndex("passportid");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Street", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("id");

                    b.ToTable("Street");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Surgeries_graphic", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("hospitalid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("hospitalid");

                    b.ToTable("Surgeries_graphic");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Surgeries_history", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("hospitalid")
                        .HasColumnType("int");

                    b.Property<int>("patientid")
                        .HasColumnType("int");

                    b.Property<int>("surgery_Reportid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("hospitalid");

                    b.HasIndex("patientid");

                    b.HasIndex("surgery_Reportid");

                    b.ToTable("Surgeries_history");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Surgery_reports", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("file_path")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("id");

                    b.ToTable("Surgery_reports");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Xray_results", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("file_path")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("patientid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("patientid");

                    b.ToTable("Xray_results");
                });

            modelBuilder.Entity("DiagnosisPatients", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Diagnosis", null)
                        .WithMany()
                        .HasForeignKey("diagnosesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Patients", null)
                        .WithMany()
                        .HasForeignKey("patientsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoctorsSurgeries_graphic", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Doctors", null)
                        .WithMany()
                        .HasForeignKey("Doctorsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Surgeries_graphic", null)
                        .WithMany()
                        .HasForeignKey("surgeries_Graphicsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoctorsSurgeries_history", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Doctors", null)
                        .WithMany()
                        .HasForeignKey("doctorsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Surgeries_history", null)
                        .WithMany()
                        .HasForeignKey("surgeries_Historyid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HelpApp.Data.Models.Address", b =>
                {
                    b.HasOne("HelpApp.Data.Models.City", "city")
                        .WithMany()
                        .HasForeignKey("cityid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Country", "country")
                        .WithMany()
                        .HasForeignKey("countryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Street", "street")
                        .WithMany()
                        .HasForeignKey("streetid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");

                    b.Navigation("country");

                    b.Navigation("street");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Appointment_records", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Doctors", "doctor")
                        .WithMany()
                        .HasForeignKey("doctorid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Hospitals", "hospital")
                        .WithMany()
                        .HasForeignKey("hospitalid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Patients", "patient")
                        .WithMany()
                        .HasForeignKey("patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doctor");

                    b.Navigation("hospital");

                    b.Navigation("patient");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Doctors", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Hospitals", "hospital")
                        .WithMany()
                        .HasForeignKey("hospitalid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hospital");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Doctors_recommendations", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Doctors", "doctor")
                        .WithMany()
                        .HasForeignKey("doctorid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Patients", "patient")
                        .WithMany()
                        .HasForeignKey("patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doctor");

                    b.Navigation("patient");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Game_history", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Levels", "level")
                        .WithMany()
                        .HasForeignKey("levelid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Patients", "patient")
                        .WithMany()
                        .HasForeignKey("patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("level");

                    b.Navigation("patient");
                });

            modelBuilder.Entity("HelpApp.Data.Models.General_blood_analysis", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Patients", "patient")
                        .WithMany()
                        .HasForeignKey("patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("patient");
                });

            modelBuilder.Entity("HelpApp.Data.Models.General_urine_analysis", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Patients", "patient")
                        .WithMany()
                        .HasForeignKey("patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("patient");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Hospitals", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Address", "address")
                        .WithMany()
                        .HasForeignKey("addressid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("address");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Passport", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Address", "address")
                        .WithMany()
                        .HasForeignKey("addressid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("address");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Patients", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Doctors", "doctor")
                        .WithMany()
                        .HasForeignKey("doctorid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Passport", "passport")
                        .WithMany()
                        .HasForeignKey("passportid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doctor");

                    b.Navigation("passport");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Surgeries_graphic", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Hospitals", "hospital")
                        .WithMany()
                        .HasForeignKey("hospitalid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hospital");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Surgeries_history", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Hospitals", "hospital")
                        .WithMany()
                        .HasForeignKey("hospitalid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Patients", "patient")
                        .WithMany()
                        .HasForeignKey("patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelpApp.Data.Models.Surgery_reports", "surgery_Report")
                        .WithMany()
                        .HasForeignKey("surgery_Reportid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hospital");

                    b.Navigation("patient");

                    b.Navigation("surgery_Report");
                });

            modelBuilder.Entity("HelpApp.Data.Models.Xray_results", b =>
                {
                    b.HasOne("HelpApp.Data.Models.Patients", "patient")
                        .WithMany()
                        .HasForeignKey("patientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("patient");
                });
#pragma warning restore 612, 618
        }
    }
}