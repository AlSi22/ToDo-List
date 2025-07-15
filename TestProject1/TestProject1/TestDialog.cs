using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ToDo.Service;

namespace TestProject1
{
    public class TestDialog : InterfaceDialogService
    {
       
        public bool ShowYesNo (string message, string title = "???")
        {
            
            return true;
        }

        public void ShowMessage(string message, string title = "!!!")
        {
        }
    }
}
