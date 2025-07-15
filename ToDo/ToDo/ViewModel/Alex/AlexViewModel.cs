using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Input;
using ToDo.Model;
using ToDo.ViewModel.Main;
using ToDo.ViewModel.Aufgaben;
using System.Windows;
using ToDo.Service;

namespace ToDo.ViewModel.Alex
{
    public class AlexViewModel : INotifyPropertyChanged
    {
        private readonly InterfaceDialogService _dialogService;
        private MainViewModel _hauptViewModel;

        public ObservableCollection<Eintrag> AlexListe { get; set; }
        
        // Punkte-Setup
        private int _alexpunkte;
        public int AlexPunkte
        {
            get => _alexpunkte;
            set
            {
                _alexpunkte= value; //interne speicherung
                OnPropertyChanged(nameof(AlexPunkte)); //ui benachrichtigen
            }
        }

        // _name = private Variable, Name = öffentliche Eigenschaft, name = lokaler Parameter
        private AufgabenViewModel _aufgabenViewModel;
        public AlexViewModel(ObservableCollection<Eintrag> daten, AufgabenViewModel aufgabenViewModel, MainViewModel hauptViewModel, InterfaceDialogService dialogService)
        {
            AlexListe = daten;
            _aufgabenViewModel = aufgabenViewModel;
            _hauptViewModel = hauptViewModel;
            _dialogService = dialogService;
        }

        public void Hinzufügen(Eintrag eintrag)
        {
            AlexListe.Add(eintrag);
            OnPropertyChanged(nameof(AlexListe));

            
        }

        public void Entfernen(Eintrag eintrag)
        {
            AlexListe.Remove(eintrag);
            OnPropertyChanged(nameof(AlexListe));


            // Abgleich mit Aufgabentabelle
            var aufgabeGefunden = _aufgabenViewModel.Aufgabenliste.FirstOrDefault(a => a.Art == eintrag.Text);

            if (aufgabeGefunden != null)
            {
                AlexPunkte += aufgabeGefunden.Punkte;


                _dialogService.ShowMessage($"{aufgabeGefunden.Punkte} Points for the good Guy");
            }

            _hauptViewModel.Aktualisiere();
        }


        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string name) =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        public void SpeichernPunkte()
        {

        }

    }
}