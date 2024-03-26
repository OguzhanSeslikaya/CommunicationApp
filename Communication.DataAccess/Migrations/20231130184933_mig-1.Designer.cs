﻿// <auto-generated />
using System;
using Communication.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Communication.DataAccess.Migrations
{
    [DbContext(typeof(CommunicationAppDBContext))]
    [Migration("20231130184933_mig-1")]
    partial class mig1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Communication.Entity.Models.Company.Call", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("calledId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("callerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("groupId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mainCallFileId")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("calledId");

                    b.HasIndex("callerId");

                    b.HasIndex("groupId");

                    b.HasIndex("mainCallFileId");

                    b.ToTable("calls");
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.Group", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("creatorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasAlternateKey("name");

                    b.HasIndex("creatorId");

                    b.ToTable("groups");
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.GroupUser", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("groupId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("groupId");

                    b.HasIndex("userId");

                    b.ToTable("groupUser");
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.GroupUserMessage", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("groupId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("receiverId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("senderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("groupId");

                    b.HasIndex("receiverId");

                    b.HasIndex("senderId");

                    b.ToTable("groupUserMessages");
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.RequestToJoinGroup", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("groupId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("state")
                        .HasColumnType("integer");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("groupId");

                    b.HasIndex("userId");

                    b.ToTable("requestsToJoinGroup");
                });

            modelBuilder.Entity("Communication.Entity.Models.File.LocalStorage.CallFile", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("callId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("fileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isMixed")
                        .HasColumnType("boolean");

                    b.Property<string>("path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("storage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("callId");

                    b.ToTable("callFiles");
                });

            modelBuilder.Entity("Communication.Entity.Models.File.LocalStorage.PostFile", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("fileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("storage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("postFiles");
                });

            modelBuilder.Entity("Communication.Entity.Models.User.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Communication.Entity.Models.User.Identity.Endpoint", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("permission")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("endpoints");
                });

            modelBuilder.Entity("Communication.Entity.Models.User.UserPosts.UserPost", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("groupId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("postFileId")
                        .HasColumnType("text");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("groupId");

                    b.HasIndex("postFileId");

                    b.HasIndex("userId");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.Call", b =>
                {
                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", "called")
                        .WithMany()
                        .HasForeignKey("calledId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", "caller")
                        .WithMany()
                        .HasForeignKey("callerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Communication.Entity.Models.Company.Group", "group")
                        .WithMany()
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Communication.Entity.Models.File.LocalStorage.CallFile", "mainCallFile")
                        .WithMany()
                        .HasForeignKey("mainCallFileId");

                    b.Navigation("called");

                    b.Navigation("caller");

                    b.Navigation("group");

                    b.Navigation("mainCallFile");
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.Group", b =>
                {
                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", "creator")
                        .WithMany()
                        .HasForeignKey("creatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("creator");
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.GroupUser", b =>
                {
                    b.HasOne("Communication.Entity.Models.Company.Group", "group")
                        .WithMany()
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("group");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.GroupUserMessage", b =>
                {
                    b.HasOne("Communication.Entity.Models.Company.Group", "group")
                        .WithMany()
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", "receiver")
                        .WithMany()
                        .HasForeignKey("receiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", "sender")
                        .WithMany()
                        .HasForeignKey("senderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("group");

                    b.Navigation("receiver");

                    b.Navigation("sender");
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.RequestToJoinGroup", b =>
                {
                    b.HasOne("Communication.Entity.Models.Company.Group", "group")
                        .WithMany()
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("group");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Communication.Entity.Models.File.LocalStorage.CallFile", b =>
                {
                    b.HasOne("Communication.Entity.Models.Company.Call", "call")
                        .WithMany("callFiles")
                        .HasForeignKey("callId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("call");
                });

            modelBuilder.Entity("Communication.Entity.Models.User.UserPosts.UserPost", b =>
                {
                    b.HasOne("Communication.Entity.Models.Company.Group", "group")
                        .WithMany()
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Communication.Entity.Models.File.LocalStorage.PostFile", "postFile")
                        .WithMany()
                        .HasForeignKey("postFileId");

                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", "user")
                        .WithMany("posts")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("group");

                    b.Navigation("postFile");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Communication.Entity.Models.User.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Communication.Entity.Models.Company.Call", b =>
                {
                    b.Navigation("callFiles");
                });

            modelBuilder.Entity("Communication.Entity.Models.User.Identity.AppUser", b =>
                {
                    b.Navigation("posts");
                });
#pragma warning restore 612, 618
        }
    }
}
