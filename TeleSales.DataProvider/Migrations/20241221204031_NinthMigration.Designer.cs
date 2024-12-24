﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeleSales.DataProvider.Context;

#nullable disable

namespace TeleSales.DataProvider.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241221204031_NinthMigration")]
    partial class NinthMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TeleSales.DataProvider.Entities.CallCenters", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("Addition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Administration")
                        .HasColumnType("int");

                    b.Property<int>("ApplicationType")
                        .HasColumnType("int");

                    b.Property<string>("Conclusion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Department")
                        .HasColumnType("int");

                    b.Property<string>("DetailsContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ExcludedBy")
                        .HasColumnType("bigint");

                    b.Property<long?>("ForwardTo")
                        .HasColumnType("bigint");

                    b.Property<bool>("Forwarding")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.Property<string>("ShortContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Usersid")
                        .HasColumnType("bigint");

                    b.Property<long>("VOEN")
                        .HasColumnType("bigint");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("kanalId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("Usersid");

                    b.HasIndex("kanalId");

                    b.ToTable("CallCenters");
                });

            modelBuilder.Entity("TeleSales.DataProvider.Entities.Calls", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<int?>("Conclusion")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ExcludedBy")
                        .HasColumnType("bigint");

                    b.Property<long>("InvoiceNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("KanalId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastStatusUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LegalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Month10_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month11_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month12_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month1_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month1_2025")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month2_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month2_2025")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month3_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month3_2025")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month4_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month5_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month6_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month7_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month8_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Month9_2024")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("NextCall")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("PermissionEndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("PermissionStartDate")
                        .HasColumnType("date");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TotalDebt")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("VOEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Year2018")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Year2019")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Year2020")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Year2021")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Year2022")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Year2023")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("isDone")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("ExcludedBy");

                    b.HasIndex("KanalId");

                    b.ToTable("Calls");
                });

            modelBuilder.Entity("TeleSales.DataProvider.Entities.Kanals", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("Kanals");
                });

            modelBuilder.Entity("TeleSales.DataProvider.Entities.UserKanals", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("KanalId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("id")
                        .HasColumnType("bigint");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "KanalId");

                    b.HasIndex("KanalId");

                    b.ToTable("UserKanals");
                });

            modelBuilder.Entity("TeleSales.DataProvider.Entities.Users", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            id = 1L,
                            CreateAt = new DateTime(2024, 12, 22, 0, 40, 30, 996, DateTimeKind.Local).AddTicks(3449),
                            Email = "admin@adra.gov.az",
                            FullName = "Admin",
                            Password = "Admin123",
                            Role = 2,
                            isDeleted = false
                        });
                });

            modelBuilder.Entity("TeleSales.DataProvider.Entities.CallCenters", b =>
                {
                    b.HasOne("TeleSales.DataProvider.Entities.Users", "Users")
                        .WithMany()
                        .HasForeignKey("Usersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeleSales.DataProvider.Entities.Kanals", "Kanal")
                        .WithMany()
                        .HasForeignKey("kanalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kanal");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("TeleSales.DataProvider.Entities.Calls", b =>
                {
                    b.HasOne("TeleSales.DataProvider.Entities.Users", "User")
                        .WithMany("Calls")
                        .HasForeignKey("ExcludedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TeleSales.DataProvider.Entities.Kanals", "Kanal")
                        .WithMany("Calls")
                        .HasForeignKey("KanalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Kanal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TeleSales.DataProvider.Entities.UserKanals", b =>
                {
                    b.HasOne("TeleSales.DataProvider.Entities.Kanals", "Kanals")
                        .WithMany("UserKanal")
                        .HasForeignKey("KanalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeleSales.DataProvider.Entities.Users", "Users")
                        .WithMany("UserKanal")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kanals");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("TeleSales.DataProvider.Entities.Kanals", b =>
                {
                    b.Navigation("Calls");

                    b.Navigation("UserKanal");
                });

            modelBuilder.Entity("TeleSales.DataProvider.Entities.Users", b =>
                {
                    b.Navigation("Calls");

                    b.Navigation("UserKanal");
                });
#pragma warning restore 612, 618
        }
    }
}