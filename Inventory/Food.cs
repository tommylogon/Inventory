namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

   
    public partial class Food
    {
        
        public string Location { get; set; }

        public DateTime BoughtDate { get; set; }

       
        public DateTime ExpirationDate { get; set; }

        public virtual Item Item { get; set; }
    }
}
