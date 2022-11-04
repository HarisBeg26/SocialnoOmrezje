using Microsoft.Win32;
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

namespace Naloga_1
{
    /// <summary>
    /// Interaction logic for OMeni.xaml
    /// </summary>
    public partial class OMeni : UserControl
    {
        private Uporabnik trenutniUporabnik;

        public Uporabnik TrenutniUporabnik
        {
            get => trenutniUporabnik;
            set => trenutniUporabnik = value;
        }

        public OMeni()
        {
            InitializeComponent();
            trenutniUporabnik = new Uporabnik("Haris", "Begovic", "Bijelo Polje");

            trenutniUporabnik.Ime = Properties.Settings.Default.ime;
            trenutniUporabnik.Priimek = Properties.Settings.Default.priimek;
            trenutniUporabnik.RojstniKraj = Properties.Settings.Default.rojstniKraj;
            trenutniUporabnik.ProfilnaSlika = Properties.Settings.Default.profilnaSlika;

            PosodobiGUI();
        }

        private void PosodobiGUI()
        {
            textBoxIme.Text = trenutniUporabnik.Ime;
            textBoxPriimek.Text = trenutniUporabnik.Priimek;
            textBoxRojstniKraj.Text = trenutniUporabnik.RojstniKraj;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(trenutniUporabnik.ProfilnaSlika);
            bitmap.EndInit();
            imageProfilnaSlika.Source = bitmap;
        }

        private void Button_Shrani_O_Meni_Click(object sender, RoutedEventArgs e)
        {
            trenutniUporabnik.Ime = textBoxIme.Text;
            trenutniUporabnik.Priimek = textBoxPriimek.Text;
            trenutniUporabnik.RojstniKraj = textBoxRojstniKraj.Text;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(trenutniUporabnik.ProfilnaSlika);
            bitmap.EndInit();
            imageProfilnaSlika.Source = bitmap;

            Properties.Settings.Default.ime = trenutniUporabnik.Ime;
            Properties.Settings.Default.priimek = trenutniUporabnik.Priimek;
            Properties.Settings.Default.rojstniKraj = trenutniUporabnik.RojstniKraj;
            Properties.Settings.Default.profilnaSlika = trenutniUporabnik.ProfilnaSlika;
            Properties.Settings.Default.Save();
        }

        private void buttonNaloziProfilnoSliko_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
            openFileDialog.Title = "Select Photos";

            if (openFileDialog.ShowDialog() == true)
            {
                trenutniUporabnik.ProfilnaSlika = openFileDialog.FileName;
            }

            PosodobiGUI();
        }
    }
}
