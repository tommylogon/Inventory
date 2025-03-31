using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Inventory.Model;
using Inventory.Services;

namespace Inventory.ViewModel
{
    public class AddFoodViewModel : BaseViewModel
    {
        private readonly IInventoryService _inventoryService;
        private readonly Window _window;

        public AddFoodViewModel(Window window)
        {
            _inventoryService = new InventoryService();
            _window = window;
            Title = "Add New Food Item";
            BoughtDate = DateTime.Now;

            SaveCommand = new RelayCommand(async _ => await Save());
            CancelCommand = new RelayCommand(_ => Cancel());

            _ = LoadData();
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _barcode;
        public string Barcode
        {
            get => _barcode;
            set => SetProperty(ref _barcode, value);
        }

        private string _category;
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private string _location;
        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private double _weight;
        public double Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        private double _price;
        public double Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private int _lifeTime;
        public int LifeTime
        {
            get => _lifeTime;
            set => SetProperty(ref _lifeTime, value);
        }

        private DateTime _boughtDate;
        public DateTime BoughtDate
        {
            get => _boughtDate;
            set => SetProperty(ref _boughtDate, value);
        }

        private string _store;
        public string Store
        {
            get => _store;
            set => SetProperty(ref _store, value);
        }

        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private ObservableCollection<string> _locations;
        public ObservableCollection<string> Locations
        {
            get => _locations;
            set => SetProperty(ref _locations, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private async Task LoadData()
        {
            try
            {
                IsBusy = true;
                var categories = await _inventoryService.GetAllCategoriesAsync();
                Categories = new ObservableCollection<string>(categories);

                var locations = await _inventoryService.GetAllLocationsAsync();
                Locations = new ObservableCollection<string>(locations);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Save()
        {
            try
            {
                IsBusy = true;

                var food = new Food
                {
                    Name = Name,
                    Barcode = Barcode,
                    Category = Category,
                    Location = Location,
                    Weight = Weight,
                    Price = Price,
                    LifeTime = LifeTime,
                    BoughtDate = BoughtDate,
                    Store = Store
                };

                var result = await _inventoryService.AddFoodAsync(food);
                if (result)
                {
                    _window.DialogResult = true;
                    _window.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save food item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving food item: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Cancel()
        {
            _window.DialogResult = false;
            _window.Close();
        }
    }
} 