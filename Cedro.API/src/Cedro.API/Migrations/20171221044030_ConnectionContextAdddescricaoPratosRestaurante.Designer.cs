using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Cedro.API.Entities;

namespace Cedro.API.Migrations
{
    [DbContext(typeof(ConnectContext))]
    [Migration("20171221044030_ConnectionContextAdddescricaoPratosRestaurante")]
    partial class ConnectionContextAdddescricaoPratosRestaurante
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cedro.API.Entities.PratosRestaurantes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .HasMaxLength(200);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("RestaurantesId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantesId");

                    b.ToTable("PratosRestaurantes");
                });

            modelBuilder.Entity("Cedro.API.Entities.Restaurantes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Restaurantes");
                });

            modelBuilder.Entity("Cedro.API.Entities.PratosRestaurantes", b =>
                {
                    b.HasOne("Cedro.API.Entities.Restaurantes", "Restaurantes")
                        .WithMany("PatosRestaurantes")
                        .HasForeignKey("RestaurantesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
