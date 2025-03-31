using System.Data.Entity;
using System.IO;

namespace Inventory.Model
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext()
            : base(GetConnectionString())
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<InventoryDbContext>());
        }

        private static string GetConnectionString()
        {
            string dbPath = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "Inventory.db");
            
            return $"Data Source={dbPath};Version=3;";
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("Foods");
                });

            modelBuilder.Entity<Item>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("Items");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
} 