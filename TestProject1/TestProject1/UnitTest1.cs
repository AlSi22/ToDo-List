using NUnit.Framework;
using ToDo.ViewModel.Main;
using System.Collections.ObjectModel;
using ToDo.Model;
//using NUnit.Framework.Legacy;

namespace TestProject1
{
    public class Tests

    {

        //MainView - Test : Aktualisiere, Verschiebe
        [Test]
        public void Aktualisiere_Setzt_Korrekte_Überschrift_Bei_LeererListe()
        {
            // Vorbereitung leere Listen
            var leereListe = new ObservableCollection<Eintrag>();

            var testAlex = new ObservableCollection<Eintrag>();
            var testVerena = new ObservableCollection<Eintrag>();

            // Json testdatei, message immer ja
            var dialogService = new TestDialog();
            var jsonService = new TestJsonService();


            var viewModel = new MainViewModel(
                                                leereListe,
                                                testAlex,
                                                testVerena,
                                                0, 0, "TestWinner",
                                                jsonService,
                                                dialogService);
            // Führe Methode aus
            viewModel.Aktualisiere();


            // Überprüfung
            // altes Assert
            // ClassicAssert.AreEqual("Sehr vorbildlich, hier ein Eis ", viewModel.ListenÜberschrift);
            // neues Assert
            Assert.That(viewModel.ListenÜberschrift, Is.EqualTo("Sehr vorbildlich, hier ein Eis "));
        }

        [Test]
        public void Aktualisiere_Setzt_Korrekte_Überschrift_Bei_KurzerListe()
        {
            // Vorbereitung Liste mit 2 Einträgen
            var kurzeListe = new ObservableCollection<Eintrag>
            {
                new Eintrag { Text = "Test1" },
                new Eintrag { Text = "Test2" }
            };
            var testAlex = new ObservableCollection<Eintrag>();
            var testVerena = new ObservableCollection<Eintrag>();

            var dialogService = new TestDialog();
            var jsonService = new TestJsonService();

            var viewModel = new MainViewModel(
                                                kurzeListe,
                                                testAlex,
                                                testVerena,
                                                0, 0, "TestWinner",
                                                jsonService,
                                                dialogService);

            // Führe Methode aus
            viewModel.Aktualisiere();

            // Ergebnis-Check 24
            // altes Assert
            // ClassicAssert.IsTrue(viewModel.ListenÜberschrift.Contains("2 Dinge"));
            Assert.That(viewModel.ListenÜberschrift, Does.Contain("2 Dinge"));
        }

        [Test]
        public void Aktualisiere_Setzt_Korrekte_Überschrift_Bei_LangerListe()
        {
            // Vorbereitung Liste mit 6 Einträgen
            var langeListe = new ObservableCollection<Eintrag>
            {
                new Eintrag { Text = "Test1" },
                new Eintrag { Text = "Test2" },
                new Eintrag { Text = "Test3" },
                new Eintrag { Text = "Test4" },
                new Eintrag { Text = "Test5" },
                new Eintrag { Text = "Test6" },
            };
            var testAlex = new ObservableCollection<Eintrag>();
            var testVerena = new ObservableCollection<Eintrag>();

            var dialogService = new TestDialog();
            var jsonService = new TestJsonService();

            var viewModel = new MainViewModel(
                                                langeListe,
                                                testAlex,
                                                testVerena,
                                                0, 0, "TestWinner",
                                                jsonService,
                                                dialogService);

            // Führe Methode aus
            viewModel.Aktualisiere();

            // Ergebnis-Check 24
            
            Assert.That(viewModel.ListenÜberschrift, Does.Contain("Pötte"));
        }

        [Test]
        public void Hinzufügen_Erhöht_Liste_Um_Einen_Eintrag()
        {
            // Vorberetung leere liste
            var liste = new ObservableCollection<Eintrag>();
            var dialog = new TestDialog();
            var jsonService = new TestJsonService();

            var viewModel = new MainViewModel(
                                                liste,
                                                new ObservableCollection<Eintrag>(),
                                                new ObservableCollection<Eintrag>(),
                                                0, 0, "Test",
                                                jsonService,
                                                dialog);


            // Füge 1 Element hinzu
            viewModel.EingabeText="Test";   
            viewModel.Hinzufügen();

            // Ergebnischeck
            // ClassicAssert.AreEqual(1, liste.Count);
            // ClassicAssert.AreEqual("Test", liste[0].Text);


            // Multiple Mehrere Bedingungen in einem Test
            Assert.Multiple(() =>
            {
                // hat liste 1 Eintrag
                Assert.That(liste, Has.Count.EqualTo(1));
                // ist der Eintrag Test
                Assert.That(liste[0].Text, Is.EqualTo("Test"));
            });
        }

        [Test]
        public void Verschieben()
        {
            // Vorbereitung
            var Liste = new ObservableCollection<Eintrag>
            {
                new Eintrag { Text = "unten" },
                new Eintrag { Text = "oben" },
                new Eintrag { Text = "Test" },
             
            };
            var testAlex = new ObservableCollection<Eintrag>();
            var testVerena = new ObservableCollection<Eintrag>();

            var dialogService = new TestDialog();
            var jsonService = new TestJsonService();

            var viewModel = new MainViewModel(
                                                Liste,
                                                testAlex,
                                                testVerena,
                                                0, 0, "TestWinner",
                                                jsonService,
                                                dialogService);

            // Ausführung
            viewModel.SelectedListItem = Liste[1];
            viewModel.ObenVerschieben();
            // Grenzfall ausloten, Oberstes Element, nach oben verschieben
            viewModel.ObenVerschieben();


            viewModel.SelectedListItem = Liste[1];
            viewModel.UntenVerschieben();
            // Grenzfall ausloten, Unterstes Element, nach unten verschieben
            viewModel.UntenVerschieben();


            // Check
            Assert.Multiple(() =>
            {
                
                Assert.That(Liste[0].Text, Is.EqualTo("oben"));
               
                Assert.That(Liste[2].Text, Is.EqualTo("unten"));
            });


        }
    }
}