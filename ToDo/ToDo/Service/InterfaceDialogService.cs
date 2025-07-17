using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Service
{
    /*   InterfaceDialogService.cs
    
        Schnittstelle zur Anzeige von Dialogen in der Anwendung
    
   
          Methoden:
          ShowMessage: Zeigt eine einfache Informationsmeldung.
          ShowYesNo: Zeigt eine Ja/Nein-Abfrage und gibt die Benutzerantwort zurück.
    */
    public interface InterfaceDialogService
    {
        void ShowMessage(string message, string title = "!!!");
        bool ShowYesNo(string message, string title = "???");
    }
}
