using NUnit.Framework;
using ToDo.ViewModel.Main;
using System.Collections.ObjectModel;
using ToDo.Model;
using Microsoft.Testing.Platform.Extensions.Messages;
using ToDo.ViewModel.Aufgaben;

namespace TestProject1
{

    
    public class AufgabenViewModel_Test
    {
        // Test ob Aufgaben automatisch auf MainListe hinzugefügt werden
        
        [Test]

        public void CheckHauptliste_addAufgabe()
        {
            // Vorbereitung

            var heute = DateTime.Today;
            var alteAufgabe = new Aufgabe
            {
                Art = "Spülmaschine",
                Tage = 3,
                // Aufgabe fällig
                AufgabeHinzugefügt = heute.AddDays(-5)
            };

            var aufgabenListe = new ObservableCollection<Aufgabe> { alteAufgabe };
            
            var jsonService = new TestJsonService { Aufgaben = aufgabenListe };
            var dialogService = new TestDialog();

            var viewModel = new AufgabenViewModel(jsonService, dialogService);

            var hauptliste = new ObservableCollection<Eintrag>();
            var alexliste = new ObservableCollection<Eintrag>();
            var veranaliste = new ObservableCollection<Eintrag>();


            viewModel.CheckHauptliste(hauptliste, alexliste, veranaliste);

            Assert.Multiple(() =>
            {
                Assert.That(hauptliste, Has.Count.EqualTo(1));
                Assert.That(hauptliste[0].Text, Is.EqualTo("Spülmaschine"));
                Assert.That(alteAufgabe.AufgabeHinzugefügt, Is.EqualTo(heute));
            });
        }

        [Test]

        public void CheckHauptliste_addNothing()
        { 

            // Vorbereitung
                var heute = DateTime.Today;
                var keineAufgabe = new Aufgabe
                {
                    Art = "Spülmaschine",
                    Tage = 7,
                    // Aufgabe nicht fällig
                    AufgabeHinzugefügt = heute.AddDays(-5)
                };

                var aufgabenListe = new ObservableCollection<Aufgabe> { keineAufgabe };

                var jsonService = new TestJsonService { Aufgaben = aufgabenListe };
                var dialogService = new TestDialog();

                var viewModel = new AufgabenViewModel(jsonService, dialogService);

                var hauptliste = new ObservableCollection<Eintrag>();
                var alexliste = new ObservableCollection<Eintrag>();
                var veranaliste = new ObservableCollection<Eintrag>();

            // Ausführung
                viewModel.CheckHauptliste(hauptliste, alexliste, veranaliste);

              // Check  
                Assert.That(hauptliste, Is.Empty);
        }
        [Test]
        public void CheckHauptliste_AufgabeAufHauptListe()
        {
            // Vorbereitung

            var heute = DateTime.Today;
            var alteAufgabe = new Aufgabe
            {
                Art = "Spülmaschine",
                Tage = 3,
                // Aufgabe fällig
                AufgabeHinzugefügt = heute.AddDays(-5)
            };

            var aufgabenListe = new ObservableCollection<Aufgabe> { alteAufgabe };

            var jsonService = new TestJsonService { Aufgaben = aufgabenListe };
            var dialogService = new TestDialog();

            var viewModel = new AufgabenViewModel(jsonService, dialogService);

            var hauptliste = new ObservableCollection<Eintrag> { new Eintrag { Text = "Spülmaschine"} };
            var alexliste = new ObservableCollection<Eintrag>();
            var veranaliste = new ObservableCollection<Eintrag>();

            // Ausführung
            viewModel.CheckHauptliste(hauptliste, alexliste, veranaliste);


            // Check
            Assert.Multiple(() =>
            {
                Assert.That(hauptliste, Has.Count.EqualTo(1));
                Assert.That(hauptliste[0].Text, Is.EqualTo("Spülmaschine"));
            });
        }
        [Test]
        public void CheckHauptliste_AufgabeAufAlexListe()
        {
            // Vorbereitung

            var heute = DateTime.Today;
            var alteAufgabe = new Aufgabe
            {
                Art = "Spülmaschine",
                Tage = 3,
                // Aufgabe fällig
                AufgabeHinzugefügt = heute.AddDays(-5)
            };

            var aufgabenListe = new ObservableCollection<Aufgabe> { alteAufgabe };

            var jsonService = new TestJsonService { Aufgaben = aufgabenListe };
            var dialogService = new TestDialog();

            var viewModel = new AufgabenViewModel(jsonService, dialogService);

            var hauptliste = new ObservableCollection<Eintrag> ();
            var alexliste = new ObservableCollection<Eintrag> { new Eintrag { Text = "Spülmaschine" } };
            var veranaliste = new ObservableCollection<Eintrag>();

            // Ausführung
            viewModel.CheckHauptliste(hauptliste, alexliste, veranaliste);


            // Check
            Assert.That(hauptliste, Is.Empty);
        }
        [Test]
        public void CheckHauptliste_AufgabeAufVerenaListe()
        {
            // Vorbereitung

            var heute = DateTime.Today;
            var alteAufgabe = new Aufgabe
            {
                Art = "Spülmaschine",
                Tage = 3,
                // Aufgabe fällig
                AufgabeHinzugefügt = heute.AddDays(-5)
            };

            var aufgabenListe = new ObservableCollection<Aufgabe> { alteAufgabe };

            var jsonService = new TestJsonService { Aufgaben = aufgabenListe };
            var dialogService = new TestDialog();

            var viewModel = new AufgabenViewModel(jsonService, dialogService);

            var hauptliste = new ObservableCollection<Eintrag>();
            var alexliste = new ObservableCollection<Eintrag>();
            var veranaliste = new ObservableCollection<Eintrag> { new Eintrag { Text = "Spülmaschine" } };

            // Ausführung
            viewModel.CheckHauptliste(hauptliste, alexliste, veranaliste);


            // Check
            Assert.That(hauptliste, Is.Empty);
        }
    }
}
