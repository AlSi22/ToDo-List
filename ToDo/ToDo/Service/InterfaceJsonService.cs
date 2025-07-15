using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;

namespace ToDo.Service
{
    public interface InterfaceJsonService
    {
        void Speichern(ObservableCollection<Eintrag> main, ObservableCollection<Eintrag> alex,
                      ObservableCollection<Eintrag> verena, int alexPunkte, int verenaPunkte, string winner);

        (ObservableCollection<Eintrag> main, ObservableCollection<Eintrag> alex,
         ObservableCollection<Eintrag> verena, int alexPunkte, int verenaPunkte, string winner) Laden();

        void SpeichernAufgaben(ObservableCollection<Aufgabe> aufgaben);

        ObservableCollection<Aufgabe> LadenAufgaben();
    }
}
 