namespace Inventory
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InventoryContext : DbContext
    {
        public InventoryContext()
            : base("name=InventoryContext")
        {
        }

        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>()
                .Property(e => e.Location)
                .IsFixedLength();

            modelBuilder.Entity<Food>()
                .Property(e => e.BaseItem)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.Barcode)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.Store)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Foods)
                .WithRequired(e => e.Item)
                .HasForeignKey(e => e.BaseItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasOptional(e => e.Item1)
                .WithRequired(e => e.Item2);
        }
    }
}
