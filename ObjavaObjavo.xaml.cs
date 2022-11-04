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
    /// Interaction logic for DodajObjavo.xaml
    /// </summary>
    public partial class ObjavaOkno : Window
    {
        private Objava objava;
        private Uporabnik trenutniUporabnik;

        public ObjavaOkno(Uporabnik u)
        {
            InitializeComponent();
            objava = new Objava();
            trenutniUporabnik = u;
            NastaviGUI();
        }

        public ObjavaOkno(Objava o, Uporabnik u)
        {
            InitializeComponent();
            objava = o;
            trenutniUporabnik = u;
            NastaviGUI();
            PosodobiGUI();
        }

        private void NastaviGUI()
        {
            comboboxPrijatelji.ItemsSource = trenutniUporabnik.SeznamPrijateljev;
            comboboxPrijatelji.Text = "Izberite";
            listviewOznaceniPrijatelji.ItemsSource = objava.OznaceniPrijatelji;
        }

        private void PosodobiGUI()
        {
            TextBox_Vsebina.Text = objava.Vsebina;
            TextBox_Lokacija.Text = objava.Lokacija;
            TextBox_Povezava.Text = objava.Povezava;
            TextBox_Obcutek.Text = objava.Obcutek;

            if (File.Exists(objava.Fotografija))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(objava.Fotografija);
                bitmap.EndInit();
                Image_PotDoSlike.Source = bitmap;
            }

            listviewOznaceniPrijatelji.Items.Refresh();
        }

        private void Nalozi(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
            openFileDialog.Title = "Select Photos";

            if (openFileDialog.ShowDialog() == true)
            {
                objava.Fotografija = openFileDialog.FileName;
            }

            PosodobiGUI();
        }

        private void Button_ShraniSpremembe_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBoxes_KeyUp(object sender, KeyEventArgs e)
        {
            objava.Vsebina = TextBox_Vsebina.Text;
            objava.Lokacija = TextBox_Lokacija.Text;
            objava.Povezava = TextBox_Povezava.Text;
            objava.Obcutek = TextBox_Obcutek.Text;
        }

        internal Objava VrniObjavo()
        {
            return objava;
        }

        private void Image_PotDoSlike_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
            openFileDialog.Title = "Select Photos";

            if (openFileDialog.ShowDialog() == true)
            {
                objava.Fotografija = openFileDialog.FileName;
            }

            PosodobiGUI();
        }

        private void comboboxPrijatelji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            objava.OznaciPrijatelja(trenutniUporabnik.SeznamPrijateljev[comboboxPrijatelji.SelectedIndex]);
            PosodobiGUI();
            //objava.OznaciPrijatelja((Uporabnik)comboboxPrijatelji.SelectedItem);
        }
    }
}
