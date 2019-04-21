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
using System.Globalization;

namespace Point_Of_Sales.Utils
{
    /// <summary>
    /// Interaction logic for Pembayaran.xaml
    /// </summary>
    public partial class Pembayaran : Window
    {
        public Pembayaran()
        {
            InitializeComponent();
        }

        private void BtnBayar_Click(object sender, RoutedEventArgs e)
        {
            if (textBayar.Text != "0")
            {
                if (int.Parse(textBayar.Text) < int.Parse(textTotal.Text)) {
                    int kurang = int.Parse(textTotal.Text) - int.Parse(textBayar.Text);
                    MessageBox.Show("Uang pembayaran kurang " + kurang.ToString());
                } else
                {
                    Kembalian kembalian = new Kembalian();
                    kembalian.DataContext = this;
                    int kembali = int.Parse(textBayar.Text) - int.Parse(textTotal.Text);
                    kembalian.textKembali.Text = kembali.ToString("C2", new CultureInfo("id-ID"));
                    kembalian.Show();
                    this.Close();
                }
            } 
        }
    }
}
