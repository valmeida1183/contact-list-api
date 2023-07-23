﻿// <auto-generated />
using System;
using ContactListApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace contact_list_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContactListApi.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<int>("ContactTypeId")
                        .HasColumnType("INT")
                        .HasColumnName("ContactTypeId");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PersonId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("ContactTypeId");

                    b.HasIndex("PersonId");

                    b.ToTable("Contact", (string)null);
                });

            modelBuilder.Entity("ContactListApi.Models.ContactType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("ContactType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Telefone"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Email"
                        },
                        new
                        {
                            Id = 3,
                            Name = "WhatsApp"
                        });
                });

            modelBuilder.Entity("ContactListApi.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("ContactListApi.Models.Contact", b =>
                {
                    b.HasOne("ContactListApi.Models.ContactType", "Type")
                        .WithMany("Contacts")
                        .HasForeignKey("ContactTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Contact_Type");

                    b.HasOne("ContactListApi.Models.Person", "Person")
                        .WithMany("Contacts")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Contact_Person");

                    b.Navigation("Person");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ContactListApi.Models.ContactType", b =>
                {
                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("ContactListApi.Models.Person", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
