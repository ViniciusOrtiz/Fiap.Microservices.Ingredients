using GeekBurguer.Ingredients.Api.Model.Products;
using GeekBurguer.Ingredients.Api.Model.Products.Response;
using Microsoft.EntityFrameworkCore;

namespace GeekBurguer.Ingredients.Api.Infra
{
    public class IngredientsContext : DbContext
    {
        public IngredientsContext(DbContextOptions<IngredientsContext> options)
            : base(options)

        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

}
