using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

public class Student
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<int> Grades { get; set; }

    public Student() { }

    public Student(string name, string surname, List<int> grades)
    {
        Name = name;
        Surname = surname;
        Grades = grades;
    }

    public override string ToString() => $"{Name} {Surname} | Oceny: {string.Join(", ", Grades)}";
}

class Program
{
    static void Main()
    {
        Console.WriteLine($"Ścieżka robocza: {Environment.CurrentDirectory}\n{new string('-', 40)}");

        // Sekcja 1: Praca z plikami tekstowymi
        string txtPath = "notatki.txt";
        UtworzPlikTekstowy(txtPath);
        WyswietlZawartoscPliku(txtPath);
        DodajLinieDoPliku(txtPath);

        // Przygotowanie danych studentów
        var grupaStudentow = new List<Student>
        {
            new Student("Marek", "Zieliński", new List<int> { 4, 4, 2 }),
            new Student("Zofia", "Kowalska", new List<int> { 6, 5, 5 }),
            new Student("Adam", "Mickiewicz", new List<int> { 3, 2, 4 })
        };

        // Sekcja 2: JSON
        string jsonFile = "dane_studentow.json";
        EksportujDoJson(grupaStudentow, jsonFile);
        ImportujZJson(jsonFile);

        // Sekcja 3: XML
        string xmlFile = "baza_studentow.xml";
        EksportujDoXml(grupaStudentow, xmlFile);
        ImportujZXml(xmlFile);

        // Sekcja 4: CSV (Statystyki i Filtrowanie)
        string csvSource = "Iris.csv";
        if (File.Exists(csvSource))
        {
            AnalizujStatystykiCsv(csvSource);
            FiltrujIZapiszCsv(csvSource, "iris_wynik.csv");
        }
        else
        {
            Console.WriteLine("\nUwaga: Nie odnaleziono pliku Iris.csv.");
        }

        Console.WriteLine("\nZakończono działanie. Naciśnij dowolny klawisz...");
        Console.ReadKey();
    }

    // --- METODY OBSŁUGI TEKSTU ---

    static void UtworzPlikTekstowy(string sciezka)
    {
        Console.WriteLine("Wprowadź 3 dowolne zdania:");
        var tresc = Enumerable.Range(1, 3).Select(i => {
            Console.Write($"Linia {i}: ");
            return Console.ReadLine();
        }).ToList();
        File.WriteAllLines(sciezka, tresc);
        Console.WriteLine("Plik tekstowy został utworzony.");
    }

    static void WyswietlZawartoscPliku(string sciezka)
    {
        if (!File.Exists(sciezka)) return;
        Console.WriteLine("\nAktualna treść pliku:");
        Console.WriteLine(File.ReadAllText(sciezka));
    }

    static void DodajLinieDoPliku(string sciezka)
    {
        Console.Write("Wpisz linię, którą chcesz dopisać: ");
        string dopisek = Console.ReadLine();
        File.AppendAllLines(sciezka, new[] { dopisek });
    }

    // --- METODY SERIALIZACJI (JSON & XML) ---

    static void EksportujDoJson(List<Student> dane, string sciezka)
    {
        var opcje = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        File.WriteAllText(sciezka, JsonSerializer.Serialize(dane, opcje));
        Console.WriteLine("\n[JSON] Dane zapisane.");
    }

    static void ImportujZJson(string sciezka)
    {
        if (!File.Exists(sciezka)) return;
        var studenci = JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(sciezka));
        Console.WriteLine("[JSON] Odczytani studenci:");
        studenci?.ForEach(s => Console.WriteLine(s));
    }

    static void EksportujDoXml(List<Student> dane, string sciezka)
    {
        var serializer = new XmlSerializer(typeof(List<Student>));
        using var writer = new StreamWriter(sciezka);
        serializer.Serialize(writer, dane);
        Console.WriteLine("\n[XML] Dane wyeksportowane.");
    }

    static void ImportujZXml(string sciezka)
    {
        if (!File.Exists(sciezka)) return;
        var serializer = new XmlSerializer(typeof(List<Student>));
        using var stream = new FileStream(sciezka, FileMode.Open);
        var wynik = (List<Student>)serializer.Deserialize(stream);
        Console.WriteLine("[XML] Dane odczytane:");
        wynik.ForEach(s => Console.WriteLine(s));
    }

    // --- METODY ANALIZY CSV ---

    static void AnalizujStatystykiCsv(string sciezka)
    {
        var rekordy = File.ReadAllLines(sciezka)
            .Skip(1)
            .Select(linia => linia.Split(','))
            .Where(kolumny => kolumny.Length >= 4)
            .ToList();

        Console.WriteLine("\n--- Statystyki zbioru Iris ---");
        string[] nazwy = { "Sepal Length", "Sepal Width", "Petal Length", "Petal Width" };

        for (int i = 0; i < 4; i++)
        {
            var wartosci = rekordy
                .Select(r => double.TryParse(r[i], NumberStyles.Any, CultureInfo.InvariantCulture, out double v) ? v : (double?)null)
                .Where(v => v.HasValue)
                .Select(v => v.Value)
                .ToList();

            if (wartosci.Any())
                Console.WriteLine($"{nazwy[i],-15}: Średnia = {wartosci.Average():F2}");
        }
    }

    static void FiltrujIZapiszCsv(string wejscie, string wyjscie)
    {
        var przefiltrowane = File.ReadAllLines(wejscie)
            .Skip(1)
            .Select(l => l.Split(','))
            .Where(k => k.Length >= 5 && double.TryParse(k[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double len) && len < 5.0)
            .Select(k => $"{k[0]},{k[1]},{k[4]}")
            .ToList();

        przefiltrowane.Insert(0, "sepal_length,sepal_width,species");
        File.WriteAllLines(wyjscie, przefiltrowane);
        Console.WriteLine($"\nFiltrowanie zakończone. Zapisano {przefiltrowane.Count - 1} rekordów do {wyjscie}");
    }
}