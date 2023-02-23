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
using Microsoft.Win32;
using System.IO;

namespace WPFNepesseg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Telepules> lista;

            lista = FajlbaTolt("D:\\Gazdag Zsolt\\wpf2\\WPFNepesseg\\WPFNepesseg\\Adatok\\kozerdeku_lakossag_2022.csv");
            dgTelepulesek.ItemsSource = lista;
            cbMegyek.ItemsSource = lista.Select(x => x.Megye).Distinct().ToList();
        }

        private List<Telepules> FajlbaTolt(string fajlnev)
        {
            List<Telepules> telepulesek = new List<Telepules>();

            string[] sorok = File.ReadAllLines(fajlnev);

            //"Megye kód";"KSH kód";Megye;Település;"Település típusa";"Állandó férfi lakosság összesen";"Állandó női lakosság összesen"

            for (int sorIndexe = 1; sorIndexe < sorok.Length; sorIndexe++)
            {
                Telepules ujTelepules = new Telepules(sorok[sorIndexe].Split(";")[2],
                                                      sorok[sorIndexe].Split(";")[3], sorok[sorIndexe].Split(";")[4], Convert.ToInt32(sorok[sorIndexe].Split(";")[5].Replace(" ", "")), Convert.ToInt32(sorok[sorIndexe].Split(";")[6].Replace(" ", "")));
                telepulesek.Add(ujTelepules);
            }

            return telepulesek;
        }

        private void cbMegyek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string KivalasztottMegye = cbMegyek.SelectedItem.ToString();

            List<Telepules> lista;
            lista = FajlbaTolt("D:\\Gazdag Zsolt\\wpf2\\WPFNepesseg\\WPFNepesseg\\Adatok\\kozerdeku_lakossag_2022.csv");
            dgTelepulesek.ItemsSource = lista.Where(x => x.Megye == KivalasztottMegye);
        }
    }
}
