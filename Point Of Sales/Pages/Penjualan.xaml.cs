using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Point_Of_Sales.Pages
{
    /// <summary>
    /// Interaction logic for Penjualan.xaml
    /// </summary>
    public partial class Penjualan : Page
    {

        private String cs = @"Data Source=AHMADMSFF\SQLEXPRESS;Initial Catalog=api;Integrated Security=True";
        
        public Penjualan()
        {
            InitializeComponent();

            textTanggal.Text = DateTime.Now.ToString("dd-MM-yyyy");
            
            textNoNota.Text = "NP" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            this.Barangs = new ObservableCollection<AddBarang>();
            this.Barangs.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(dataChanged);

        }

        public ObservableCollection<AddBarang> Barangs { get; set; }

        private void BtnBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Main());
        }

        private void BtnGetID_Click(object sender, RoutedEventArgs e)
        {
            Utils.GetIDProduk getID = new Utils.GetIDProduk();
            getID.DataContext = this;
            getID.Show();
        }

        private void TextIDProduk_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textIDProduk.Text != "")
            {
                btnTambah.IsEnabled = true;
                using (SqlConnection connect = new SqlConnection(cs))
                {
                    String query = "SELECT * FROM barang WHERE id_barang=@id";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("@id", textIDProduk.Text);
                    try
                    {
                        connect.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            textNamaProduk.Text = reader["nama_barang"].ToString();
                            textHargaSatuan.Text = reader["harga"].ToString();
                            double nominalDisc = 10;
                            double disc = nominalDisc / 100;
                            textDisc.Text = nominalDisc + "%";
                            textHargaDisc.Text = (disc * int.Parse(reader["harga"].ToString())).ToString();
                            textQty.Text = "1";
                            int total = (int.Parse(textQty.Text) * int.Parse(textHargaSatuan.Text)) - int.Parse(textHargaDisc.Text);
                            textSubtotal.Text = total.ToString();

                        }
                        connect.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void TextQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textQty.Text != "0")
            {
                int total = (int.Parse(textQty.Text) * int.Parse(textHargaSatuan.Text)) - int.Parse(textHargaDisc.Text);
                textSubtotal.Text = total.ToString();
            }
        }

        private void BtnTambah_Click(object sender, RoutedEventArgs e)
        {
            AddBarang barang = new AddBarang
            {
                id_barang = textIDProduk.Text,
                nama_barang = textNamaProduk.Text,
                harga = textHargaSatuan.Text,
                disc = textDisc.Text,
                qty = textQty.Text,
                subTotal = textSubtotal.Text
            };
            this.Barangs.Add(barang);
            dataGrid.ItemsSource = Barangs;
            reset();
            btnTambah.IsEnabled = false;
        }

        private void reset() {
            textIDProduk.Text = "";
            textNamaProduk.Text = "";
            textHargaSatuan.Text = "0";
            textDisc.Text = "0%";
            textHargaDisc.Text = "0";
            textQty.Text = "0";
            textSubtotal.Text = "0";
        }

        public class AddBarang
        {
            public String id_barang { get; set; }
            public String nama_barang { get; set; }
            public String harga { get; set; }
            public String disc { get; set; }
            public String qty { get; set; }
            public String subTotal { get; set; }
        }
        private void Btnbayar_Click(object sender, RoutedEventArgs e)
        {
            Utils.Pembayaran pembayaran = new Utils.Pembayaran();
            pembayaran.DataContext = this;
            int sum = Barangs.Sum(item => int.Parse(item.subTotal));
            pembayaran.textTotal.Text = (sum - (0.01 * sum)).ToString();
            pembayaran.Show();
        }

        private void dataChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            int sum = Barangs.Sum(item => int.Parse(item.subTotal));

            totalHarga.Text = (sum - (0.01 * sum)).ToString("C2", new CultureInfo("id-ID"));
            textSubtotalBawah.Text = sum.ToString("C2", new CultureInfo("id-ID"));
            textPPn.Text = (0.01 * sum).ToString("C2", new CultureInfo("id-ID"));
            textTotal.Text = (sum - (0.01 * sum)).ToString("C2", new CultureInfo("id-ID"));
        }
    }
}
