using System;
using System.Collections.Generic;
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
using Inventory.ViewModel;

namespace Inventory
{
    /// <summary>
    /// Interaction logic for AddFood_Window.xaml
    /// </summary>
    public partial class AddFood_Window : Window
    {
        public AddFood_Window()
        {
            InitializeComponent();
            DataContext = new AddFoodViewModel(this);
        }
    }
}