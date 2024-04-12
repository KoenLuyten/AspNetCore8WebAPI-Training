using Microsoft.EntityFrameworkCore;
using PieShopApi.Models;

namespace PieShopApi.Persistence
{
    public class PieShopDbContext : DbContext
    {
        public PieShopDbContext(DbContextOptions<PieShopDbContext> options) : base(options)
        {
        }

        public DbSet<Pie> Pies { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pie>().HasData(
                new Pie { Id = 1, Name = "Apple Pie", Description = "Tasty" },
                new Pie { Id = 2, Name = "Cherry Pie", Description = "Yummy" },
                new Pie { Id = 3, Name = "Pumpkin Pie", Description = "Delicious" },
                new Pie { Id = 4, Name = "Blueberry Pie", Description = "Scrumptious" },
                new Pie { Id = 5, Name = "Peach Pie", Description = "Succulent" },
                new Pie { Id = 6, Name = "Pecan Pie", Description = "Rich" },
                new Pie { Id = 7, Name = "Lemon Meringue Pie", Description = "Zesty" },
                new Pie { Id = 8, Name = "Key Lime Pie", Description = "Tangy" },
                new Pie { Id = 9, Name = "Banana Cream Pie", Description = "Creamy" },
                new Pie { Id = 10, Name = "Coconut Cream Pie", Description = "Tropical" },
                new Pie { Id = 11, Name = "Rhubarb Pie", Description = "Tart" },
                new Pie { Id = 12, Name = "Sweet Potato Pie", Description = "Velvety" },
                new Pie { Id = 13, Name = "Blackberry Pie", Description = "Juicy" },
                new Pie { Id = 14, Name = "Bumbleberry Pie", Description = "Berry-licious" },
                new Pie { Id = 15, Name = "Pear Pie", Description = "Sweet" },
                new Pie { Id = 16, Name = "Raspberry Pie", Description = "Tangy" },
                new Pie { Id = 17, Name = "Strawberry Rhubarb Pie", Description = "Refreshing" },
                new Pie { Id = 18, Name = "Mississippi Mud Pie", Description = "Decadent" },
                new Pie { Id = 19, Name = "Salted Caramel Pie", Description = "Savory-sweet" },
                new Pie { Id = 20, Name = "Mince Pie", Description = "Spiced" },
                new Pie { Id = 21, Name = "Custard Pie", Description = "Smooth" },
                new Pie { Id = 22, Name = "French Silk Pie", Description = "Luxurious" },
                new Pie { Id = 23, Name = "Grasshopper Pie", Description = "Minty" },
                new Pie { Id = 24, Name = "S'mores Pie", Description = "Toasty" },
                new Pie { Id = 25, Name = "Margarita Pie", Description = "Zingy" },
                new Pie { Id = 26, Name = "Mud Pie", Description = "Dense" },
                new Pie { Id = 27, Name = "Oatmeal Pie", Description = "Hearty" },
                new Pie { Id = 28, Name = "Peanut Butter Pie", Description = "Rich" },
                new Pie { Id = 29, Name = "Butterscotch Pie", Description = "Buttery" },
                new Pie { Id = 30, Name = "Chess Pie", Description = "Classic" },
                new Pie { Id = 31, Name = "Almond Pie", Description = "Nutty" },
                new Pie { Id = 32, Name = "Caramel Apple Pie", Description = "Sticky-sweet" },
                new Pie { Id = 33, Name = "Maple Pie", Description = "Syrupy" },
                new Pie { Id = 34, Name = "Orange Marmalade Pie", Description = "Bitter-sweet" },
                new Pie { Id = 35, Name = "Fig Pie", Description = "Jammy" },
                new Pie { Id = 36, Name = "Chocolate Peanut Butter Pie", Description = "Sinful" },
                new Pie { Id = 37, Name = "Lime Pie", Description = "Citrusy" },
                new Pie { Id = 38, Name = "Vanilla Bean Pie", Description = "Aromatic" },
                new Pie { Id = 39, Name = "Mixed Berry Pie", Description = "Bursting with berries" },
                new Pie { Id = 40, Name = "Espresso Pie", Description = "Coffee-flavored" },
                new Pie { Id = 41, Name = "Spiced Pear Pie", Description = "Warm" },
                new Pie { Id = 42, Name = "Toffee Pie", Description = "Buttery-caramel" },
                new Pie { Id = 43, Name = "Plum Pie", Description = "Tart-sweet" },
                new Pie { Id = 44, Name = "Mango Pie", Description = "Exotic" },
                new Pie { Id = 45, Name = "Blackcurrant Pie", Description = "Intense" },
                new Pie { Id = 46, Name = "Nectarine Pie", Description = "Summery" },
                new Pie { Id = 47, Name = "Macadamia Nut Pie", Description = "Crunchy" },
                new Pie { Id = 48, Name = "Cranberry Pie", Description = "Tangy" },
                new Pie { Id = 49, Name = "White Chocolate Raspberry Pie", Description = "Velvety-rich" },
                new Pie { Id = 50, Name = "Apricot Pie", Description = "Honeyed" }
            );

            base.OnModelCreating(modelBuilder);

        }
    }
}