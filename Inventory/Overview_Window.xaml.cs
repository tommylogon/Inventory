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

namespace Inventory
{
    /// <summary>
    /// Interaction logic for Overview_Window.xaml
    /// </summary>
    public partial class Overview_Window : Window
    {
        public Overview_Window()
        {
            InitializeComponent();
        }

        private void Btn_fridge_Click(object sender, RoutedEventArgs e)
        {
            InsideLocation_Window inside = new InsideLocation_Window();
            inside.Show();
        }
    }
}
