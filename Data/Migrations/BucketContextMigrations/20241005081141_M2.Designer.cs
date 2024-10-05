﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using expense_transactions.Data;

#nullable disable

namespace expense_transactions.Data.Migrations.BucketContextMigrations
{
    [DbContext(typeof(BucketContext))]
    [Migration("20241005081141_M2")]
    partial class M2
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

                    b.ToTable("Buckets");
                });
#pragma warning restore 612, 618
        }
    }
}