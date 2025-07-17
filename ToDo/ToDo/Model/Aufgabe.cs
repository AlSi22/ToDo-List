using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    // Aufgabe.cs
    
    // - Art: Bezeichnung oder Typ der Aufgabe (z. B. "Wäsche waschen", "Einkaufen")
    // - Tage: Rhythmus der wiederkehrenden Aufgaben
    // - Punkte: Belohnung für das Erledigen der Aufgaben
    // - AufgabeHinzugefügt: Datum, wann die Aufgabe hinzugefügt wurde (automatisch generiert)
    public class Aufgabe
    {
        public string Art { get; set; }
        public int Tage { get; set; }
        public int Punkte { get; set; }

        public DateTime? AufgabeHinzugefügt { get; set; }
    }
}
