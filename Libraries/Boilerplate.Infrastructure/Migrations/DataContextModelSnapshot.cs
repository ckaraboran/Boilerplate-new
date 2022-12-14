// <auto-generated />
using Boilerplate.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Boilerplate.Domain.Entities.Dummy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Dummies");
                });

            modelBuilder.Entity("Boilerplate.Domain.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IsDeleted = false,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 2L,
                            IsDeleted = false,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("Boilerplate.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IsDeleted = false,
                            Name = "Manager",
                            Password = "AQAAAAEAACcQAAAAEPAP+lL/oxuJsRb0+JNJqx5qJM7ZwJFO07Mozj19QlCpb5mSeDp/VfRbOTy03G2qaQ==",
                            Surname = "Manager",
                            Username = "Manager"
                        },
                        new
                        {
                            Id = 2L,
                            IsDeleted = false,
                            Name = "User",
                            Password = "AQAAAAEAACcQAAAAEEYBeXoqyOYv3D0Gnzdp2aa/p4XJEu2Rt9YAtezKBEcF6ECjPv2NoRamELuvdBSvaw==",
                            Surname = "User",
                            Username = "User"
                        });
                });

            modelBuilder.Entity("Boilerplate.Domain.Entities.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersRoles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IsDeleted = false,
                            RoleId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            IsDeleted = false,
                            RoleId = 2L,
                            UserId = 2L
                        });
                });

            modelBuilder.Entity("Boilerplate.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Boilerplate.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boilerplate.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
