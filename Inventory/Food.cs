namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Food")]
    public partial class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Food_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Location { get; set; }

        [Column(TypeName = "date")]
        public DateTime BoughtDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(10)]
        public string BaseItem { get; set; }

        public virtual Item Item { get; set; }
    }
}
