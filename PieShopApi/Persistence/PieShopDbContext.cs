using Microsoft.EntityFrameworkCore;
using PieShopApi.Filters;
using PieShopApi.Models.Allergies;
using PieShopApi.Models.Pies;

namespace PieShopApi.Persistence
{
    public class PieShopDbContext : DbContext
    {
        public PieShopDbContext(DbContextOptions<PieShopDbContext> options) : base(options)
        {
        }

        public DbSet<Pie> Pies { get; set; }
        public DbSet<Allergy> Allergies { get; set; }

        public DbSet<AuditEntry> AuditEntries { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allergy>().HasData(
                new Allergy { Id = -1, Name = "None" },
                new Allergy { Id = 1, Name = "Cow's milk" },
                new Allergy { Id = 2, Name = "Eggs" },
                new Allergy { Id = 3, Name = "Tree nuts" },
                new Allergy { Id = 4, Name = "Peanuts" },
                new Allergy { Id = 5, Name = "Shellfish" },
                new Allergy { Id = 6, Name = "Wheat" },
                new Allergy { Id = 7, Name = "Soy" },
                new Allergy { Id = 8, Name = "Fish" },
                new Allergy { Id = 9, Name = "Sesame" }
            );

            modelBuilder.Entity<Pie>().HasData(
                new Pie { Id = 1, Name = "Apple Pie", Description = "Tasty", Category = "Fruit" },
                new Pie { Id = 2, Name = "Cherry Pie", Description = "Yummy", Category = "Fruit" },
                new Pie { Id = 3, Name = "Pumpkin Pie", Description = "Delicious", Category = "Vegetable" },
                new Pie { Id = 4, Name = "Blueberry Pie", Description = "Scrumptious", Category = "Fruit" },
                new Pie { Id = 5, Name = "Peach Pie", Description = "Succulent", Category = "Fruit" },
                new Pie { Id = 6, Name = "Pecan Pie", Description = "Rich", Category = "Nutty" },
                new Pie { Id = 7, Name = "Lemon Meringue Pie", Description = "Zesty", Category = "Fruit" },
                new Pie { Id = 8, Name = "Key Lime Pie", Description = "Tangy", Category = "Fruit" },
                new Pie { Id = 9, Name = "Banana Cream Pie", Description = "Creamy", Category = "Fruit" },
                new Pie { Id = 10, Name = "Coconut Cream Pie", Description = "Tropical", Category = "Nutty" },
                new Pie { Id = 11, Name = "Rhubarb Pie", Description = "Tart", Category = "Fruit" },
                new Pie { Id = 12, Name = "Sweet Potato Pie", Description = "Velvety", Category = "Vegetable" },
                new Pie { Id = 13, Name = "Blackberry Pie", Description = "Juicy", Category = "Fruit" },
                new Pie { Id = 14, Name = "Bumbleberry Pie", Description = "Berry-licious", Category = "Fruit" },
                new Pie { Id = 15, Name = "Pear Pie", Description = "Sweet", Category = "Fruit" },
                new Pie { Id = 16, Name = "Raspberry Pie", Description = "Tangy", Category = "Fruit" },
                new Pie { Id = 17, Name = "Strawberry Rhubarb Pie", Description = "Refreshing", Category = "Fruit" },
                new Pie { Id = 18, Name = "Mississippi Mud Pie", Description = "Decadent", Category = "Other" },
                new Pie { Id = 19, Name = "Salted Caramel Pie", Description = "Savory-sweet", Category = "Other" },
                new Pie { Id = 20, Name = "Mince Pie", Description = "Spiced", Category = "Fruit" },
                new Pie { Id = 21, Name = "Custard Pie", Description = "Smooth", Category = "Other" },
                new Pie { Id = 22, Name = "French Silk Pie", Description = "Luxurious", Category = "Other" },
                new Pie { Id = 23, Name = "Grasshopper Pie", Description = "Minty", Category = "Other" },
                new Pie { Id = 24, Name = "S'mores Pie", Description = "Toasty", Category = "Other" },
                new Pie { Id = 25, Name = "Margarita Pie", Description = "Zingy", Category = "Alcohol" },
                new Pie { Id = 26, Name = "Mud Pie", Description = "Dense", Category = "Other" },
                new Pie { Id = 27, Name = "Oatmeal Pie", Description = "Hearty", Category = "Nutty" },
                new Pie { Id = 28, Name = "Peanut Butter Pie", Description = "Rich", Category = "Nutty" },
                new Pie { Id = 29, Name = "Butterscotch Pie", Description = "Buttery", Category = "Other" },
                new Pie { Id = 30, Name = "Chess Pie", Description = "Classic", Category = "Other" },
                new Pie { Id = 31, Name = "Almond Pie", Description = "Nutty", Category = "Nutty" },
                new Pie { Id = 32, Name = "Caramel Apple Pie", Description = "Sticky-sweet", Category = "Fruit" },
                new Pie { Id = 33, Name = "Maple Pie", Description = "Syrupy", Category = "Other" },
                new Pie { Id = 34, Name = "Orange Marmalade Pie", Description = "Bitter-sweet", Category = "Fruit" },
                new Pie { Id = 35, Name = "Fig Pie", Description = "Jammy", Category = "Fruit" },
                new Pie { Id = 36, Name = "Chocolate Peanut Butter Pie", Description = "Sinful", Category = "Nutty" },
                new Pie { Id = 37, Name = "Lime Pie", Description = "Citrusy", Category = "Fruit" },
                new Pie { Id = 38, Name = "Vanilla Bean Pie", Description = "Aromatic", Category = "Other" },
                new Pie { Id = 39, Name = "Mixed Berry Pie", Description = "Bursting with berries", Category = "Fruit" },
                new Pie { Id = 40, Name = "Espresso Pie", Description = "Coffee-flavored", Category = "Other" },
                new Pie { Id = 41, Name = "Spiced Pear Pie", Description = "Warm", Category = "Fruit" },
                new Pie { Id = 42, Name = "Toffee Pie", Description = "Buttery-caramel", Category = "Other" },
                new Pie { Id = 43, Name = "Plum Pie", Description = "Tart-sweet", Category = "Fruit" },
                new Pie { Id = 44, Name = "Mango Pie", Description = "Exotic", Category = "Fruit" },
                new Pie { Id = 45, Name = "Blackcurrant Pie", Description = "Intense", Category = "Fruit" },
                new Pie { Id = 46, Name = "Nectarine Pie", Description = "Summery", Category = "Fruit" },
                new Pie { Id = 47, Name = "Macadamia Nut Pie", Description = "Crunchy", Category = "Nutty" },
                new Pie { Id = 48, Name = "Cranberry Pie", Description = "Tangy", Category = "Fruit" },
                new Pie { Id = 49, Name = "White Chocolate Raspberry Pie", Description = "Velvety-rich", Category = "Fruit" },
                new Pie { Id = 50, Name = "Apricot Pie", Description = "Honeyed", Category = "Fruit" },
                new Pie { Id = 51, Name = "Vegan & Gluten-Free Apple Pie", Description = "Allergy free", Category = "Vegan" }
            );

            modelBuilder.Entity<Pie>()
                .HasMany(p => p.AllergyItems)
                .WithMany(a => a.Pies)
                .UsingEntity(p => p.HasData(
                    new { PiesId = 1, AllergyItemsId = 1 },
                    new { PiesId = 1, AllergyItemsId = 2 },
                    new { PiesId = 1, AllergyItemsId = 6 },
                    new { PiesId = 51, AllergyItemsId = -1 }
                    ));

            base.OnModelCreating(modelBuilder);

        }
    }
}