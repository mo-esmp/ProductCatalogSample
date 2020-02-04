﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Catalog.Api.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    partial class ProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Catalog.Api.Domain.ProductCatalog.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Catalog.Api.Domain.ProductCatalog.Product", b =>
                {
                    b.OwnsOne("Catalog.Api.Domain.ProductCatalog.ProductCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnName("Code")
                                .HasColumnType("varchar(64)")
                                .HasMaxLength(64)
                                .IsUnicode(false);

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Catalog.Api.Domain.ProductCatalog.ProductName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnName("Name")
                                .HasColumnType("varchar(64)")
                                .HasMaxLength(64)
                                .IsUnicode(false);

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Catalog.Api.Domain.ProductCatalog.ProductPrice", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");

                            b1.OwnsOne("Catalog.Api.Domain.Shared.Currency", "Currency", b2 =>
                                {
                                    b2.Property<Guid>("ProductPriceProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("CurrencyCode")
                                        .HasColumnType("int");

                                    b2.Property<int>("DecimalPlaces")
                                        .HasColumnType("int");

                                    b2.Property<bool>("InUse")
                                        .HasColumnType("bit");

                                    b2.HasKey("ProductPriceProductId");

                                    b2.ToTable("Products");

                                    b2.WithOwner()
                                        .HasForeignKey("ProductPriceProductId");
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}