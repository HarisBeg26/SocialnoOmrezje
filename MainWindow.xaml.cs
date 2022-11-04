using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;


namespace Naloga_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public Uporabnik uporabnik;
        List<Uporabnik> seznamUporabnikov;
        ObservableCollection<Objava> seznamIskanihObjav;

        Uporabnik trenutniUporabnik;
        public Objava objava = new Objava();

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimerAutoSave;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
            // Log in
            trenutniUporabnik = UserControlOMeni.TrenutniUporabnik;

            // Backend - Admin del - Initiate
            seznamUporabnikov = new List<Uporabnik>();
            seznamIskanihObjav = new ObservableCollection<Objava>();


            Uporabnik jernej = new Uporabnik("Jernej", "Peklar", "Hrastnik");
            Uporabnik klemen = new Uporabnik("Klemen", "Ključevšek", "Maribor");
            Uporabnik ognjen = new Uporabnik("Ognjen", "Borovic", "Banja Luka");
            seznamUporabnikov.Add(trenutniUporabnik); //haris
            seznamUporabnikov.Add(jernej);
            seznamUporabnikov.Add(klemen);
            seznamUporabnikov.Add(ognjen);

            trenutniUporabnik.SeznamObjav.Add(new Objava("Danes je lep sončen dan! NOT!", @"E:\Uporabniški vmesniki\Vaje\Naloga 1,2\Images\Kanye_West.png", "Maribor", "https://estudij.um.si", "Vesel!"));
            trenutniUporabnik.SeznamObjav.Add(new Objava("Kanye West New Album is OUT!", @"E:\Uporabniški vmesniki\Vaje\Naloga 1,2\Images\Kanye_West.png", "Maribor", "https://estudij.um.si", "Vesel!"));
            trenutniUporabnik.DodajPrijatelja(jernej);
            trenutniUporabnik.DodajPrijatelja(klemen);
            trenutniUporabnik.DodajPrijatelja(ognjen);

            jernej.DodajPrijatelja(trenutniUporabnik);
            jernej.DodajPrijatelja(ognjen);

            ListView_Objave.ItemsSource = trenutniUporabnik.SeznamObjav;
            ListView_Prijatelji.ItemsSource = trenutniUporabnik.SeznamPrijateljev;

            LoadXML("objave.xml");
            PosodobiGUI();
        }

        private void PosodobiGUI()
        {
            ListView_Objave.Items.Refresh();
            ListView_Prijatelji.Items.Refresh();
        }

        public void DodajPrijatelja(Uporabnik uporabnik)
        {
            seznamUporabnikov.Add(uporabnik);
            PosodobiGUI();
        }

     

        private void MenuItem_DodajObjavo_Click(object sender, RoutedEventArgs e)
        {
            ObjavaOkno dodajOkno = new ObjavaOkno(trenutniUporabnik);
            dodajOkno.Title = "Dodaj objavo";
            dodajOkno.ShowDialog();

            trenutniUporabnik.SeznamObjav.Add(dodajOkno.VrniObjavo());
            PosodobiGUI();
        }

        private void MenuItem_OdstraniObjavo_Click(object sender, RoutedEventArgs e)
        {
            trenutniUporabnik.SeznamObjav.RemoveAt(ListView_Objave.SelectedIndex);
        }

        private void MenuItem_UrediObjavo_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_Objave.SelectedItem != null)
            {
                Objava objava = trenutniUporabnik.SeznamObjav[ListView_Objave.SelectedIndex];
                ObjavaOkno urediOkno = new ObjavaOkno(objava, trenutniUporabnik);
                urediOkno.ShowDialog();

                PosodobiGUI();
            }
            else
            {
                MessageBox.Show("Izberite objavo ki jo želite urediti.");
            }
        }

        private void MenuItem_Izvozi_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fDialog = new SaveFileDialog();
            fDialog.Filter = "Text File|*.xml";
            fDialog.ShowDialog();

            Izvozi(fDialog.FileName);
        }

        private void Izvozi(string path)
        {
            XmlAttributeOverrides overrides = new XmlAttributeOverrides();
            XmlAttributes attribs = new XmlAttributes();
            attribs.XmlIgnore = true;
            attribs.XmlElements.Add(new XmlElementAttribute("SeznamPrijateljev"));
            overrides.Add(typeof(Uporabnik), "SeznamPrijateljev", attribs);
            attribs.XmlElements.Add(new XmlElementAttribute("SeznamObjav"));
            overrides.Add(typeof(Uporabnik), "SeznamObjav", attribs);


            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Objava>), overrides);
            if (path != "")
            {
                TextWriter file = new StreamWriter(path);
                serializer.Serialize(file, trenutniUporabnik.SeznamObjav);
                file.Close();
            }
        }

        private void MenuItem_Uvozi_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Filter = "All Files|*.xml;";
            fDialog.ShowDialog();

            LoadXML(fDialog.FileName);
        }

        private void LoadXML(string path)
        {
            if (File.Exists(path))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Objava>));

                TextReader file = new StreamReader(path);
                trenutniUporabnik.SeznamObjav = (ObservableCollection<Objava>)deserializer.Deserialize(file);
                file.Close();

                ListView_Objave.ItemsSource = trenutniUporabnik.SeznamObjav;
                PosodobiGUI();
            }
        }

        private void UrediPrijatelje_Click(object sender, RoutedEventArgs e)
        {
            UrediPrijatelje okno = new UrediPrijatelje(trenutniUporabnik.SeznamPrijateljev);
            okno.Title = "Uredi prijatelje";
            okno.ShowDialog();

            PosodobiGUI();
        }

        private void MenuItem_IzhodClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_OdstraniObjavo2_Click(object sender, RoutedEventArgs e)
        {
            trenutniUporabnik.SeznamObjav.RemoveAt(ListView_Objave.SelectedIndex);
        }

        private void MenuItem_UrediObjavo2_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_Objave.SelectedItem != null)
            {
                Objava objava = trenutniUporabnik.SeznamObjav[ListView_Objave.SelectedIndex];
                ObjavaOkno urediOkno = new ObjavaOkno(objava, trenutniUporabnik);
                urediOkno.ShowDialog();

                PosodobiGUI();
            }
            else
            {
                MessageBox.Show("Izberite objavo ki jo želite urediti.");
            }
        }

        private void MenuItem_DodajObjavo2_Click(object sender, RoutedEventArgs e)
        {
            ObjavaOkno dodajOkno = new ObjavaOkno(trenutniUporabnik);
            dodajOkno.Title = "Dodaj objavo";
            dodajOkno.ShowDialog();

            trenutniUporabnik.SeznamObjav.Add(dodajOkno.VrniObjavo());
            PosodobiGUI();
        }

        

        private void MenuItem_Notifikacije_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Imaš 2 neprebrane objave");
        }

        private void TextBox_SearchBar_KeyUp(object sender, KeyEventArgs e)
        {
            IsciObjave(TextBox_SearchBar.Text);
        }

        private void IsciObjave(string iskanTekst)
        {
            if (iskanTekst == "")
            {
                ListView_Objave.ItemsSource = trenutniUporabnik.SeznamObjav;
            }
            else
            {
                seznamIskanihObjav.Clear();
                string delVsebine;

                foreach (var objava in trenutniUporabnik.SeznamObjav)
                {
                    iskanTekst = iskanTekst.ToLower();

                    for (int i = 0; i < objava.Vsebina.Length - iskanTekst.Length; i++)
                    {
                        delVsebine = objava.Vsebina.ToLower().Substring(i, iskanTekst.Length);

                        if (iskanTekst.CompareTo(delVsebine) == 0)
                        {
                            seznamIskanihObjav.Add(objava);
                            break;
                        }
                    }
                }

                ListView_Objave.ItemsSource = seznamIskanihObjav;
            }
        }

        private void DispatcherTimerAutoSave(object sender, EventArgs e)
        {
            Izvozi("autosave.xml");
        }

    }
}