using Microsoft.EntityFrameworkCore;

namespace Ewaste_Vs2022.Models
{
    public class EwasteDbContext : DbContext
    {
        public EwasteDbContext(DbContextOptions<EwasteDbContext> options) : base(options)
        {

        }

        public DbSet<PersonMaster> PersonMasters { get; set; }        
        public DbSet<ComplainMaster> ComplainMasters { get; set; }
        public DbSet<FeedbackMaster> FeedbackMasters { get; set; }
        public DbSet<ProductCategoryMaster> ProductCategoryMasters { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }
        public DbSet<QuestionMaster> QuestionMasters { get; set; }

        public DbSet<AreaMaster> AreaMasters { get; set; }
        public DbSet<OrderMaster> OrderMasters { get; set; }
        public DbSet<CartMaster> CartMasters { get; set; }
        public DbSet<DriverMaster> DriverMasters { get; set; }
    }
}
