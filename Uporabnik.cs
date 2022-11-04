using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Naloga_1
{
    public class Uporabnik : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string ime;
        private string priimek;
        private string rojstniKraj;
        private string profilnaSlika;

        public ObservableCollection<Objava> SeznamObjav;
        public ObservableCollection<Uporabnik> SeznamPrijateljev;
        
        public string Ime
        {
            get => ime;
            set
            {
                ime = value;
                OnPropertyChanged();
            }
        }

        public string Priimek
        {
            get => priimek;
            set 
            {
                priimek = value;
                OnPropertyChanged();
            }
        }

        public string ImePriimek
        {
            get => ime + " " + priimek;
        }

        public string RojstniKraj
        {
            get => rojstniKraj;
            set
            {
                rojstniKraj = value;
                OnPropertyChanged();
            }
        }

        public string ProfilnaSlika
        {
            get => profilnaSlika;
            set 
            {
                profilnaSlika = value;
                OnPropertyChanged();
            }
        }

        public Uporabnik()
        {
            SeznamObjav = new ObservableCollection<Objava>();
            SeznamPrijateljev = new ObservableCollection<Uporabnik>();
            profilnaSlika = "";
        }

        public Uporabnik(string ime, string priimek, string rojstniKraj, string profilnaSlika)
        {
            SeznamObjav = new ObservableCollection<Objava>();
            SeznamPrijateljev = new ObservableCollection<Uporabnik>();
            this.ime = ime;
            this.priimek = priimek;
            this.rojstniKraj = rojstniKraj;
            this.profilnaSlika = profilnaSlika;
        }

        public Uporabnik(string ime, string priimek, string rojstniKraj)
        {
            SeznamObjav = new ObservableCollection<Objava>();
            SeznamPrijateljev = new ObservableCollection<Uporabnik>();
            this.ime = ime;
            this.priimek = priimek;
            this.rojstniKraj = rojstniKraj;
            this.profilnaSlika = "";
        }
        
        public void DodajPrijatelja(Uporabnik uporabnik)
        {
            SeznamPrijateljev.Add(uporabnik);
        }

        public void OdstraniPrijatelja(Uporabnik uporabnik)
        {
            SeznamPrijateljev.Remove(uporabnik);
        }

        public void DodajObjavo(Objava objava)
        {
            SeznamObjav.Add(objava);
        }

        public void OdstraniObjavo(Objava objava)
        {
            SeznamObjav.Remove(objava);
        }
    }
}
