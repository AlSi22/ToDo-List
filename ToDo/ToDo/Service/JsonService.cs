using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Globalization;
using ToDo.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDo.Service
{
    /* JsonService.cs
   
        Diese Klasse kümmert sich um das Speichern und Laden der relevanten Daten im JSON-Format.
   
        Verwendet bei
        Haupt- und Benutzerlisten (Main, Alex, Verena) + Punktestände + letzter Gewinner
        Aufgabenliste (wird separat in einer anderen Datei gespeichert)
    
          Dateipfade:
            "daten2.json"  = Hauptdaten
            "aufgaben.json" = Aufgabentabelle
  
          DatumsConverter für bessere lesbarkeit
    
          Error
          Falls beim Laden ein Fehler auftritt, wird eine leere Sammlung zurückgegeben + Fehlermeldung
    */
    public class JsonService : InterfaceJsonService
    {
        private static readonly string Dateipfad = "daten2.json";

        private static readonly string AufgabenDateipfad = "aufgaben.json";

        private readonly InterfaceDialogService _dialogService;

        public JsonService(InterfaceDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        // Haupt- und Nebenlisten speichern
        public void Speichern    (ObservableCollection<Eintrag> main, ObservableCollection<Eintrag> alex, ObservableCollection<Eintrag> verena,
                                        int alexPunkte, int verenaPunkte, string winner)
        {
            var daten = new GesamtDaten
            {
                Liste = main,
                AlexListe = alex,
                VerenaListe = verena,
                AlexPunkte = alexPunkte,
                VerenaPunkte = verenaPunkte,
                Winner = winner
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true, //Formatierung, Einrückungen um JSON Datei lesbar zu halten
            };
            string json = JsonSerializer.Serialize(daten, options); // Umwanldung in Json
            File.WriteAllText(Dateipfad, json); // eigentliche Speicherung
        }

        public          (ObservableCollection<Eintrag> main, ObservableCollection<Eintrag> alex, ObservableCollection<Eintrag> verena,
                        int alexPunkte, int verenaPunkte, string winner) Laden()
            
        {
            if (!File.Exists(Dateipfad))
            {
                return (new ObservableCollection<Eintrag>(),
                new ObservableCollection<Eintrag>(),
                new ObservableCollection<Eintrag>(),
                0,
                0,
                "Kein Sieger gefunden");

            }

            string json = File.ReadAllText(Dateipfad);
            try
            {
                var daten = JsonSerializer.Deserialize<GesamtDaten>(json);
                return 
                    (
                    daten?.Liste ?? new ObservableCollection<Eintrag>(),
                    daten?.AlexListe ?? new ObservableCollection<Eintrag>(),
                    daten?.VerenaListe ?? new ObservableCollection<Eintrag>(),
                    daten?.AlexPunkte ?? 0,
                    daten?.VerenaPunkte ?? 0,
                    daten?.Winner ?? "Kein Sieger gefunden"
                    );
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage("Hilfe, MayDay, Laden.... Error: hier für Gebildete " + ex.Message);
                return (new ObservableCollection<Eintrag>(), new ObservableCollection<Eintrag>(), 
                        new ObservableCollection<Eintrag>(), 0, 0, "Kein Sieger gefunden");
            }
            
        }
        

        private class GesamtDaten
        {
            public ObservableCollection<Eintrag> Liste { get; set; } = new();
            public ObservableCollection<Eintrag> AlexListe { get; set; } = new();
            public ObservableCollection<Eintrag> VerenaListe { get; set; } = new();

            public int AlexPunkte { get; set; }

            public int VerenaPunkte { get; set; }   

            public string Winner {  get; set; }

        }

        // Aufgabe seperat speichern

        public void SpeichernAufgaben(ObservableCollection<Aufgabe> aufgaben)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Datei-Eigenschaftsnamen in CamelCase-Form
                Converters=
                {
                    new AufgabenDatumConverter() 
                }
            };
            string json = JsonSerializer.Serialize(aufgaben, options);
            File.WriteAllText(AufgabenDateipfad, json);
        }

        public class AufgabenDatumConverter : JsonConverter<DateTime?>
        {
            private const string DatumsFormat = "dd.MM.yyyy";
            public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                    return null;

                if (reader.TokenType == JsonTokenType.String)
                {
                    var str = reader.GetString();
                    if (DateTime.TryParseExact(str, DatumsFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                        return result;
                }
            
                return null;
            }

            public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
            {
                if (value.HasValue)
                    writer.WriteStringValue(value.Value.ToString(DatumsFormat)); // amerikanisches Datum ist Standard, daher Änderung
                else
                    writer.WriteNullValue(); //wenn kein Datum vorhanden, dann Null
            }
        }

        // auch beim Laden muss das Datum angepasst werden, sonst werden zuvor gespeicherte elemente zu null
        public ObservableCollection<Aufgabe> LadenAufgaben()
        {
            if (!File.Exists(AufgabenDateipfad))
                return new ObservableCollection<Aufgabe>();

            try
            {
                string json = File.ReadAllText(AufgabenDateipfad);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters =
            {
                new AufgabenDatumConverter()  
            }
                };
                return JsonSerializer.Deserialize<ObservableCollection<Aufgabe>>(json, options)
                       ?? new ObservableCollection<Aufgabe>();
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage("Hilfe, MayDay, Laden.... Error: hier für Gebildete " + ex.Message);
                return new ObservableCollection<Aufgabe>();
            }
        }
    }
}

