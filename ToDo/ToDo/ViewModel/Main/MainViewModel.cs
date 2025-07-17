using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
using ToDo.ViewModel.Verena;
using ToDo.ViewModel.Alex;
using ToDo.View.Verena;
using ToDo.View.Alex;
using ToDo.View.Aufgabenliste;
using ToDo.ViewModel.Aufgaben;

namespace ToDo.ViewModel.Main
{
    // Ort der Logik
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly InterfaceJsonService _jsonService;
        private readonly InterfaceDialogService _dialogService;
        public ObservableCollection<Eintrag> Liste { get; set; } = new();
        // Abkürzung von public ObservableCollection<string> StackListe { get; set; } = new ObservableCollection<string>();


        private string _eingabeText;
        public string EingabeText
        {
            get => _eingabeText;
            set
            {
                if (_eingabeText != value)
                {
                    // sobald was in der Combobox eingegeben wurde, mach Vorschläge
                    _eingabeText = value;
                    OnPropertyChanged(nameof(EingabeText));
                    AktualisiereVorschläge();
                }
            }
        }
        public ObservableCollection<string> Vorschläge { get; set; } = new();

        private void AktualisiereVorschläge()
        {
            // Alte Vorschläge löschen
            Vorschläge.Clear();

            if (string.IsNullOrWhiteSpace(EingabeText)) return;

            // Abgleich Eingabe mit Aufgabenliste
            var itsamatch = _aufgabenViewModel.Aufgabenliste
                            .Where(a => a.Art.Contains(EingabeText, StringComparison.OrdinalIgnoreCase))
                            .Select(a => a.Art)
                            // Mehrfache Treffer verhindern
                            .Distinct()
                            .ToList();

            // alle Treffer auf Liste hinzufügen
            foreach (var v in itsamatch)
                Vorschläge.Add(v);
        }
        // Wenn Vorschlag in DropdownMenu ausgewählt, dann übernehmen
        private string _ausgewählterVorschlag;
        public string AusgewählterVorschlag
        {
            get => _ausgewählterVorschlag;
            set
            {
                if (_ausgewählterVorschlag != value)
                {
                    _ausgewählterVorschlag = value;

                    _eingabeText = value;
                    OnPropertyChanged(nameof(AusgewählterVorschlag));
                    OnPropertyChanged(nameof(EingabeText));
                }
            }
        }

        private VerenaViewModel _verenaViewModel;
        private AlexViewModel _alexViewModel;
        private AufgabenViewModel _aufgabenViewModel;

        public MainViewModel(ObservableCollection<Eintrag> mainDaten, ObservableCollection<Eintrag> verenaDaten, ObservableCollection<Eintrag> alexDaten,
                            int alexPunkte, int verenaPunkte, string winner, InterfaceJsonService jsonService, InterfaceDialogService dialogService)
        {
            Liste = mainDaten;
            _jsonService = jsonService;
            _dialogService = dialogService;

            _aufgabenViewModel = new AufgabenViewModel(_jsonService, _dialogService);

            // this ist das gerade aktive Objekt selbst
            _verenaViewModel = new VerenaViewModel(verenaDaten, _aufgabenViewModel, this, new DialogService())
            {
                VerenaPunkte = verenaPunkte
            };
            _alexViewModel = new AlexViewModel(alexDaten, _aufgabenViewModel, this, new DialogService())
            {
                AlexPunkte = alexPunkte
            };

            Winner = winner;
        }

        public MainViewModel(ObservableCollection<Eintrag> mainDaten, InterfaceJsonService jsonService, InterfaceDialogService dialogService)
        {
            Liste = mainDaten;
            _jsonService = jsonService;
            _dialogService = dialogService;


            Liste.CollectionChanged += (_, _) => Aktualisiere();
            // CollectionChanged += Nur auf Änderungen reagieren
            // (_, _) =>  Nur Methode ausführen, keine Event-Daten
            // Eventdaten add, remove, replace, move, set, man könnte z.b. Statistiken führen oder Undo-Button

            _aufgabenViewModel = new AufgabenViewModel(_jsonService, _dialogService);
        }

        private string _listenÜberschrift = "Superduper wichtig";

        // _ Konvention, um private Felder von gleichnamigen zu unterscheiden
        // Diese Felder speichern den tatsächlichen Wert und werden von der Property genutzt, um bei Änderung das UI zu benachrichtigen.
        public string ListenÜberschrift
        {
            get => _listenÜberschrift;
            set
            {
                _listenÜberschrift = value;
                // speichert die alte Überschrift

                OnPropertyChanged(nameof(ListenÜberschrift));
                // UI bei Änderung aktualisieren
            }
        }



        private Eintrag? _selectedListItem;
        public Eintrag? SelectedListItem
        {
            get => _selectedListItem;
            set
            {
                _selectedListItem = value;
                OnPropertyChanged(nameof(SelectedListItem));
            }
        }

        public void Aktualisiere()
        {
            //Überschrift
            if (Liste.Count == 0)
            {
                ListenÜberschrift = $"Sehr vorbildlich, hier ein Eis ";
            }
            else if (Liste.Count < 5)
            {
                ListenÜberschrift = $"{Liste.Count} Dinge! Wäre nett, die zu erledigen ";
            }
            else
            {
                ListenÜberschrift = $"Komm mal in die Pötte! {Liste.Count}!!!";
            }
            //Punktestand
            OnPropertyChanged(nameof(Punktestand));
            OnPropertyChanged(nameof(Winner));
        }

        public string Punktestand => $" Alex {_alexViewModel.AlexPunkte} vs Verena {_verenaViewModel.VerenaPunkte}";

        private string _winner;
        public string Winner
        {
            get => _winner;
            set
            {
                _winner = value;
                OnPropertyChanged(nameof(Winner));
            }
        }
        // Abgleich ob ELement auf Liste soll
        public void CheckAufgabenliste()
        {
            _aufgabenViewModel.CheckHauptliste(Liste, _alexViewModel.AlexListe, _verenaViewModel.VerenaListe);
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        // Standard -Event, aus dem Interface INotifyPropertyChanged. 
        // Sonst bleibt die UI statisch

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        // Benachrichtigt alle UI-Elemente (Bindings), dass sich eine Eigenschaft geändert hat.



        //              Buttonlogik

        public void Hinzufügen()
        {
            if (!string.IsNullOrWhiteSpace(EingabeText))
            {
                Liste.Add(new Eintrag { Text = EingabeText });
                EingabeText = " ";
                //inputBox.Focus();
                Aktualisiere();
            }
        }

        // wer übernimmt die Aufgabe
        public void VerenaJob()
        {
            if (SelectedListItem != null)
            {
                bool bestätigung = _dialogService.ShowYesNo
                                                             ("Soll das Verena machen?",
                                                                "Logisch"
                                                             );


                if (bestätigung)
                    // wenn bestätigt, Element verschieben und von Mainliste löschen
                {
                    _verenaViewModel.Hinzufügen(SelectedListItem);
                    Liste.Remove(SelectedListItem);
                    Aktualisiere();
                }
            }
        }

        public void AlexJob()
        {
            if (SelectedListItem != null)
            {
                bool bestätigung = _dialogService.ShowYesNo
                                                             ("Soll das Alex machen?",
                                                             "Auf keinen Fall");


                if (bestätigung)

                {
                    _alexViewModel.Hinzufügen(SelectedListItem);
                    Liste.Remove(SelectedListItem);
                    Aktualisiere();
                }
            }
        }

        // Liste Items verschieben
        public void ObenVerschieben()
        {
            var item = SelectedListItem;
            int index = Liste.IndexOf(item);
            if (index > 0)
                Liste.Move(index, index - 1);
        }

        public void UntenVerschieben()
        {
            var item = SelectedListItem;
            int index = Liste.IndexOf(item);
            if (index >= 0 && index < Liste.Count - 1)
                Liste.Move(index, index + 1);
        }
        private VerenaListe _verenafenster;

        // Öffnen der anderen Fenster
        public void ÖffneVerenaListe()
        {
            // null = noch nie göffnet, loaded ist geöffenet
            if (_verenafenster == null || !_verenafenster.IsLoaded)
            {
                _verenafenster = new VerenaListe(_verenaViewModel)

                {
                    DataContext = _verenaViewModel
                };
                //wenn geschlossen, setze fenster auf null. s sender (fenster), e eventargs(hier irrelevant)
                _verenafenster.Closed += (s, e) => _verenafenster = null;
                _verenafenster.Show();
            }
            else
            {
                _verenafenster.Activate(); //Fenster in Vordergrund
            }
        }


        private AlexListe _alexfenster;
        public void ÖffneAlexListe()
        {
            // null = noch nie göffnet, loaded ist geöffenet
            if (_alexfenster == null || !_alexfenster.IsLoaded)
            {
                _alexfenster = new AlexListe(_alexViewModel)

                {
                    DataContext = _alexViewModel
                };
                //wenn geschlossen, setze fenster auf null. s sender (fenster), e eventargs(hier irrelevant)
                _alexfenster.Closed += (s, e) => _alexfenster = null;
                _alexfenster.Show();
            }
            else
            {
                _alexfenster.Activate();//Fenster in Vordergrund
            }
        }

        private Aufgabenliste _aufgabenfenster;
        public void ÖffneAufgabenliste()
        {
            // null = noch nie göffnet, loaded ist geöffenet
            if (_aufgabenfenster == null || !_aufgabenfenster.IsLoaded)
            {
                _aufgabenfenster = new Aufgabenliste(_aufgabenViewModel)
                {
                    DataContext = _aufgabenViewModel
                };
                //wenn geschlossen, setze fenster auf null. s sender (fenster), e eventargs(hier irrelevant)
                _aufgabenfenster.Closed += (s, e) => _aufgabenfenster = null;
                _aufgabenfenster.Show();
            }
            else
            {
                _aufgabenfenster.Activate();//Fenster in Vordergrund
            }
        }

        // Winner Winner ...
                public void Auswertung()
                {
                    bool bestätigung = _dialogService.ShowYesNo
                                        ("Soll der Sieger gekürt werden?",
                                        "Ist Alex vorne?");

                    if (bestätigung)
                    {
                        if (_alexViewModel.AlexPunkte > _verenaViewModel.VerenaPunkte)
                        {
                            _dialogService.ShowMessage(
                                                        "Alex hat gewonnen"
                                                      );
                            Winner = "Der letzte Sieger ist Alex";
                        }
                        else if (_alexViewModel.AlexPunkte == _verenaViewModel.VerenaPunkte)

                        {
                            _dialogService.ShowMessage(
                                                        "Unentschieden"
                                                      );
                            Winner = "Das letzte Mal hat Niemand gewonnen";
                        }
                        else
                        {
                            _dialogService.ShowMessage(
                                                        "Verena hat gewonnen"
                                                      );
                            Winner = "Wow, Siegerin Verena";
                        }
                        
                        // Rücksetzung der Punkte

                        _alexViewModel.AlexPunkte = 0;
                        _verenaViewModel.VerenaPunkte = 0;
                        Aktualisiere();
                        Speichern();
                    }
                }


        // Speichern
        public void Speichern()
        {
            _jsonService.Speichern(  Liste, 
                                    _alexViewModel.AlexListe, 
                                    _verenaViewModel.VerenaListe, 
                                    _alexViewModel.AlexPunkte, 
                                    _verenaViewModel.VerenaPunkte,
                                    Winner);

        }
    }
}
