using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    public class Aufgabe
    {
        public string Art { get; set; }
        public int Tage { get; set; }
        public int Punkte { get; set; }

        public DateTime? AufgabeHinzugefügt { get; set; }
    }
}
