using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Naloga_1
{
    /// <summary>
    /// Interaction logic for UrediPrijateljaOkno.xaml
    /// </summary>
    public partial class PrijateljOkno : Window
    {
        private Uporabnik uporabnik;

        public PrijateljOkno()
        {
            InitializeComponent();
            uporabnik = new Uporabnik();
        }

        public PrijateljOkno(Uporabnik u)
        {
            InitializeComponent();
            uporabnik = u;
            PosodobiGUI();
        }

        private void PosodobiGUI()
        {
            TextBox_Ime.Text = uporabnik.Ime;
            TextBox_Priimek.Text = uporabnik.Priimek;
            TextBox_Rojstni_Kraj.Text = uporabnik.RojstniKraj;

            if (File.Exists(uporabnik.ProfilnaSlika))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(uporabnik.ProfilnaSlika);
                bitmap.EndInit();
                imageProfilnaSlika.Source = bitmap;
            }
        }

        private void Shrani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonNalozi_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
            openFileDialog.Title = "Select Photos";

            if (openFileDialog.ShowDialog() == true)
            {
                uporabnik.ProfilnaSlika = openFileDialog.FileName;
            }

            PosodobiGUI();
        }

        private void TextBoxes_KeyUp(object sender, KeyEventArgs e)
        {
            uporabnik.Ime = TextBox_Ime.Text;
            uporabnik.Priimek = TextBox_Priimek.Text;
            uporabnik.RojstniKraj = TextBox_Rojstni_Kraj.Text;
        }

        internal Uporabnik VrniPrijatelja()
        {
            return uporabnik;
        }
    }
}