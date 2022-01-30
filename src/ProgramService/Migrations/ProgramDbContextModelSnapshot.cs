﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProgramService.Database;

#nullable disable

namespace ProgramService.Migrations
{
    [DbContext(typeof(ProgramDbContext))]
    partial class ProgramDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("ProgramService.Entity.ProgramEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Homepage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Program", (string)null);
                });

            modelBuilder.Entity("ProgramService.Entity.TestEntity", b =>
                {
                    b.Property<string>("TestPk1")
                        .HasColumnType("TEXT");

                    b.Property<string>("TestPk2")
                        .HasColumnType("TEXT");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TestPk1", "TestPk2");

                    b.ToTable("Test", (string)null);
                });

            modelBuilder.Entity("ProgramService.Entity.VersionEntity", b =>
                {
                    b.Property<string>("VersionTag")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProgramName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("VersionTag");

                    b.HasIndex("ProgramName");

                    b.ToTable("Version", (string)null);
                });

            modelBuilder.Entity("ProgramService.Entity.VersionEntity", b =>
                {
                    b.HasOne("ProgramService.Entity.ProgramEntity", "Program")
                        .WithMany("Version")
                        .HasForeignKey("ProgramName");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("ProgramService.Entity.ProgramEntity", b =>
                {
                    b.Navigation("Version");
                });
#pragma warning restore 612, 618
        }
    }
}