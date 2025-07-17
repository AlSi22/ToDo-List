using System;
using System.Collections.Generic;
using System.Linq;
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
using ToDo.ViewModel.Alex;
using ToDo.ViewModel.Aufgaben;
using ToDo.ViewModel.Main;

namespace ToDo.View.Aufgabenliste
{
    /// <summary>
    /// Aufgabenliste.xaml".
    /// Dient der Verwaltung wiederkehrender Aufgaben.
    /// 
    /// Funktionen:
    /// - Übergibt das AufgabenViewModel als DataContext
    /// - Reagiert auf Button-Klicks (Hinzufügen & Entfernen)
    /// - Speichert beim Schließen automatisch alle Aufgaben
    /// </summary>
    public partial class Aufgabenliste : Window
    {
        private readonly AufgabenViewModel _viewModel;
        public Aufgabenliste(AufgabenViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _viewModel.Speichern();  // speichert automatisch beim Schließen
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var aufgabenviewModel = DataContext as AufgabenViewModel;
            if (aufgabenviewModel == null || sender is not Button button) return;

            switch (button.Name)
                {
                    case    "Hinzufügen":
                                            aufgabenviewModel.Hinzufügen(ArtTextBox.Text, 
                                            TageTextBox.Text, PunkteTextBox.Text); 
                    break;
                    

                    case    "Entfernen":
                                        var selected = AufgabenListbox.SelectedItem as Aufgabe;
                                        if (selected != null)
                                        {
                                            aufgabenviewModel.Entfernen(selected);
                                        }
                    break;
                     
                }
        }
    }
}


 
