﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ToDo.Model;
using ToDo.View.Alex;
using ToDo.ViewModel.Aufgaben;
using ToDo.ViewModel.Main;
using ToDo.Service;

namespace ToDo.ViewModel.Verena
{
    /// <summary>
    /// VerenaViewModel.cs
    /// ViewModel für die Aufgabenliste von Verena
    /// Verwaltet eine ObservableCollection von Einträgen sowie Punktestand und Interaktionen mit dem Aufgabenmodell.
    /// Verantwortlich für das Hinzufügen und Entfernen von Einträgen, inklusive Punktevergabe.
    /// </summary>
    public class VerenaViewModel : INotifyPropertyChanged
    {
        private MainViewModel _hauptViewModel;
        private readonly InterfaceDialogService _dialogService; 

        public ObservableCollection<Eintrag> VerenaListe { get; set; }
       
        // Punkte-Setup
        private int _verenapunkte;
        public int VerenaPunkte
        {
            get => _verenapunkte;
            set
            {
                _verenapunkte = value; //interne speicherung
                OnPropertyChanged(nameof(VerenaPunkte)); //ui benachrichtigen
            }
        }

        private AufgabenViewModel _aufgabenViewModel;
        public VerenaViewModel(ObservableCollection<Eintrag> daten, AufgabenViewModel aufgabenViewModel, MainViewModel hauptViewModel, InterfaceDialogService dialogService)
        {
            VerenaListe = daten;
            _aufgabenViewModel = aufgabenViewModel;
            _hauptViewModel = hauptViewModel;
            _dialogService = dialogService; 
        }

        public void Hinzufügen(Eintrag eintrag)
        {
            VerenaListe.Add(eintrag);
            OnPropertyChanged(nameof(VerenaListe));
        }

        public void Entfernen(Eintrag eintrag)
        {
            VerenaListe.Remove(eintrag);
            OnPropertyChanged(nameof(VerenaListe));

            // Abgleich mit Aufgabentabelle
            var aufgabeGefunden = _aufgabenViewModel.Aufgabenliste.FirstOrDefault(a => a.Art == eintrag.Text);

            if (aufgabeGefunden != null)
            {
                VerenaPunkte += aufgabeGefunden.Punkte;


                _dialogService.ShowMessage($"{aufgabeGefunden.Punkte} Points for the good Girl");
            }
            _hauptViewModel.Aktualisiere();
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string name) =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    }
}