using System.Printing;
using System.Text;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDo.Model;
using ToDo.Service;
using ToDo.ViewModel.Main;
using ToDo.View.Alex;
using ToDo.View.Verena;

namespace ToDo
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
           
            DataContext = viewModel;
            // ohne Datacontext kein Binding

            this.Closing += Fenster_Schließen;


        }

        private void Fenster_Schließen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show(
                           "Schon fertig?",
                           "Ende",
                           MessageBoxButton.YesNo,
                           MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                e.Cancel = true; // Schließen abbrechen
                return;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (viewModel == null || sender is not Button button) return;

            //string input = InputBox.Text;

            switch (button.Name)
            {
                case "Hinzufügen":
                    viewModel.Hinzufügen();
                    break;

                case "VerenaJob":
                    viewModel.VerenaJob();
                    break;

                case "VerenaListe":
                    viewModel.ÖffneVerenaListe();
                    break;

                case "AlexJob":
                    viewModel.AlexJob();
                    break;

                case "AlexListe":
                    viewModel.ÖffneAlexListe();
                    break;

                case "Oben":
                    viewModel.ObenVerschieben();
                    break;

                case "Unten":
                    viewModel.UntenVerschieben();
                    break;

                case "Aufgaben":
                    viewModel.ÖffneAufgabenliste();
                    break;

                case "Auswertung":
                    viewModel.Auswertung();
                    break;
            }
        }

        private void InputBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
