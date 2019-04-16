using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Classes
{
    public class Item : INotifyPropertyChanged
    {
        private string itemName;

        private string barcode;
        private int lifeTime;
        private double weight;
        private double price;
        private string fromWhatStore;

        public Item()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int LifeTime
        {
            get
            {
                return lifeTime;
            }
            set
            {
                if (lifeTime != value)
                {
                    lifeTime = value;
                    OnPropertyChanged("LifeTime");
                }
            }
        }

        public string Barcode
        {
            get
            {
                return barcode;
            }
            set
            {
                if (barcode != value)
                {
                    barcode = value;
                    OnPropertyChanged("Barcode");
                }
            }
        }

        public string Name
        {
            get
            {
                return itemName;
            }
            set
            {
                if (itemName != value)
                {
                    itemName = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        public string Store
        {
            get
            {
                return fromWhatStore;
            }
            set
            {
                if (fromWhatStore != value)
                {
                    fromWhatStore = value;
                    OnPropertyChanged("Store");
                }
            }
        }

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