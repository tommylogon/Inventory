namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Foods = new HashSet<Food>();
        }

        [Key]
        [StringLength(10)]
        public string Barcode { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public int LifeTime { get; set; }

        public double Weight { get; set; }

        public double Price { get; set; }

        [Required]
        [StringLength(10)]
        public string Store { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Food> Foods { get; set; }

        public virtual Item Item1 { get; set; }

        public virtual Item Item2 { get; set; }
    }
}
