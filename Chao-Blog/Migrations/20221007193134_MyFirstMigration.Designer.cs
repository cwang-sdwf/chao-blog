﻿// <auto-generated />
using System;
using Chao_Blog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chao_Blog.Migrations
{
    [DbContext(typeof(RoutineDbContext))]
    [Migration("20221007193134_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Chao_Blog.Entity.Resume", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Educations")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Experiences")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<string>("Introduction")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

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
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserLevel")
                        .HasMaxLength(1)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bbdee09c-089b-4d30-bece-44df5923716c"),
                            FirstName = "Chao",
                            LastName = "Wang",
                            Password = "wc1234",
                            Salt = "",
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

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chao_Blog.Entity.User", b =>
                {
                    b.Navigation("Resumes");
                });
#pragma warning restore 612, 618
        }
    }
}
