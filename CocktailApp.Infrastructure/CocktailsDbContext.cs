using System;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CocktailApp.Infrastructure
{
    public partial class CocktailsDbContext : DbContext
    {
        public CocktailsDbContext() : base()
        {       
        }

        public CocktailsDbContext(DbContextOptions<CocktailsDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Alcoholic> Alcoholics { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<Glass> Glasses { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Measure> Measures { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                  optionsBuilder.UseSqlServer("Data Source=A06436\\SQLEXPRESS;Initial Catalog=COCKTAILDB.MDF;Integrated Security=True");
            }  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Alcoholic>(entity =>
            {
                entity.HasKey(e => e.PkId).HasName("PK__Alcoholi__F4A24B22EC97E290");
                entity.ToTable("Alcoholic");
                entity.Property(e => e.PkId).ValueGeneratedNever().HasColumnName("PK_Id");
                entity.Property(e => e.AlcoholicName).IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.PkId).HasName("PK__Category__F4A24B228B21D4F9");
                entity.ToTable("Category");
                entity.Property(e => e.PkId).ValueGeneratedNever().HasColumnName("PK_Id");
                entity.Property(e => e.CategoryName).IsUnicode(false);
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.HasKey(e => e.PkId).HasName("PK__Drink__F4A24B2230E38BAC");
                entity.ToTable("Drink");
                entity.Property(e => e.PkId).ValueGeneratedNever().HasColumnName("PK_Id");
                entity.Property(e => e.DrinkName).HasMaxLength(200).IsUnicode(false);
                entity.Property(e => e.PkId).ValueGeneratedNever().HasColumnName("PK_Id");

                
                entity.Property(e => e.FkAlcoholic).IsUnicode(false).HasColumnName("FK_Alcoholic");
                entity.Property(e => e.FkCategory).IsUnicode(false).HasColumnName("FK_Category");
                entity.Property(e => e.FkGlass).IsUnicode(false).HasColumnName("FK_Glass");
                entity.Property(e => e.IdSource).HasMaxLength(200).IsUnicode(false);
                entity.Property(e => e.Instruction).IsUnicode(false);

                // ?????
                entity.HasOne(d => d.Category).WithMany(d => d.Drinks).HasForeignKey(d => d.FkCategory).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Category");
                entity.HasOne(d => d.Glass).WithMany(d => d.Drinks).HasForeignKey(d => d.FkGlass).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Glass");
                entity.HasOne(d => d.Alcoholic).WithMany(d => d.Drinks).HasForeignKey(d => d.FkAlcoholic).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Alcoholic");

                //entity.HasMany(d => d.Measures).WithOne(d => d.Drink);
            });

            modelBuilder.Entity<Glass>(entity =>
            {
                entity.HasKey(e => e.PkId).HasName("PK__Glass__F4A24B2212CE4704");
                entity.ToTable("Glass");
                entity.Property(e => e.PkId).ValueGeneratedNever().HasColumnName("PK_Id");
                entity.Property(e => e.GlassName).IsUnicode(false);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => e.PkId).HasName("PK__Ingredie__F4A24B221B8F1315");
                entity.ToTable("Ingredient");
                entity.Property(e => e.PkId).ValueGeneratedNever().HasColumnName("PK_Id");
                entity.Property(e => e.IngredientName).HasMaxLength(200).IsUnicode(false);
            });

            modelBuilder.Entity<Measure>(entity =>
            {
                entity.HasKey(e => e.PK_Id);
                entity.ToTable("Measure");
                entity.Property(e => e.IngredientPkId).HasColumnName("FK_IdIngredient");
                entity.Property(e => e.DrinkPkId).HasColumnName("FK_IdDrink");
                entity.Property(e => e.Quantity).IsUnicode(false).HasColumnName("Quantity");

                entity.HasOne(d => d.Ingredient).WithMany(p => p.Measures).HasForeignKey(d => d.IngredientPkId)
                                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_IdIngredient");
                entity.HasOne(d => d.Drink).WithMany(p => p.Measures).HasForeignKey(d => d.DrinkPkId)
                                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_IdDrink");
                //entity.HasKey(e => new { e.IngredientPkId, e.DrinkPkId }).HasName("PK__Measure__E48BFAE6ECCDB344");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.PkId);
                entity.ToTable("Users");
                entity.Property(e => e.PkId).ValueGeneratedNever().HasColumnName("PK_Id");
                entity.Property(e => e.FirstName).HasMaxLength(200).IsUnicode(false);
                entity.Property(e => e.LastName).HasMaxLength(200).IsUnicode(false);
                entity.Property(e => e.Username).HasMaxLength(200).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(200).IsUnicode(false);
                entity.Property(e => e.UserType).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
