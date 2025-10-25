using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inventory.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            LoadFile();
            AddFoodCommand = new RelayCommand(OpenAddNewFoodWindow);
            OpenOverviewCommand = new RelayCommand(OpenOverviewWindow);
            AddNewItemCommand = new RelayCommand(OpenAddNewItemWindow);
            RowEditEndingCommand = new RelayCommand(RowEditEnding);
        }

        private void LoadFile()
        {
            try
            {
                string itemsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InventoryManagementSystem", "Items.xml");
                string foodsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InventoryManagementSystem", "Foods.xml");

                if (File.Exists(itemsPath))
                {
                    List<Item> itemsToLoad = XmlSerialization.ReadFromXmlFile<List<Item>>(itemsPath);
                    Items = new ObservableCollection<Item>(itemsToLoad);
                }
                if (File.Exists(foodsPath))
                {
                    List<Food> foodsToLoad = XmlSerialization.ReadFromXmlFile<List<Food>>(foodsPath);
                    Foods = new ObservableCollection<Food>(foodsToLoad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveFile()
        {
            try
            {
                string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InventoryManagementSystem");
                if (!Directory.Exists(appDataFolder))
                {
                    Directory.CreateDirectory(appDataFolder);
                }

                string itemsPath = Path.Combine(appDataFolder, "Items.xml");
                string foodsPath = Path.Combine(appDataFolder, "Foods.xml");

                List<Item> itemToSave = new List<Item>(Items);
                List<Food> foodToSave = new List<Food>(Foods);
                XmlSerialization.WriteToXmlFile<List<Item>>(itemsPath, itemToSave, false);
                XmlSerialization.WriteToXmlFile<List<Food>>(foodsPath, foodToSave, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ObservableCollection<Item> items = new ObservableCollection<Item>();
        private ObservableCollection<Food> foods = new ObservableCollection<Food>();

        public ObservableCollection<Item> Items
        {
            get { return items; }
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
            get { return foods; }
            set
            {
                if (foods != value)
                {
                    foods = value;
                    OnPropertyChanged("Foods");
                }
            }
        }

        public ICommand AddFoodCommand { get; private set; }
        public ICommand OpenOverviewCommand { get; private set; }
        public ICommand AddNewItemCommand { get; private set; }
        public ICommand RowEditEndingCommand { get; private set; }

        private void OpenAddNewFoodWindow(object obj)
        {
            AddFood_Window window = new AddFood_Window();
            window.Show();
        }

        private void OpenOverviewWindow(object obj)
        {
            Overview_Window overview = new Overview_Window();
            overview.Show();
        }

        private void OpenAddNewItemWindow(object obj)
        {
            AddNewItem_Window window = new AddNewItem_Window();
            window.Show();
        }

        private void RowEditEnding(object parameter)
        {
            System.Windows.Controls.DataGridRowEditEndingEventArgs e = (System.Windows.Controls.DataGridRowEditEndingEventArgs)parameter;
            if (e.Row.Item != null)
            {
                if (!Foods.Contains(e.Row.Item))
                {
                    Foods.Add((Food)e.Row.Item);
                }
                SaveFile();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute)
        {
            this._execute = execute;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true : this._canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }
}
