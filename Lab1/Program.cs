class Program
{
    public static void Main()
    {
        Console.WriteLine("To jest ćwiczenie 1");

        Zwierze[] zwierzeta = new Zwierze[4];
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("Podaj nazwę:");
            string nazwa = Console.ReadLine();

            Console.WriteLine("Podaj gatunek:");
            string gatunek = Console.ReadLine();

            Console.WriteLine("Podaj liczbę nóg:");
            int liczbaNog = int.Parse(Console.ReadLine());

            zwierzeta[i] = new Zwierze(nazwa, gatunek, liczbaNog);
        }

        Console.WriteLine("Klona którego zwierzęcia chcesz stworzyć? (1-3)");
        int index = int.Parse(Console.ReadLine()) - 1;

        zwierzeta[3] = new Zwierze(zwierzeta[index]);

        Console.WriteLine("Podaj nową nazwę dla klona:");
        zwierzeta[3].setNazwa(Console.ReadLine());

        Console.WriteLine("\n Lista zwierząt:");

        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine(zwierzeta[i].ToString());
            zwierzeta[i].daj_glos();
        }

        Console.WriteLine($"\nŁączna liczba zwierząt: {Zwierze.getLiczbaZwierzat()}");
    }
}

class Zwierze
{
    private string nazwa;
    private string gatunek;
    private int liczbaNog;
    private static int liczbaZwierzat = 0;

    public string getNazwa() => nazwa;
    public string getGatunek() => gatunek;
    public int getLiczbaNog() => liczbaNog;

    public static int getLiczbaZwierzat() => liczbaZwierzat;

    public void setNazwa(string nowaNazwa)
    {
        nazwa = nowaNazwa;
    }

    public Zwierze()
    {
        nazwa = "Rex";
        gatunek = "Pies";
        liczbaNog = 4;
        liczbaZwierzat++;
    }

    public Zwierze(string n, string g, int l)
    {
        nazwa = n;
        gatunek = g;
        liczbaNog = l;
        liczbaZwierzat++;
    }

    public Zwierze(Zwierze x)
    {
        nazwa = x.nazwa;
        gatunek = x.gatunek;
        liczbaNog = x.liczbaNog;
        liczbaZwierzat++;
    }

    public void daj_glos()
    {
        string g = gatunek.ToLower();

        if (g.Contains("kot"))
            Console.WriteLine("Miau!");
        else if (g.Contains("pies"))
            Console.WriteLine("Hau!");
        else if (g.Contains("krow"))
            Console.WriteLine("Muuu!");
        else
            Console.WriteLine("(nieznany głos)");
    }

    public override string ToString()
    {
        return $"Nazwa: {nazwa}, Gatunek: {gatunek}, Nóg: {liczbaNog}";
    }
}