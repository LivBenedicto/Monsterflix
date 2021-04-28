﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Monsterflix.Api.Data;

namespace Monsterflix.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Monsterflix.Api.Models.Account", b =>
                {
                    b.Property<int>("IdAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Birthday")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("IdAccount");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.Movie", b =>
                {
                    b.Property<int>("IdMovie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdMovieService")
                        .HasColumnType("int");

                    b.HasKey("IdMovie");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.MovieGenre", b =>
                {
                    b.Property<int>("IdMovieGenre")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdGenreService")
                        .HasColumnType("int");

                    b.Property<int>("IdMovie")
                        .HasColumnType("int");

                    b.HasKey("IdMovieGenre");

                    b.HasIndex("IdMovie");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.Profile", b =>
                {
                    b.Property<int>("IdProfile")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdAccount")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("IdProfile");

                    b.HasIndex("IdAccount");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.ProfileMovie", b =>
                {
                    b.Property<int>("IdProfileMovie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdMovie")
                        .HasColumnType("int");

                    b.Property<int>("IdProfile")
                        .HasColumnType("int");

                    b.Property<int>("StatusWatch")
                        .HasColumnType("int");

                    b.HasKey("IdProfileMovie");

                    b.HasIndex("IdMovie");

                    b.HasIndex("IdProfile");

                    b.ToTable("ProfileMovies");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.MovieGenre", b =>
                {
                    b.HasOne("Monsterflix.Api.Models.Movie", "Movie")
                        .WithMany("Genre")
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.Profile", b =>
                {
                    b.HasOne("Monsterflix.Api.Models.Account", "Account")
                        .WithMany("Profile")
                        .HasForeignKey("IdAccount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.ProfileMovie", b =>
                {
                    b.HasOne("Monsterflix.Api.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Monsterflix.Api.Models.Profile", "Profile")
                        .WithMany("Movie")
                        .HasForeignKey("IdProfile")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.Account", b =>
                {
                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.Movie", b =>
                {
                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Monsterflix.Api.Models.Profile", b =>
                {
                    b.Navigation("Movie");
                });
#pragma warning restore 612, 618
        }
    }
}
