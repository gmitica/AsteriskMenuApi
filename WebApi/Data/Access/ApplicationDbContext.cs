using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data.Access
{

    public class ApplicationDbContext : DbContext
    {

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantItemCategory> RestaurantItemCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UserRestaurant> UserRestaurants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<UserRestaurant>().HasKey(ur => new {ur.UserId, ur.RestaurantId});
            modelBuilder.Entity<OrderItem>().HasKey(oi => new {oi.RestaurantItemCategoryId, oi.OrderId});
        }

        /**
         * TODO: Quitar cuando se ponga en producción
         */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
