﻿// <auto-generated />
using BitcoinShow.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BitcoinShow.Web.Migrations
{
    [DbContext(typeof(BitcoinShowDBContext))]
    [Migration("20171011175204_Add_Award")]
    partial class Add_Award
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BitcoinShow.Web.Models.Award", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Fail");

                    b.Property<byte>("Level");

                    b.Property<decimal>("Quit");

                    b.Property<decimal>("Success");

                    b.HasKey("Id");

                    b.ToTable("Award");
                });

            modelBuilder.Entity("BitcoinShow.Web.Models.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("QuestionId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Option");
                });

            modelBuilder.Entity("BitcoinShow.Web.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<byte>("Level");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("BitcoinShow.Web.Models.Option", b =>
                {
                    b.HasOne("BitcoinShow.Web.Models.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BitcoinShow.Web.Models.Question", b =>
                {
                    b.HasOne("BitcoinShow.Web.Models.Option", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
