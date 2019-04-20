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

namespace Point_Of_Sales.Pages
{
    /// <summary>
    /// Interaction logic for Penjualan.xaml
    /// </summary>
    public partial class Penjualan : Page
    {
        public Penjualan()
        {
            InitializeComponent();

            textTanggal.Text = DateTime.Now.ToString("dd-MM-yyyy");
            
            textNoNota.Text = "NP" + DateTime.Now.ToString("ddMMyyyyHHmmss");
        }

        private void BtnBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Main());
        }
    }
}
