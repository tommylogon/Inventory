using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Inventory
{
    /// <summary>
    /// Interaction logic for AddNewItem_Window.xaml
    /// </summary>
    public partial class AddNewItem_Window : Window, INotifyPropertyChanged
    {
        private Item newItem = new Item();

        public event PropertyChangedEventHandler PropertyChanged;

        public Item NewItem
        {
            get
            {
                return newItem;
            }
            set
            {
                if (newItem != value)
                {
                    newItem = value;
                    OnPropertyChanged("NewItem");
                }
            }
        }

        public AddNewItem_Window()
        {
            InitializeComponent();
            DataContext = MainWindow.AppWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.Items.Add(NewItem);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}