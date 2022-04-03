﻿// <auto-generated />
using System;
using EF_project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EF_project.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("ChatEntityUserEntity", b =>
                {
                    b.Property<string>("ParticipantsNickName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserChatsName")
                        .HasColumnType("TEXT");

                    b.HasKey("ParticipantsNickName", "UserChatsName");

                    b.HasIndex("UserChatsName");

                    b.ToTable("ChatEntityUserEntity");
                });

            modelBuilder.Entity("EF_project.Entities.ChatEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("EF_project.Entities.MessageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorNickName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChatName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Contents")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateSent")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorNickName");

                    b.HasIndex("ChatName");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("EF_project.Entities.UserEntity", b =>
                {
                    b.Property<string>("NickName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.HasKey("NickName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatEntityUserEntity", b =>
                {
                    b.HasOne("EF_project.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsNickName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_project.Entities.ChatEntity", null)
                        .WithMany()
                        .HasForeignKey("UserChatsName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EF_project.Entities.MessageEntity", b =>
                {
                    b.HasOne("EF_project.Entities.UserEntity", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorNickName");

                    b.HasOne("EF_project.Entities.ChatEntity", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatName");

                    b.Navigation("Author");

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("EF_project.Entities.ChatEntity", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
