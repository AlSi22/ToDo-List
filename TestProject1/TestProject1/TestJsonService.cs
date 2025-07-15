using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ToDo.Model;
using ToDo.Service;

namespace TestProject1
{
    public class TestJsonService :InterfaceJsonService
    {
        public ObservableCollection<Aufgabe> Aufgaben { get; set;} = new();

     
        public void Speichern(ObservableCollection<Eintrag> main, ObservableCollection<Eintrag> alex,
                               ObservableCollection<Eintrag> verena, int alexPunkte, int verenaPunkte, string winner)
        {
            // Leer für Testzwecke
        }

        public (ObservableCollection<Eintrag>, ObservableCollection<Eintrag>, ObservableCollection<Eintrag>, int, int, string) Laden()
        {
            return (new ObservableCollection<Eintrag>(), new(), new(), 0, 0, "Test");
        }

        public void SpeichernAufgaben(ObservableCollection<Aufgabe> aufgaben) { }

        public ObservableCollection<Aufgabe> LadenAufgaben()
        {
            //return new ObservableCollection<Aufgabe>
        {
                //new Aufgabe { Art = "Testaufgabe", AufgabeHinzugefügt = DateTime.Today }
                return Aufgaben;
        };
        }
    }
}