﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using invoiceF.Models;

namespace invoiceF.Migrations
{
    [DbContext(typeof(CustomerContext))]
    partial class CustomerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("invoiceF.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BusinessName")
                        .HasMaxLength(50);

                    b.Property<string>("CustomerEmail");

                    b.Property<string>("CustomerPhone")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("invoiceF.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerID");

                    b.Property<DateTime>("InvoiceCreatedAt");

                    b.Property<decimal>("ServiceCost")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ServiceDescription");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("InvoiceID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("invoiceF.Models.Invoice", b =>
                {
                    b.HasOne("invoiceF.Models.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
