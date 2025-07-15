using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Service
{
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
