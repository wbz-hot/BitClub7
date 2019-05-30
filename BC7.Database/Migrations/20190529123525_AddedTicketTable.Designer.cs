﻿// <auto-generated />
using System;
using BC7.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BC7.Database.Migrations
{
    [DbContext(typeof(BitClub7Context))]
    [Migration("20190529123525_AddedTicketTable")]
    partial class AddedTicketTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BC7.Domain.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<Guid>("CreatorId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("BC7.Domain.MatrixPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("DepthLevel");

                    b.Property<int>("Left");

                    b.Property<int>("MatrixLevel");

                    b.Property<Guid?>("ParentId");

                    b.Property<int>("Right");

                    b.Property<Guid?>("UserMultiAccountId");

                    b.HasKey("Id");

                    b.HasIndex("UserMultiAccountId");

                    b.ToTable("MatrixPositions");
                });

            modelBuilder.Entity("BC7.Domain.PaymentHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AmountToPay")
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<Guid>("OrderId");

                    b.Property<decimal?>("PaidAmount")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("PaymentFor")
                        .IsRequired();

                    b.Property<Guid>("PaymentId");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PaymentHistories");
                });

            modelBuilder.Entity("BC7.Domain.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FullTicketNumber");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("BC7.Domain.UserAccountData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BtcWalletAddress")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Hash")
                        .IsRequired();

                    b.Property<bool>("IsMembershipFeePaid");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Role")
                        .IsRequired();

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("UserAccountsData");
                });

            modelBuilder.Entity("BC7.Domain.UserMultiAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<bool>("IsMainAccount");

                    b.Property<string>("MultiAccountName");

                    b.Property<string>("RefLink");

                    b.Property<Guid?>("SponsorId");

                    b.Property<Guid>("UserAccountDataId");

                    b.HasKey("Id");

                    b.HasIndex("SponsorId");

                    b.HasIndex("UserAccountDataId");

                    b.ToTable("UserMultiAccounts");
                });

            modelBuilder.Entity("Z.EntityFramework.Plus.AuditEntry", b =>
                {
                    b.Property<int>("AuditEntryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("EntitySetName")
                        .HasMaxLength(255);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(255);

                    b.Property<int>("State");

                    b.Property<string>("StateName")
                        .HasMaxLength(255);

                    b.HasKey("AuditEntryID");

                    b.ToTable("AuditEntries");
                });

            modelBuilder.Entity("Z.EntityFramework.Plus.AuditEntryProperty", b =>
                {
                    b.Property<int>("AuditEntryPropertyID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditEntryID");

                    b.Property<string>("NewValueFormatted")
                        .HasColumnName("NewValue");

                    b.Property<string>("OldValueFormatted")
                        .HasColumnName("OldValue");

                    b.Property<string>("PropertyName")
                        .HasMaxLength(255);

                    b.Property<string>("RelationName")
                        .HasMaxLength(255);

                    b.HasKey("AuditEntryPropertyID");

                    b.HasIndex("AuditEntryID");

                    b.ToTable("AuditEntryProperties");
                });

            modelBuilder.Entity("BC7.Domain.Article", b =>
                {
                    b.HasOne("BC7.Domain.UserAccountData", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BC7.Domain.MatrixPosition", b =>
                {
                    b.HasOne("BC7.Domain.UserMultiAccount")
                        .WithMany("MatrixPositions")
                        .HasForeignKey("UserMultiAccountId");
                });

            modelBuilder.Entity("BC7.Domain.UserMultiAccount", b =>
                {
                    b.HasOne("BC7.Domain.UserMultiAccount", "Sponsor")
                        .WithMany()
                        .HasForeignKey("SponsorId");

                    b.HasOne("BC7.Domain.UserAccountData", "UserAccountData")
                        .WithMany("UserMultiAccounts")
                        .HasForeignKey("UserAccountDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Z.EntityFramework.Plus.AuditEntryProperty", b =>
                {
                    b.HasOne("Z.EntityFramework.Plus.AuditEntry", "Parent")
                        .WithMany("Properties")
                        .HasForeignKey("AuditEntryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
