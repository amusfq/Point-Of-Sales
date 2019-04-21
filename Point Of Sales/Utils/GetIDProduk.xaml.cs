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
using System.Data;
using System.Data.SqlClient;

namespace Point_Of_Sales.Utils
{
    /// <summary>
    /// Interaction logic for GetIDProduk.xaml
    /// </summary>
    public partial class GetIDProduk : Window
    {
        private String cs = @"Data Source=AHMADMSFF\SQLEXPRESS;Initial Catalog=api;Integrated Security=True";
        public GetIDProduk()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            using (SqlConnection connect = new SqlConnection(cs))
            {
                String query = "SELECT id_barang, nama_barang FROM barang";
                SqlCommand cmd = new SqlCommand(query, connect);
                try
                {
                    connect.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Data");
                    da.Fill(dt);
                    dataGrid.ItemsSource = dt.DefaultView;

                    connect.Close();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        private void BtnBatal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnPilih_Click(object sender, RoutedEventArgs e)
        {
            var tableIndex = dataGrid.SelectedIndex;
            if (tableIndex.ToString() == "-1")
            {
                MessageBox.Show("Belum ada barang dipilih");
            } else
            {
                var id = dataGrid.Columns[0].GetCellContent(dataGrid.Items[tableIndex]) as TextBlock;
                var nama = dataGrid.Columns[1].GetCellContent(dataGrid.Items[tableIndex]) as TextBlock;
                Pages.Penjualan penjualan = (Pages.Penjualan) this.DataContext;
                penjualan.textIDProduk.Text = id.Text;
                this.Close();
            }
        }
    }
}
