﻿// <auto-generated />
using System;
using CocktailMaker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CocktailMaker.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CocktailMaker.Data.Entities.Cocktail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("integer")
                        .HasColumnName("category");

                    b.Property<int>("CocktailDbId")
                        .HasColumnType("integer")
                        .HasColumnName("cocktail_db_id");

                    b.Property<int>("Glass")
                        .HasColumnType("integer")
                        .HasColumnName("glass");

                    b.Property<string>("IbaCategory")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("iba_category");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("instructions");

                    b.Property<int>("IsAlcoholic")
                        .HasColumnType("integer")
                        .HasColumnName("is_alcoholic");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Cocktails");
                });

            modelBuilder.Entity("CocktailMaker.Data.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("ABV")
                        .HasColumnType("double precision")
                        .HasColumnName("abv");

                    b.Property<int?>("CocktailDbId")
                        .HasColumnType("integer")
                        .HasColumnName("cocktail_db_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsAlcohol")
                        .HasColumnType("boolean")
                        .HasColumnName("is_alcohol");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("CocktailMaker.Data.Entities.Measure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CocktailId")
                        .HasColumnType("integer")
                        .HasColumnName("cocktail_id");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer")
                        .HasColumnName("ingredient_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("CocktailId");

                    b.HasIndex("IngredientId");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("CocktailMaker.Data.Entities.Measure", b =>
                {
                    b.HasOne("CocktailMaker.Data.Entities.Cocktail", "Cocktail")
                        .WithMany("Measures")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailMaker.Data.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cocktail");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("CocktailMaker.Data.Entities.Cocktail", b =>
                {
                    b.Navigation("Measures");
                });
#pragma warning restore 612, 618
        }
    }
}
