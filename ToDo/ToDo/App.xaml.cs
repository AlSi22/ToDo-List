using System.Configuration;
using System.Data;
using System.Windows;
using ToDo.Model;
using ToDo.Service;
using ToDo.ViewModel.Aufgaben;
using ToDo.ViewModel.Main;


namespace ToDo
{

    public partial class App : Application
    {
        private MainViewModel _hauptViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var dialogService = new DialogService();
            var jsonService = new JsonService(dialogService);

            var (hauptListe, AlexListe, VerenaListe, alexPunkte, verenaPunkte, winner) = jsonService.Laden();

            
            _hauptViewModel = new MainViewModel(hauptListe, AlexListe, VerenaListe, alexPunkte, verenaPunkte, winner, jsonService, dialogService);

            // Abgleich mit Aufgaben-Liste
            _hauptViewModel.CheckAufgabenliste();
            _hauptViewModel.Aktualisiere();

            var mainWindow = new MainWindow(_hauptViewModel);
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // Nur über MainViewModel speichern
            _hauptViewModel.Speichern();
        }
    }
}
