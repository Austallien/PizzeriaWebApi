using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels
{
    public class PizzeriaContext : DbContext
    {
        public PizzeriaContext(DbContextOptions<PizzeriaContext> options)
            : base(options)
        {

        }

       // public DbSet<Discount> Discount { get; set; }
       // public DbSet<Client> Client { get; set; }
       // public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ReceivingMethod> ReceivingMethod { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*builder.Entity<OrderStatus>()
                .ToTable("Order").
                .HasMany(e => e.Orders);*/

            /*builder.Entity<Order>()
                .HasOne(e => e.OrderStatus)
                .WithMany(e => e.Orders)
                .IsRequired()
                .HasForeignKey(e => e.IdStatus);*/

           /* builder.Entity<OrderStatus>()
                .HasMany(e => e.Orders)
                .WithOne(e => e.OrderStatus)
                .IsRequired()
                .HasForeignKey(e => e.IdStatus);*/
        }
    }
}