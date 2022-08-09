using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;
using Application.Models;

namespace Application.Infrastructure
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                Gender man = new Gender { Name = "Man", Slug = "man" };
                Gender woman = new Gender { Name = "Woman", Slug = "woman" };
                Gender kids = new Gender { Name = "Kids", Slug = "kids" };
                Category sneakers = new Category { Name = "Sneakers" };
                Category sweatsuit = new Category { Name = "Sweatsuit" };
                Category t_shirts = new Category { Name = "T_Shirts" };
                Brand nike = new Brand { Name = "Nike", Slug = "nike" };
                Brand adidas = new Brand { Name = "Adidas", Slug = "adidas" };
                Brand puma = new Brand { Name = "Puma", Slug = "puma" };
                Brand champion = new Brand { Name = "Champion", Slug = "champion" };

                context.Products.AddRange(
                        new Product
                        {
                            Name = "Nike AIR MAX COMMAND",
                            Slug = "air max",
                            Description = "Nike AIR MAX COMMAND",
                            Price = 17900,
                            Brand = nike,
                            Category = sneakers,
                            Gender = man,
                            Image = "AM_Command.jpg"
                        },
                        new Product
                        {
                            Name = "Adidas Astir",
                            Slug = "adidas",
                            Description = "Adidas Astir",
                            Price = 8900,
                            Brand = adidas,
                            Category = sneakers,
                            Gender = kids,
                            Image = "adidas_sw.jpg"
                        },
                        new Product
                        {
                            Name = "Puma T-Shirt",
                            Slug = "puma",
                            Description = "Puma T-Shirt",
                            Price = 8900,
                            Brand = puma,
                            Category = t_shirts,
                            Gender = woman,
                            Image = "puma_T.jpg"
                        },
                        new Product
                        {
                            Name = "Champion pants",
                            Slug = "champion",
                            Description = "Champion Pants",
                            Price = 8900,
                            Brand = champion,
                            Category = sweatsuit,
                            Gender = man,
                            Image = "champion.jpg"
                        }
                );

                context.SaveChanges();
            }
        }
    }
}