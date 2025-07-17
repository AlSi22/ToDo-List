using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Service
{
    /* DialogService.cs
  
       Möglichkeit zur Anzeige von Dialogen
   
       Implementiert das Interface `InterfaceDialogService`
       So kann das UI vom ViewModel aus angesprochen werden, ohne direkt auf MessageBox zuzugreifen. 
       Würde zu Problemen beim Unit-Test führen
    
       Methoden:
            ShowMessage: Zeigt einen einfachen Hinweis-Dialog.
            ShowYesNo: Zeigt einen Ja/Nein-Dialog und gibt das Ergebnis zurück.
    */
    public class DialogService : InterfaceDialogService
    {
        public void ShowMessage (string message, string title = "!!!")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool ShowYesNo (string message, string title = "???")
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question); 
            return result == MessageBoxResult.Yes;  
        }
    }
}
