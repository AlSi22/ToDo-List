using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDo.Model;
using ToDo.View;
using ToDo.ViewModel.Alex;
using ToDo.ViewModel.Main;

namespace ToDo.View.Alex
{
/*
    AlexListe.xaml.cs
    
    Funktionen:
    - Konstruktor übernimmt ein ViewModel (AlexViewModel) zur Datenbindung
    - Button_Click-Methode: Wenn Button "EntfernAlex" geklickt wird und ein Eintrag ausgewählt ist,
      ruft sie die Entfernen-Methode im ViewModel auf.
    - Typprüfung zur Sicherheit eingebaut
*/
    public partial class AlexListe : Window
    {
        
        public AlexListe(AlexViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var alexviewModel = DataContext as AlexViewModel;
            if (alexviewModel == null || sender is not Button button) return;
            
            switch (button.Name)
            {
                case "EntfernAlex":
                    var selected = AlexListBox.SelectedItem as Eintrag;
                    if (selected != null)
                    {
                        alexviewModel.Entfernen(selected);
                    }
                    break;

            }
        }
    }
}
