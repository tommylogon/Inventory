using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Model
{
    public class Item : BaseModel
    {
        private string _barcode;
        [Required(ErrorMessage = "Barcode is required")]
        public string Barcode
        {
            get => _barcode;
            set => SetProperty(ref _barcode, value);
        }

        private string _name;
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _lifeTime;
        [Range(1, 3650, ErrorMessage = "Lifetime must be between 1 and 3650 days")]
        public int LifeTime
        {
            get => _lifeTime;
            set => SetProperty(ref _lifeTime, value);
        }

        private double _weight;
        [Range(0.01, 1000000, ErrorMessage = "Weight must be greater than 0")]
        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        private double _price;
        [Range(0.01, 1000000, ErrorMessage = "Price must be greater than 0")]
        public double Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private string _store;
        [Required(ErrorMessage = "Store name is required")]
        public string Store
        {
            get => _store;
            set => SetProperty(ref _store, value);
        }

        private string _category;
        [Required(ErrorMessage = "Category is required")]
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
} 