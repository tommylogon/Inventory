using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace Inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static MainWindow AppWindow;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            AppWindow = this;

            Item bread = new Item();
            bread.Name = "bread";
            bread.Barcode = "21354681321";
            bread.LifeTime = 14;
            bread.Price = 30;
            bread.Store = "EuroCash";

            Food food = new Food()
            {
                Location = "fryseren",
                BaseItem = "21354681321",
                Item = bread
            };
            using (var db = new InventoryContext())
            {
                db.Items.Add(bread);
                db.SaveChanges();
            }
        }

        private void LoadFile()
        {
            if (File.Exists("Items.xml"))
            {
                List<Item> itemsToLoad = XmlSerialization.ReadFromXmlFile<List<Item>>("Items.xml");
            }
            if (File.Exists("Foods.xml"))
            {
                List<Food> itemsToLoad = XmlSerialization.ReadFromXmlFile<List<Food>>("Foods.xml");
            }
        }

        private void SaveFile()
        {
            List<Item> itemToSave = new List<Item>(Items);
            List<Food> foodToSave = new List<Food>(Foods);
            XmlSerialization.WriteToXmlFile<List<Item>>("Items.xml", itemToSave, false);
            XmlSerialization.WriteToXmlFile<List<Food>>("Foods.xml", foodToSave, false);
        }

        private ObservableCollection<Item> items = new ObservableCollection<Item>();
        private ObservableCollection<Food> foods = new ObservableCollection<Food>();

        public ObservableCollection<Item> Items
        {
            get
            {
                return items;
            }
            set
            {
                if (items != value)
                {
                    items = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        public ObservableCollection<Food> Foods
        {
            get
            {
                return foods;
            }
            set
            {
                if (foods != value)
                {
                    foods = value;
                    OnPropertyChanged("Foods");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Inventory_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.Row.Item != null)
            {
                if (!Foods.Contains(e.Row.Item))
                {
                    Foods.Add((Food)e.Row.Item);
                }
                SaveFile();
            }
        }

        private void OpenAddNewItemWindow()
        {
            AddNewItem_Window window = new AddNewItem_Window();
            window.Show();
        }

        private void OpenNewItemWindow_Clicked(object sender, RoutedEventArgs e)
        {
            OpenAddNewItemWindow();
        }

        private void AddNewFood_Clicked(object sender, RoutedEventArgs e)
        {
            OpenAddNewFoodWindow();
        }

        private void OpenAddNewFoodWindow()
        {
            AddFood_Window window = new AddFood_Window();
            window.Show();
        }
    }
}