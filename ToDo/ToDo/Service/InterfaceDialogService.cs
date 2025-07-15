using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Service
{
    public interface InterfaceDialogService
    {
        void ShowMessage(string message, string title = "!!!");
        bool ShowYesNo(string message, string title = "???");
    }
}
