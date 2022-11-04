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
using System.Windows.Shapes;

namespace Naloga_1
{
    /// <summary>
    /// Interaction logic for UrediPrijatelja.xaml
    /// </summary>
    public partial class UrediPrijatelje : Window
    {
        private ObservableCollection<Uporabnik> seznamPrijateljev;

        public UrediPrijatelje(ObservableCollection<Uporabnik> sp)
        {
            InitializeComponent();
            
            seznamPrijateljev = sp;
            ListView_SeznamPrijateljev.ItemsSource = seznamPrijateljev;
            PosodobiGUI();
        }

        private void PosodobiGUI()
        {
            ListView_SeznamPrijateljev.Items.Refresh();
        }

        private void Shrani(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_OdstraniPrijatelja_Click(object sender, RoutedEventArgs e)
        {
            seznamPrijateljev.RemoveAt(ListView_SeznamPrijateljev.SelectedIndex);
            PosodobiGUI();
        }

      
        private void MenuItem_UrediPrijatelja_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_SeznamPrijateljev.SelectedItem != null)
            {
                Uporabnik prijatelj = seznamPrijateljev[ListView_SeznamPrijateljev.SelectedIndex];
                PrijateljOkno okno = new PrijateljOkno(prijatelj);
                okno.Title = "Podatki o prijatelju";
                okno.ShowDialog();

                PosodobiGUI();
            }
            else
            {
                MessageBox.Show("Označi prijatelja.");
            }
        }

        private void buttonDodajPrijatelja_Click(object sender, RoutedEventArgs e)
        {
            PrijateljOkno okno = new PrijateljOkno();
            okno.Title = "Dodaj prijatelja";
            okno.ShowDialog();

            seznamPrijateljev.Add(okno.VrniPrijatelja());

            PosodobiGUI();
        }
    }
}
