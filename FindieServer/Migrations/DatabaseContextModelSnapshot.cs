﻿// <auto-generated />

using System;
using FindieServer.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FindieServer.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FindieWeb.Models.AdminsModel.Notepads", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NotepadContent")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.Property<string>("Username")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("NotepadTable");
                });

            modelBuilder.Entity("FindieWeb.Models.DbModels.EventComments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<int>("EventId");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventComments");
                });

            modelBuilder.Entity("FindieWeb.Models.DbModels.EventParticipants", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<bool?>("IsUserAcceptedEventRequest");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventParticipants");
                });

            modelBuilder.Entity("FindieWeb.Models.DbModels.Events", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfCreate");

                    b.Property<DateTime?>("EventDate");

                    b.Property<string>("EventDescription");

                    b.Property<string>("EventName");

                    b.Property<int>("HostId");

                    b.Property<string>("ImageUri");

                    b.Property<bool>("IsEventPrivate");

                    b.Property<decimal?>("Latitude");

                    b.Property<decimal?>("Longitude");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("FindieWeb.Models.DbModels.Friends", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FirstUser");

                    b.Property<bool>("IsRequestAccepted");

                    b.Property<int?>("SecondUser");

                    b.HasKey("Id");

                    b.ToTable("FriendTable");
                });

            modelBuilder.Entity("FindieWeb.Models.DbModels.Messages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MessageFrom");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("MessageTo");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("FindieWeb.Models.IdentityModels.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("FindieWeb.Models.IdentityModels.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AccountDescription")
                        .HasMaxLength(250);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool?>("IsBanned");

                    b.Property<bool?>("IsUserOnline");

                    b.Property<decimal?>("Latitude");

                    b.Property<string>("Localization")
                        .HasMaxLength(20);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<decimal?>("Longitude");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname")
                        .HasMaxLength(50);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FindieWeb.Models.DbModels.EventComments", b =>
                {
                    b.HasOne("FindieWeb.Models.DbModels.Events", "Event")
                        .WithMany("EventComments")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FindieWeb.Models.DbModels.EventParticipants", b =>
                {
                    b.HasOne("FindieWeb.Models.DbModels.Events", "Event")
                        .WithMany("EventParticipants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("FindieWeb.Models.IdentityModels.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("FindieWeb.Models.IdentityModels.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("FindieWeb.Models.IdentityModels.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("FindieWeb.Models.IdentityModels.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FindieWeb.Models.IdentityModels.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("FindieWeb.Models.IdentityModels.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
