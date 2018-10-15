﻿// <auto-generated />
using System;
using FNZ.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FNZ.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180825113836_AddedIdentity")]
    partial class AddedIdentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FNZ.Share.Models.Animal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedToSystemAt");

                    b.Property<DateTime>("AdoptionDate");

                    b.Property<double>("Age");

                    b.Property<string>("Breed");

                    b.Property<DateTime>("FoundAt");

                    b.Property<int>("MaxWeight");

                    b.Property<string>("Name");

                    b.Property<long?>("PostId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("FNZ.Share.Models.Application", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress");

                    b.Property<long?>("AnimalId");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("FNZ.Share.Models.Moderator", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Moderators");
                });

            modelBuilder.Entity("FNZ.Share.Models.MoneyCollection", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<long>("MoneyTarget");

                    b.Property<long?>("PostId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("MoneyCollections");
                });

            modelBuilder.Entity("FNZ.Share.Models.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AddedAt");

                    b.Property<string>("Author");

                    b.Property<int>("Category");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("EditedAt");

                    b.Property<string>("EditedById");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("EditedById");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("FNZ.Share.Models.Request", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AcceptanceDate");

                    b.Property<int>("Action");

                    b.Property<long?>("EditedPostId");

                    b.Property<string>("ModeratorId");

                    b.Property<long?>("PostId");

                    b.Property<DateTime?>("RefusalDate");

                    b.Property<DateTime>("SentAt");

                    b.HasKey("Id");

                    b.HasIndex("EditedPostId");

                    b.HasIndex("ModeratorId");

                    b.HasIndex("PostId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("FNZ.Share.Models.Tab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<string>("LastEditedById");

                    b.Property<string>("ModeratorId");

                    b.Property<string>("PhotoPath");

                    b.Property<int>("TabCategory");

                    b.HasKey("Id");

                    b.HasIndex("LastEditedById");

                    b.HasIndex("ModeratorId");

                    b.ToTable("Tabs");
                });

            modelBuilder.Entity("FNZ.Share.Models.Animal", b =>
                {
                    b.HasOne("FNZ.Share.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("FNZ.Share.Models.Application", b =>
                {
                    b.HasOne("FNZ.Share.Models.Animal", "Animal")
                        .WithMany("Applications")
                        .HasForeignKey("AnimalId");
                });

            modelBuilder.Entity("FNZ.Share.Models.MoneyCollection", b =>
                {
                    b.HasOne("FNZ.Share.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("FNZ.Share.Models.Post", b =>
                {
                    b.HasOne("FNZ.Share.Models.Moderator", "EditedBy")
                        .WithMany()
                        .HasForeignKey("EditedById");
                });

            modelBuilder.Entity("FNZ.Share.Models.Request", b =>
                {
                    b.HasOne("FNZ.Share.Models.Post", "EditedPost")
                        .WithMany()
                        .HasForeignKey("EditedPostId");

                    b.HasOne("FNZ.Share.Models.Moderator", "Moderator")
                        .WithMany("Requests")
                        .HasForeignKey("ModeratorId");

                    b.HasOne("FNZ.Share.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("FNZ.Share.Models.Tab", b =>
                {
                    b.HasOne("FNZ.Share.Models.Moderator", "LastEditedBy")
                        .WithMany()
                        .HasForeignKey("LastEditedById");

                    b.HasOne("FNZ.Share.Models.Moderator", "Moderator")
                        .WithMany("Tabs")
                        .HasForeignKey("ModeratorId");
                });
#pragma warning restore 612, 618
        }
    }
}
