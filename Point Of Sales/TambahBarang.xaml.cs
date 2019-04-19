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
using System.Data.SqlClient;

namespace Point_Of_Sales
{
    /// <summary>
    /// Interaction logic for TambahBarang.xaml
    /// </summary>
    public partial class TambahBarang : Window
    {

        private String id_barang, nama_barang, harga_barang;
        private String conString = @"Data Source=AHMADMSFF\SQLEXPRESS;Initial Catalog=api;Integrated Security=True";

        public TambahBarang()
        {
            InitializeComponent();
            text_id.Focus();

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            id_barang = text_id.Text;
            nama_barang = text_nama.Text;
            harga_barang = text_harga.Text;

            //if (id_barang == "")
            //{
            //    MessageBox.Show("ID barang tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    text_id.Focus();
            //} else 
            if (nama_barang == "")
            {
                MessageBox.Show("Nama barang tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                text_nama.Focus();
            } else if (harga_barang == "")
            {
                MessageBox.Show("Harga barang tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                text_harga.Focus();
            } else
            {
                using (SqlConnection connect = new SqlConnection(conString))
                {
                    String query = "INSERT INTO barang ( nama_barang, harga) VALUES(@nama, @harga)";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("@nama", nama_barang);
                    cmd.Parameters.AddWithValue("harga", harga_barang);
                    try
                    {
                        connect.Open();
                        int result = cmd.ExecuteNonQuery();
                        if(result == 1)
                        {
                            MessageBox.Show("Barang " + nama_barang + " berhasil disimpan");
                            this.Close();
                        }
                        connect.Close();
                    } catch (SqlException se)
                    {
                        MessageBox.Show(se.ToString());
                    }
                }
            }
        }
    }
}
