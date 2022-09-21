﻿// <auto-generated />
using System;
using Chao_Blog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chao_Blog.Migrations
{
    [DbContext(typeof(RoutineDbContext))]
    [Migration("20220901162420_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Chao_Blog.Entity.Resume", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Educations")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Experiences")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(5000);

                    b.Property<string>("Introduction")
                        .IsRequired()
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Resumes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"),
                            Educations = "testEdutions1",
                            Experiences = "testexperience1",
                            Introduction = "testintroduction1",
                            Skills = "testskills1",
                            UserId = new Guid("bbdee09c-089b-4d30-bece-44df5923716c")
                        },
                        new
                        {
                            Id = new Guid("4b501cb3-d168-4cc0-b375-48fb33f333a5"),
                            Educations = "testEdutions2",
                            Experiences = "testexperience2",
                            Introduction = "testintroduction2",
                            Skills = "testskills2",
                            UserId = new Guid("bbdee09c-089b-4d30-bece-44df5923716c")
                        });
                });

            modelBuilder.Entity("Chao_Blog.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserLevel")
                        .HasColumnType("int")
                        .HasMaxLength(1);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bbdee09c-089b-4d30-bece-44df5923716c"),
                            FirstName = "Chao",
                            LastName = "Wang",
                            Password = "wc1234",
                            UserLevel = 2
                        });
                });

            modelBuilder.Entity("Chao_Blog.Entity.Resume", b =>
                {
                    b.HasOne("Chao_Blog.Entity.User", "User")
                        .WithMany("Resumes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
