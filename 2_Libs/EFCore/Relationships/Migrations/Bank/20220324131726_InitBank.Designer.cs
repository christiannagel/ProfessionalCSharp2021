﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Relationships.Migrations.Bank
{
    [DbContext(typeof(BankContext))]
    [Migration("20220324131726_InitBank")]
    partial class InitBank
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("bank")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("Money");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentId");

                    b.ToTable("Payments", "bank");

                    b.HasDiscriminator<string>("Type").HasValue("Payment");
                });

            modelBuilder.Entity("CashPayment", b =>
                {
                    b.HasBaseType("Payment");

                    b.HasDiscriminator().HasValue("cash");
                });

            modelBuilder.Entity("CreditcardPayment", b =>
                {
                    b.HasBaseType("Payment");

                    b.Property<string>("CreditcardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("creditcard");
                });
#pragma warning restore 612, 618
        }
    }
}