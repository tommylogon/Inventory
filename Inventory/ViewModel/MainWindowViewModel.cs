using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Inventory.Model;
using Inventory.Services;

namespace Inventory.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IInventoryService _inventoryService;
        
        public MainWindowViewModel()
        {
            _inventoryService = new InventoryService();
            Title = "Inventory Manager";
            
            LoadDataCommand = new RelayCommand(async _ => await LoadData());
            AddFoodCommand = new RelayCommand(_ => AddFood());
            DeleteFoodCommand = new RelayCommand(async p => await DeleteFood(p as Food), p => p is Food);
            RefreshCommand = new RelayCommand(async _ => await LoadData());
            
            // Load data when starting
            _ = LoadData();
        }

        private ObservableCollection<Food> _foods;
        public ObservableCollection<Food> Foods
        {
            get => _foods;
            set => SetProperty(ref _foods, value);
        }

        private ObservableCollection<Food> _expiringFoods;
        public ObservableCollection<Food> ExpiringFoods
        {
            get => _expiringFoods;
            set => SetProperty(ref _expiringFoods, value);
        }

        private ObservableCollection<string> _locations;
        public ObservableCollection<string> Locations
        {
            get => _locations;
            set => SetProperty(ref _locations, value);
        }

        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private Food _selectedFood;
        public Food SelectedFood
        {
            get => _selectedFood;
            set => SetProperty(ref _selectedFood, value);
        }

        public ICommand LoadDataCommand { get; }
        public ICommand AddFoodCommand { get; }
        public ICommand DeleteFoodCommand { get; }
        public ICommand RefreshCommand { get; }

        private async Task LoadData()
        {
            try
            {
                IsBusy = true;

                var foods = await _inventoryService.GetAllFoodAsync();
                Foods = new ObservableCollection<Food>(foods);

                var expiringFoods = await _inventoryService.GetExpiringFoodAsync();
                ExpiringFoods = new ObservableCollection<Food>(expiringFoods);

                var locations = await _inventoryService.GetAllLocationsAsync();
                Locations = new ObservableCollection<string>(locations);

                var categories = await _inventoryService.GetAllCategoriesAsync();
                Categories = new ObservableCollection<string>(categories);
            }
            catch (Exception ex)
            {
                // TODO: Show error message to user
                System.Diagnostics.Debug.WriteLine($"Error loading data: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void AddFood()
        {
            var window = new AddFood_Window();
            window.ShowDialog();
            _ = LoadData(); // Refresh data after adding
        }

        private async Task DeleteFood(Food food)
        {
            if (food == null) return;

            var result = System.Windows.MessageBox.Show(
                $"Are you sure you want to delete {food.Name}?",
                "Confirm Delete",
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Question);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                await _inventoryService.DeleteFoodAsync(food.Barcode);
                await LoadData(); // Refresh data after deleting
            }
        }
    }
} 