using Microsoft.EntityFrameworkCore;

namespace Models.Entity
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }

        public DbSet<Street> Street { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CityHasStreet> CityHasStreet { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddress { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<QuantityMeasurementUnit> QuantityMeasurementUnit { get; set; }
        public DbSet<ProductQuantity> ProductQuantity { get; set; }
        public DbSet<Ingridient> Ingridient { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductIncludeIngridient> ProductIncludeIngridient { get; set; }
        public DbSet<ProductVariety> ProductVariety { get; set; }
        public DbSet<ReceivingMethod> ReceivingMethod { get; set; }
        public DbSet<Operation> Operation { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderIncludeProductVariety> OrderIncludeProductVariety { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }
        public DbSet<Delivery> Delivery { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CityHasStreet>()
                .HasKey("IdCity", "IdStreet");

            builder.Entity<OrderIncludeProductVariety>()
                .HasKey("IdOrder", "IdProductVariety");

            builder.Entity<ProductIncludeIngridient>()
                .HasKey("IdProduct", "IdIngridient");
        }
    }
}