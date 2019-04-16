using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Classes
{
    public class Food : INotifyPropertyChanged

    {
        private Item baseItem;
        private string location;
        private DateTime boughtDate;
        private DateTime expirationDate;

        public Food()
        {
        }

        public Food(Item item)
        {
            this.BaseItem = item;
            this.BoughtDate = DateTime.Now;
            this.expirationDate = BoughtDate.AddDays(item.LifeTime);
        }

        public Item BaseItem
        {
            get
            {
                return baseItem;
            }
            set
            {
                if (baseItem != value)
                {
                    baseItem = value;
                    OnPropertyChanged("BaseItem");
                }
            }
        }

        public DateTime ExpirationDate
        {
            get
            {
                return expirationDate;
            }
            set
            {
                if (expirationDate != value)
                {
                    expirationDate = value;
                    OnPropertyChanged("ExpirationDate");
                }
            }
        }

        public DateTime BoughtDate
        {
            get
            {
                return boughtDate;
            }
            set
            {
                if (boughtDate != value)
                {
                    boughtDate = value;
                    OnPropertyChanged("BoughtDate");
                }
            }
        }

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}