using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels
{
    public class PizzeriaContext : DbContext
    {
        public PizzeriaContext(DbContextOptions<PizzeriaContext> options)
            : base(options)
        {

        }

        public DbSet<Discount> Discount { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ReceivingMethod> ReceivingMethod { get; set; }
        public DbSet<Order> Order { get; set; }
    }

}