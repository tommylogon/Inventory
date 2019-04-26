namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public partial class Item
    {
        
        public Item()
        {
            
        }

       
        public string Barcode { get; set; }

        
        public string Name { get; set; }

        public int LifeTime { get; set; }

        public double Weight { get; set; }

        public double Price { get; set; }

        
        public string Store { get; set; }

        
    }
}
