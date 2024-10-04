﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using expense_transactions.Data;

#nullable disable

namespace expense_transactions.Data.Migrations.BucketContextMigrations
{
    [DbContext(typeof(BucketContext))]
    [Migration("20241004053749_ConsolidateSchema")]
    partial class ConsolidateSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("expense_transactions.Models.Bucket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("Company")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Buckets", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Entertainment",
                            Company = "ST JAMES RESTAURAT"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Communication",
                            Company = "ROGERS MOBILE"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Groceries",
                            Company = "SAFEWAY"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Donations",
                            Company = "RED CROSS"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Entertainment",
                            Company = "PUR & SIMPLE RESTAUR"
                        },
                        new
                        {
                            Id = 6,
                            Category = "Groceries",
                            Company = "REAL CDN SUPERS"
                        },
                        new
                        {
                            Id = 7,
                            Category = "Car Insurance",
                            Company = "ICBC"
                        },
                        new
                        {
                            Id = 8,
                            Category = "Gas Heating",
                            Company = "FORTISBC"
                        });
                });

            modelBuilder.Entity("expense_transactions.Models.TransactionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int?>("BucketId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BucketId");

                    b.ToTable("TransactionModel");
                });

            modelBuilder.Entity("expense_transactions.Models.TransactionModel", b =>
                {
                    b.HasOne("expense_transactions.Models.Bucket", "Bucket")
                        .WithMany("Transactions")
                        .HasForeignKey("BucketId");

                    b.Navigation("Bucket");
                });

            modelBuilder.Entity("expense_transactions.Models.Bucket", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
