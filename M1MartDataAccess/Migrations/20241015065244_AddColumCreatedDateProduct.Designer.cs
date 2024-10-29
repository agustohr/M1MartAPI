﻿// <auto-generated />
using System;
using M1MartDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace M1MartDataAccess.Migrations
{
    [DbContext(typeof(M1MartV2Context))]
    [Migration("20241015065244_AddColumCreatedDateProduct")]
    partial class AddColumCreatedDateProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("M1MartDataAccess.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BuyerUsername")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("buyer_username");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("BuyerUsername");

                    b.HasIndex("ProductId");

                    b.ToTable("cart", (string)null);
                });

            modelBuilder.Entity("M1MartDataAccess.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("M1MartDataAccess.Models.Order", b =>
                {
                    b.Property<string>("InvoiceNumber")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("invoice_number");

                    b.Property<string>("BuyerUsername")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("buyer_username");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime")
                        .HasColumnName("order_date");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("money")
                        .HasColumnName("total_price");

                    b.Property<int>("TotalProduct")
                        .HasColumnType("int")
                        .HasColumnName("total_product");

                    b.HasKey("InvoiceNumber")
                        .HasName("PK__order__8081A63BEFF43953");

                    b.HasIndex("BuyerUsername");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("M1MartDataAccess.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("invoice_number");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money")
                        .HasColumnName("unit_price");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceNumber");

                    b.HasIndex("ProductId");

                    b.ToTable("orderDetail", (string)null);
                });

            modelBuilder.Entity("M1MartDataAccess.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("description");

                    b.Property<bool>("Discontinue")
                        .HasColumnType("bit")
                        .HasColumnName("discontinue");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("price");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("M1MartDataAccess.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("username");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("firstname");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("lastname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("role");

                    b.HasKey("Username")
                        .HasName("PK__user__F3DBC57349E24F75");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("M1MartDataAccess.Models.Cart", b =>
                {
                    b.HasOne("M1MartDataAccess.Models.User", "BuyerUsernameNavigation")
                        .WithMany("Carts")
                        .HasForeignKey("BuyerUsername")
                        .IsRequired()
                        .HasConstraintName("FK__cart__buyer_user__300424B4");

                    b.HasOne("M1MartDataAccess.Models.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__cart__product_id__2F10007B");

                    b.Navigation("BuyerUsernameNavigation");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("M1MartDataAccess.Models.Order", b =>
                {
                    b.HasOne("M1MartDataAccess.Models.User", "BuyerUsernameNavigation")
                        .WithMany("Orders")
                        .HasForeignKey("BuyerUsername")
                        .IsRequired()
                        .HasConstraintName("FK__order__buyer_use__30F848ED");

                    b.Navigation("BuyerUsernameNavigation");
                });

            modelBuilder.Entity("M1MartDataAccess.Models.OrderDetail", b =>
                {
                    b.HasOne("M1MartDataAccess.Models.Order", "InvoiceNumberNavigation")
                        .WithMany("OrderDetails")
                        .HasForeignKey("InvoiceNumber")
                        .IsRequired()
                        .HasConstraintName("FK__orderDeta__invoi__31EC6D26");

                    b.HasOne("M1MartDataAccess.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__orderDeta__produ__32E0915F");

                    b.Navigation("InvoiceNumberNavigation");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("M1MartDataAccess.Models.Product", b =>
                {
                    b.HasOne("M1MartDataAccess.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK__product__categor__2E1BDC42");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("M1MartDataAccess.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("M1MartDataAccess.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("M1MartDataAccess.Models.Product", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("M1MartDataAccess.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
