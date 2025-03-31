using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Model
{
    public class Food : Item
    {
        private string _location;
        [Required(ErrorMessage = "Location is required")]
        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private DateTime _boughtDate;
        public DateTime BoughtDate
        {
            get => _boughtDate;
            set
            {
                if (SetProperty(ref _boughtDate, value))
                {
                    UpdateExpirationDate();
                }
            }
        }

        private DateTime _expirationDate;
        public DateTime ExpirationDate
        {
            get => _expirationDate;
            private set => SetProperty(ref _expirationDate, value);
        }

        private void UpdateExpirationDate()
        {
            ExpirationDate = BoughtDate.AddDays(LifeTime);
        }

        public bool IsExpired => DateTime.Now > ExpirationDate;
        
        public int DaysUntilExpiration => (ExpirationDate - DateTime.Now).Days;

        public string ExpirationStatus
        {
            get
            {
                if (IsExpired) return "Expired";
                if (DaysUntilExpiration <= 7) return "Expiring Soon";
                return "Good";
            }
        }
    }
} 