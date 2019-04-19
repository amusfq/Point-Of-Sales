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
using System.Data;
using System.Data.SqlClient;

namespace Point_Of_Sales.Pages
{
    /// <summary>
    /// Interaction logic for Barang.xaml
    /// </summary>
    public partial class Barang : Page
    {

        private String cs = @"Data Source=AHMADMSFF\SQLEXPRESS;Initial Catalog=api;Integrated Security=True";

        public Barang()
        {
            InitializeComponent();
            load();
        }

        private void BtnTambah_Click(object sender, RoutedEventArgs e)
        {
            btnTambah.Visibility = Visibility.Collapsed;
            btnSimpan.Visibility = Visibility.Visible;
            btnBatal.Visibility = Visibility.Visible;
            enableInput();
        }

        private void BtnSimpan_Click(object sender, RoutedEventArgs e)
        {
            if (textNama.Text == "")
            {
                MessageBox.Show("Nama barang tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                textNama.Focus();
            }
            else if (textHarga.Text == "")
            {
                MessageBox.Show("Harga barang tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                textHarga.Focus();
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(cs))
                {
                    String query = "INSERT INTO barang ( nama_barang, harga) VALUES(@nama, @harga)";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("@nama", textNama.Text);
                    cmd.Parameters.AddWithValue("@harga", textHarga.Text);
                    try
                    {
                        connect.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result == 1)
                        {
                            var msg = MessageBox.Show("Barang " + textNama.Text + " berhasil disimpan", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                            if (msg == MessageBoxResult.OK)
                            {
                                load();
                                reset();
                            }

                        }
                        connect.Close();
                    }
                    catch (SqlException se)
                    {
                        MessageBox.Show(se.ToString());
                    }
                }
            }
        }

        private void BtnUbah_Click(object sender, RoutedEventArgs e)
        {
            enableInput();
            btnUbah.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Visible;
        }

        private void BtnBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Main());
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (textNama.Text == "")
            {
                MessageBox.Show("Nama barang tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                textNama.Focus();
            }
            else if (textHarga.Text == "")
            {
                MessageBox.Show("Harga barang tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                textHarga.Focus();
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(cs))
                {
                    String query = "UPDATE barang SET nama_barang=@nama, harga=@harga WHERE id_barang=@id";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("@id", textId.Text);
                    cmd.Parameters.AddWithValue("@nama", textNama.Text);
                    cmd.Parameters.AddWithValue("@harga", textHarga.Text);
                    try
                    {
                        connect.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result == 1)
                        {
                            var msg = MessageBox.Show("Barang " + textNama.Text + " berhasil diupdate", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                            if (msg == MessageBoxResult.OK)
                            {
                                load();
                                reset();
                            }

                        }
                        connect.Close();
                    }
                    catch (SqlException se)
                    {
                        MessageBox.Show(se.ToString());
                    }
                }
            }
        }

        private void TextSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (textSearch.Text.Length > 3)
            {
                search();
            }
            else if (textSearch.Text.Length == 0)
            {
                load();
            }
        }

        private void BtnBatal_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void enableInput()
        {
            textId.IsEnabled = true;
            textNama.IsEnabled = true;
            textHarga.IsEnabled = true;
        }

        private void reset()
        {
            btnBatal.Visibility = Visibility.Collapsed;
            btnTambah.Visibility = Visibility.Visible;
            btnSimpan.Visibility = Visibility.Collapsed;
            btnUbah.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            textId.Text = "";
            textNama.Text = "";
            textHarga.Text = "";
            textId.IsEnabled = false;
            textNama.IsEnabled = false;
            textHarga.IsEnabled = false;
            dataGrid.SelectedIndex = -1;
        }

        private void load()
        {
            using (SqlConnection connect = new SqlConnection(cs))
            {
                String query = "SELECT * FROM barang";
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

        private void search()
        {
            using (SqlConnection connect = new SqlConnection(cs))
            {
                String query = "SELECT * FROM barang WHERE id_barang like '%'+@key+'%' OR nama_barang like '%'+@key+'%'";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@key", textSearch.Text);
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

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var tableIndex = dataGrid.SelectedIndex;
            if (tableIndex.ToString() != "-1")
            {
                var id = dataGrid.Columns[0].GetCellContent(dataGrid.Items[tableIndex]) as TextBlock;
                var nama = dataGrid.Columns[1].GetCellContent(dataGrid.Items[tableIndex]) as TextBlock;
                var harga = dataGrid.Columns[2].GetCellContent(dataGrid.Items[tableIndex]) as TextBlock;

                textId.Text = id.Text;
                textNama.Text = nama.Text;
                textHarga.Text = harga.Text;
                btnTambah.Visibility = Visibility.Collapsed;
                btnUbah.Visibility = Visibility.Visible;
                btnBatal.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var msg = MessageBox.Show("Barang " + textNama.Text + " akan dihapus", "Peringatan", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msg == MessageBoxResult.Yes)
            {
                using (SqlConnection connect = new SqlConnection(cs))
                {
                    String query = "DELETE FROM barang WHERE id_barang=@id";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("@id", textId.Text);
                    try
                    {
                        connect.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result == 1)
                        {
                            msg = MessageBox.Show("Barang " + textNama.Text + " berhasil dihapus", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                            if (msg == MessageBoxResult.OK)
                            {
                                load();
                                reset();
                            }

                        }
                        connect.Close();
                    }
                    catch (SqlException se)
                    {
                        MessageBox.Show(se.ToString());
                    }
                }
            }
        }
    }
}
