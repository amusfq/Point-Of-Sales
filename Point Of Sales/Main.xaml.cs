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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Point_Of_Sales
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

        private void BtnBarang_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Barang());
        }

        private void BtnMember_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Member());
        }

        private void BtnSupplier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPenjualan_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Penjualan());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
