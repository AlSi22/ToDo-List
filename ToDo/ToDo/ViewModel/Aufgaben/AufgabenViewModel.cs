using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDo.Model;
using ToDo.Service;
using ToDo.View.Alex;
using ToDo.View.Verena;
using ToDo.ViewModel.Main;

namespace ToDo.ViewModel.Aufgaben
{
   public class AufgabenViewModel : INotifyPropertyChanged
    {
        private readonly InterfaceJsonService _jsonService;
        private readonly InterfaceDialogService _dialogService;
        public ObservableCollection<Aufgabe> Aufgabenliste {  get; set; }

        public string Art { get; set; }
        public string Tage { get; set; }
        public string Punkte { get; set; }

     

        // Laden
        public AufgabenViewModel(InterfaceJsonService jsonService, InterfaceDialogService dialogService)
        {
                       
            _jsonService = jsonService;
            _dialogService = dialogService; 
            Aufgabenliste = _jsonService.LadenAufgaben();
        }
      

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      public void CheckHauptliste (ObservableCollection<Eintrag> hauptliste,
                                    ObservableCollection<Eintrag> alexliste,
                                    ObservableCollection<Eintrag> verenaliste)
      {
            DateTime heute = DateTime.Today;

            foreach (var aufgabe in Aufgabenliste)
            {
                // wenn kein Datum bei Aufgabe hinterlegt, dann nimm Minimum
                DateTime aufgabeHinzugefügt = aufgabe.AufgabeHinzugefügt ?? DateTime.MinValue;
                int tageszähler = (heute - aufgabeHinzugefügt).Days;

                if (tageszähler >= aufgabe.Tage)
                {
                    // Abgleich zwischen Elemente auf Hauptliste und Aufgabentabelle
                    bool istSchonDa = false;

                    foreach (var job in hauptliste)
                    {
                        if (job.Text == aufgabe.Art)
                        {
                            istSchonDa = true;
                            break; 
                        }
                    }

                    // Abgleich mit Alexlist
                    if (!istSchonDa)
                    {
                        foreach (var job in alexliste)
                        {
                            if (job.Text == aufgabe.Art)
                            {
                                istSchonDa = true;
                                break;
                            }
                        }
                    }

                    // Abgleich mit Verenaliste

                    if (!istSchonDa)
                    {
                        foreach (var job in verenaliste)
                        {
                            if (job.Text == aufgabe.Art)
                            {
                                istSchonDa = true;
                                break;
                            }
                        }
                    }

                    // Wenn nicht da, Aufgabe hinzufügen

                    if (!istSchonDa)
                    {
                        var neuerEintrag = new Eintrag
                        {
                            Text = aufgabe.Art,

                        };


                        hauptliste.Add(neuerEintrag);


                        aufgabe.AufgabeHinzugefügt = heute;
                    }

                }
            }

            Speichern();
      }

                                                                                                // Buttons

        // 2x hinzufügen funktioniert aufgrund der unterschiedlichen Typen
        public void Hinzufügen(string art, string tageText, string punkteText)
        {
            int tage = 365; // standardwert 1x im Jahr muss die Aufgabe gemacht werden

            if (!string.IsNullOrWhiteSpace(tageText))
            {
                if (!int.TryParse(tageText, out tage))
                {
                    _dialogService.ShowMessage("Bitte gültige Zahl bei 'Tage' eingeben.");
                    return;
                }
                if (tage < 1)
                {
                    _dialogService.ShowMessage("Bitte gib bei 'Tage' eine Zahl größer oder gleich 1 ein. 1 = täglich, 2 = alle 2 Tage …");
                    return;
                }
            }

            if (!int.TryParse(punkteText, out int punkte))
            {
                _dialogService.ShowMessage("Bei Punkte bitte eine Zahl eingeben.");
                return;
            }

            Hinzufügen(art, tage, punkte); 
        }

        public void Hinzufügen(string art, int tage, int punkte)
        {
            Aufgabenliste.Add(new Aufgabe { Art = art, Tage = tage, Punkte = punkte, AufgabeHinzugefügt = DateTime.Today });

            Art = string.Empty;
            Tage = string.Empty;
            Punkte = string.Empty;

            OnPropertyChanged(nameof(Art));
            OnPropertyChanged(nameof(Tage));
            OnPropertyChanged(nameof(Punkte));

        }
        public void Entfernen(Aufgabe aufgabe)
        {
            Aufgabenliste.Remove(aufgabe);
            OnPropertyChanged(nameof(AlexListe));
        }


        // Speichern
        public void Speichern()
        {
            _jsonService.SpeichernAufgaben(Aufgabenliste);
        }
    }
}
