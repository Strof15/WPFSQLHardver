using MySql.Data.MySqlClient;
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

namespace Wpfsqltermek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string kapcsolatLeiro = "datasource=127.0.0.1;port=3306;username=root;password=;database=hardver;";
        List<Termek> termekek = new List<Termek>();
        MySqlConnection SQLkapcsolat;
        public MainWindow()
        {
            InitializeComponent();

            AdatbazisMegnyitas();
            KategoriaBetoltese();
            GyartoBetoltese();

            TermekekBetolteseListaba();

            AdatbazisLezarasa();
        }

        private void KategoriaBetoltese()
        {
            string SQLKategoriaRendezve = "Select DISTINCT kategória from termékek ORDER BY kategória;";
            MySqlCommand SQLparancs = new MySqlCommand(SQLKategoriaRendezve, SQLkapcsolat);
            MySqlDataReader eredmenyOlvaso = SQLparancs.ExecuteReader();

            cbKategoria.Items.Add(" - Nincs megadva - ");
            while (eredmenyOlvaso.Read())
            {
                cbKategoria.Items.Add(eredmenyOlvaso.GetString("kategória"));
            }
            eredmenyOlvaso.Close();
            cbKategoria.SelectedIndex = 0;
        }

        private void GyartoBetoltese()
        {
            string SQLGyartokRendezve = "Select DISTINCT gyártó FROM termékek ORDER BY gyártó;";

            MySqlCommand SQLparancs = new MySqlCommand(SQLGyartokRendezve, SQLkapcsolat);
            MySqlDataReader eredmenyOlvaso = SQLparancs.ExecuteReader();

            cbGyarto.Items.Add(" - Nincs megadva - ");
            while (eredmenyOlvaso.Read())
            {
                cbGyarto.Items.Add(eredmenyOlvaso.GetString("gyártó"));
            }
            eredmenyOlvaso.Close();
            cbGyarto.SelectedIndex = 0;
        
        }

        private void TermekekBetolteseListaba()
        {
            string SQLOsszesTermek = "Select * from termékek;";
            MySqlCommand SQLparancs = new MySqlCommand(SQLOsszesTermek, SQLkapcsolat);

            while (eredmenyOlvaso.Read())
            {
                Termek uj = new Termek(eredmenyOlvaso.GetString("Kategória"),
                                       eredmenyOlvaso.GetString("Gyártó"),
                                       eredmenyOlvaso.GetString("Név"),
                                       eredmenyOlvaso.GetString("Ár"),
                                       eredmenyOlvaso.GetString("Garidő"));

                termekek.add(uj);
            }
            eredmenyOlvaso.Close();

            dgTermekek.ItemsSource = termekek;

        }

        private void AdatbazisMegnyitas()
        {
            try
            {
                SQLkapcsolat = new MySqlConnection(kapcsolatLeiro);
                SQLkapcsolat.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("New tud kapcsolódni az adatbázishoz!");
                this.Close();
                
            }        
        
        
        
        
        
        
        }

        private void AdatbazisLezarasa()
        {
            SQLkapcsolat.Close();
            SQLkapcsolat.Dispose();
        
        }





        private void btnszukit_click(object sender, RoutedEventArgs e)
        {
            termekek.Clear();
            string SQLSzukitettLista = SzukitoLekerdezesEloallitasa();

            MySqlCommand SQLparancs = new MySqlCommand(SQLSzukitettLista, SQLkapcsolat);

            while (eredmenOlvaso.Read())
            {
                Termek uj = new Termek(eredmenyOlvaso.GetString("Kategória"),
                                       eredmenyOlvaso.GetString("Gyártó"),
                                       eredmenyOlvaso.GetString("Név"),
                                       eredmenyOlvaso.GetString("Ár"),
                                       eredmenyOlvaso.GetString("Garidő"));

                termekek.add(uj);
            }
            eredmenyOlvaso.Close();
            dgTermekek.Items.Refresh();
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("blabla.csv);
            foreach (var item in termekek)
            { 
                sw.WriteLine(item.ToCSVString());
            
            }
            sw.Close(); 
        }
    }
}
