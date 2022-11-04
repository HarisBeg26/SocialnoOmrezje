using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Naloga_1
{
    public class Objava : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
     
        private string vsebina;
        private string fotografija;
        private string lokacija;
        private string povezava;
        private string obcutek;
        public List<Uporabnik> OznaceniPrijatelji;

        public string Vsebina
        {
            get => vsebina;
            set
            {
                vsebina = value;
                OnPropertyChanged();
            }
        }

        public string Fotografija
        {
            get => fotografija;
            set
            { 
                fotografija = value;
                OnPropertyChanged();
            }
        }

        public string Lokacija
        {
            get => lokacija;
            set => lokacija = value;
        }
        public string Povezava
        {
            get => povezava;
            set
            {
                povezava = value;
                OnPropertyChanged();
            }
        }
        public string Obcutek
        {
            get => obcutek;
            set
            {
                obcutek = value;
                OnPropertyChanged();
            }
        }

        /*[NonSerialized]
        public List<Uporabnik> OznaceniPrijatelji
        {
            get => oznaceniPrijatelji;
        }*/

        public string OznaceniPrijateljiText
        {
            get => OznaceniPrijateljiToString();
        }

        private string OznaceniPrijateljiToString()
        {
            string text = "";
            for (int i = 0; i < OznaceniPrijatelji.Count; i++)
            {
                text += OznaceniPrijatelji[i].ImePriimek + ", ";
            }

            return text;
        }

        public Objava()
        {
            OznaceniPrijatelji = new List<Uporabnik>();
        }

        public Objava(string vsebina)
        {
            OznaceniPrijatelji = new List<Uporabnik>();
            this.vsebina = vsebina;
        }

        public Objava(string vsebina, string lokacija, string povezava, string obcutek)
        {
            OznaceniPrijatelji = new List<Uporabnik>();
            this.vsebina = vsebina;
            this.lokacija = lokacija;
            this.povezava = povezava;
            this.obcutek = obcutek;
        }

        public Objava(string vsebina, string fotografija, string lokacija, string povezava, string obcutek)
        {
            OznaceniPrijatelji = new List<Uporabnik>();
            this.vsebina = vsebina;
            this.fotografija = fotografija;
            this.lokacija = lokacija;
            this.povezava = povezava;
            this.obcutek = obcutek;
        }

        public void OznaciPrijatelja(Uporabnik prijatelj)
        {
            OznaceniPrijatelji.Add(prijatelj);
        }

        public void OdznaciPrijatelja(Uporabnik prijatelj)
        {
            OznaceniPrijatelji.Remove(prijatelj);
        }

        public bool IsTagged()
        {
            if (OznaceniPrijatelji.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
