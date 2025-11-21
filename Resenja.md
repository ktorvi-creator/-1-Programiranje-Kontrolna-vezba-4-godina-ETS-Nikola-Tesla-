# Rešenja zadataka (C#)

U nastavku su dati primeri koda i kratka objašnjenja za zadatke 1–44. Primeri za konzolne programe su potpuni (mogu se nalepiti u `Program.cs` neke konzolne aplikacije), dok su primeri za Windows Forms dati kroz ključne delove koda za Form klase.

## 1. Istorija pregledača (stack)
```csharp
using System;
using System.Collections.Generic;

class Program
{
    // Program simulira istoriju pregledača korišćenjem steka.
    static void Main()
    {
        Stack<string> istorija = new Stack<string>(); // čuva URL-ove

        while (true)
        {
            string unos = Console.ReadLine();

            if (unos == "exit") 
                break; // izlazak iz programa

            if (unos == "back")
            {
                // ako nema prethodne stranice, ispisujemo "-"
                if (istorija.Count == 0)
                {
                    Console.WriteLine("-");
                }
                else
                {
                    istorija.Pop(); // vraćamo se na prethodnu
                    if (istorija.Count == 0)
                        Console.WriteLine("-");
                    else
                        Console.WriteLine(istorija.Peek()); // prikaz trenutne stranice
                }
            }
            else
            {
                istorija.Push(unos); // svaka nova stranica se upisuje
            }
        }
    }
}
```

## 2. Segment najvećeg zbira dužine *k*
```csharp
using System;

class Program
{
    // Nalazi početak segmenta dužine k sa najvećim zbirom
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());

        // Cuvamo samo niz realnih brojeva (double)
        double[] a = new double[n];

        for (int i = 0; i < n; i++)
            a[i] = double.Parse(Console.ReadLine());

        // Prvi zbir – zbir prvih k elemenata
        double trenutniZbir = 0;
        for (int i = 0; i < k; i++)
            trenutniZbir += a[i];

        double maxZbir = trenutniZbir;
        int maxPoz = 0; // početna pozicija segmenta sa najvećim zbirom

        // Klizni prozor – izbegava nepotrebno sabiranje cele podsekvence
        for (int i = 1; i <= n - k; i++)
        {
            // Oduzimamo prvi element prethodnog segmenta i dodajemo novi
            trenutniZbir = trenutniZbir - a[i - 1] + a[i + k - 1];

            if (trenutniZbir > maxZbir)
            {
                maxZbir = trenutniZbir;
                maxPoz = i;
            }
        }

        Console.WriteLine(maxPoz);
    }
}
```

## 3. Rečnik proizvoda i cena
```csharp
using System;
using System.Collections.Generic;

class Program
{
    // Program koristi generički rečnik za čuvanje proizvoda i njihovih cena.
    static void Main()
    {
        Dictionary<string, double> proizvodi = new Dictionary<string, double>();

        // Unos proizvoda dok se ne unese "kraj"
        while (true)
        {
            string naziv = Console.ReadLine();

            if (naziv == "kraj")
                break;

            // Ako proizvod postoji u rečniku → prikaži cenu
            if (proizvodi.ContainsKey(naziv))
            {
                Console.WriteLine(proizvodi[naziv]);
            }
            else
            {
                // Ako ne postoji → dodaj novi proizvod
                double cena = double.Parse(Console.ReadLine());
                proizvodi[naziv] = cena;
            }
        }

        // Ispis celog rečnika
        foreach (var par in proizvodi)
        {
            Console.WriteLine($"{par.Key} {par.Value}");
        }
    }
}
```

## 4. Brojanje pojavljivanja reči u prvih *N*
```csharp
using System;
using System.Collections.Generic;

class Program
{
    // Program broji koliko se puta pojavljuje svaka od k zadatih reči među prvih n reči.
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        List<string> reci = new List<string>(n);

        // Učitavanje n reči
        for (int i = 0; i < n; i++)
            reci.Add(Console.ReadLine());

        int k = int.Parse(Console.ReadLine());

        // Učitavanje k različitih reči koje proveravamo
        List<string> trazene = new List<string>(k);
        for (int i = 0; i < k; i++)
            trazene.Add(Console.ReadLine());

        // Brojanje pojavljivanja
        foreach (string trazi in trazene)
        {
            int brojPojavljivanja = 0;

            // Prolazak kroz prvih n reči
            foreach (string r in reci)
            {
                if (r == trazi)
                    brojPojavljivanja++;
            }

            Console.WriteLine(brojPojavljivanja);
        }
    }
}
```

## 5. Pikado (brojanje pogodaka)
```csharp
using System;
using System.Collections.Generic;

class Program
{
    // Jednostavnije: brojimo samo polja koja su pogođena.
    static void Main()
    {
        string[] prvi = Console.ReadLine().Split();
        int N = int.Parse(prvi[0]);
        int K = int.Parse(prvi[1]);

        Dictionary<int, int> broj = new Dictionary<int, int>();

        string[] pogodci = Console.ReadLine().Split();

        for (int i = 0; i < N; i++)
        {
            int p = int.Parse(pogodci[i]);

            if (!broj.ContainsKey(p))
                broj[p] = 0;

            broj[p]++;
        }

        List<int> rezultat = new List<int>();

        foreach (var par in broj)
        {
            if (par.Value >= K)
                rezultat.Add(par.Key);
        }

        Console.WriteLine(rezultat.Count);

        if (rezultat.Count > 0)
            Console.WriteLine(string.Join(" ", rezultat));
    }
}
```

## 6. Generički delegat za četiri operacije
```csharp
using System;
using System.Windows.Forms;

namespace Zadatak6
{
    // generički delegat
    public delegate T Operacija<T>(T a, T b);

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // INT operacije
        int SaberiInt(int a, int b) { return a + b; }
        int OduzmiInt(int a, int b) { return a - b; }
        int PomnoziInt(int a, int b) { return a * b; }
        int PodeliInt(int a, int b) { return a / b; }

        // DOUBLE operacije
        double SaberiDouble(double a, double b) { return a + b; }
        double OduzmiDouble(double a, double b) { return a - b; }
        double PomnoziDouble(double a, double b) { return a * b; }
        double PodeliDouble(double a, double b) { return a / b; }

        private void btnIzracunaj_Click(object sender, EventArgs e)
        {
            string op = listBoxOperacije.SelectedItem.ToString();
            string tip = listBoxTip.SelectedItem.ToString();

            string s1 = txtA.Text;
            string s2 = txtB.Text;

            if (tip == "Celi brojevi")
            {
                int a = int.Parse(s1);
                int b = int.Parse(s2);

                Operacija<int> o = null;

                if (op == "Sabiranje") o = SaberiInt;
                if (op == "Oduzimanje") o = OduzmiInt;
                if (op == "Mnozenje") o = PomnoziInt;
                if (op == "Deljenje") o = PodeliInt;

                MessageBox.Show("Rezultat: " + o(a, b));
            }
            else // realni brojevi
            {
                double a = double.Parse(s1);
                double b = double.Parse(s2);

                Operacija<double> o = null;

                if (op == "Sabiranje") o = SaberiDouble;
                if (op == "Oduzimanje") o = OduzmiDouble;
                if (op == "Mnozenje") o = PomnoziDouble;
                if (op == "Deljenje") o = PodeliDouble;

                MessageBox.Show("Rezultat: " + o(a, b));
            }
        }
    }
}
```

## 7. Generička metoda PrintInfo
```csharp
using System;

class PrintInfo
{
    public void Print<T>(T x)
    {
        Console.WriteLine($"Tip: {typeof(T).Name}, Vrednost: {x}");
    }
}

class Program
{
    static void Main()
    {
        PrintInfo p = new PrintInfo();

        p.Print<int>(5);
        p.Print<double>(3.14);
        p.Print<char>('A');
        p.Print<bool>(true);
        p.Print<string>("Hello");
    }
}
```

## 8. Generička provera jednakosti
```csharp
using System;

class Program
{
    static bool Jednak<T>(T a, T b)
    {
        return a.Equals(b);
    }

    static void Main()
    {
        Console.WriteLine(Jednak(3, 3));         // true
        Console.WriteLine(Jednak("abc", "ab"));  // false
    }
}
```

## 9. Generička razmena vrednosti
```csharp
using System;

class Program
{
    // Generička metoda za ispis
    static void Ispis<T>(T x)
    {
        Console.WriteLine(x);
    }

    // Generička metoda za zamenu vrednosti
    static void Zameni<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    static void Main()
    {
        string ime = "Petar";
        string prezime = "Petrović";

        // zamena pomoću generičke metode
        Zameni(ref ime, ref prezime);

        // ispis novog poretka
        Ispis(ime);
        Ispis(prezime);
    }
}
```

## 10. Generičko sabiranje
```csharp
using System;

class Program
{
    // Generička metoda koja sabira dva broja
    static T Saberi<T>(T a, T b)
    {
        return (dynamic)a + (dynamic)b;
    }

    static void Main()
    {
        // Sabiranje int brojeva
        int x = Saberi(5, 7);
        Console.WriteLine(x);

        // Sabiranje double brojeva
        double y = Saberi(2.5, 3.4);
        Console.WriteLine(y);
    }
}
```

## 11. MessageBox sa dugmadima i helpom (Windows Forms)
```csharp
using System;
using System.Windows.Forms;

namespace Zadatak11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Otvaranje help fajla (može biti bilo koji .txt)
            string helpPutanja = @"C:\help.txt";

            MessageBox.Show(
                "Tekst u poruci",
                "Naslov poruke",
                MessageBoxButtons.YesNoCancel,     // uključuje i "Cancel" koji koristimo kao Help
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2,   // Button2 = No
                0,
                helpPutanja                        // help fajl
            );
        }
    }
}
```

## 12. Generička klasa sa jednim tipom + Student
```csharp
using System;

class GenerickaKlasa<T>
{
    private T podatak;

    public GenerickaKlasa(T x)
    {
        podatak = x;
    }

    public void Ispisi()
    {
        Console.WriteLine("Tip: " + typeof(T));
        Console.WriteLine("Vrednost: " + podatak);
        Console.WriteLine();
    }
}

class Student
{
    private string ime;
    private string prezime;
    private string brojIndeksa;
    private DateTime datumRodjenja;
    private int godina;
    private string smer;

    public Student(string i, string p, string bi, DateTime dr, int g, string s)
    {
        ime = i;
        prezime = p;
        brojIndeksa = bi;
        datumRodjenja = dr;
        godina = g;
        smer = s;
    }

    public override string ToString()
    {
        return "Student: " + ime + " " + prezime +
               ", indeks: " + brojIndeksa +
               ", datum rodjenja: " + datumRodjenja.ToShortDateString() +
               ", godina: " + godina +
               ", smer: " + smer;
    }
}

class Program
{
    static void Main()
    {
        var g1 = new GenerickaKlasa<int>(10);
        var g2 = new GenerickaKlasa<double>(3.14);
        var g3 = new GenerickaKlasa<string>("Hello");

        Student st = new Student("Marko", "Marković", "IT-42/22",
                                 new DateTime(2004, 5, 12), 2, "IT");

        var g4 = new GenerickaKlasa<Student>(st);

        g1.Ispisi();
        g2.Ispisi();
        g3.Ispisi();
        g4.Ispisi();
    }
}
```

## 13. Generička klasa sa dva tipa
```csharp
using System;

class Klasa<T, U>
{
    private T prvi;
    private U drugi;

    public Klasa(T a, U b)
    {
        prvi = a;
        drugi = b;
    }

    public T Prvi
    {
        get { return prvi; }
        set { prvi = value; }
    }

    public U Drugi
    {
        get { return drugi; }
        set { drugi = value; }
    }

    public void Ispis()
    {
        Console.WriteLine("Prvi: " + prvi + " (tip: " + typeof(T) + ")");
        Console.WriteLine("Drugi: " + drugi + " (tip: " + typeof(U) + ")");
    }
}

class Program
{
    static void Main()
    {
        Klasa<char, char> obj = new Klasa<char, char>('A', 'Z');
        obj.Ispis();
    }
}
```

## 14. Klasa Point<T>
```csharp
using System;

class Point<T>
{
    private T x;
    private T y;

    public Point(T x, T y)
    {
        this.x = x;
        this.y = y;
    }

    public T X
    {
        get { return x; }
        set { x = value; }
    }

    public T Y
    {
        get { return y; }
        set { y = value; }
    }

    public void Prikazi()
    {
        Console.WriteLine("X = " + x + " (tip: " + typeof(T) + ")");
        Console.WriteLine("Y = " + y + " (tip: " + typeof(T) + ")");
    }
}

class Program
{
    static void Main()
    {
        Point<int> p1 = new Point<int>(3, 7);
        Point<double> p2 = new Point<double>(2.5, 4.1);

        p1.Prikazi();
        Console.WriteLine();
        p2.Prikazi();
    }
}
```

## 15. Klasa Line<T>
```csharp
using System;

class Line<T>
{
    private T x1;
    private T y1;
    private T x2;
    private T y2;

    public Line(T a, T b, T c, T d)
    {
        x1 = a;
        y1 = b;
        x2 = c;
        y2 = d;
    }

    public T X1
    {
        get { return x1; }
        set { x1 = value; }
    }

    public T Y1
    {
        get { return y1; }
        set { y1 = value; }
    }

    public T X2
    {
        get { return x2; }
        set { x2 = value; }
    }

    public T Y2
    {
        get { return y2; }
        set { y2 = value; }
    }

    public void Prikazi()
    {
        Console.WriteLine("Tačka 1: (" + x1 + ", " + y1 + ")");
        Console.WriteLine("Tačka 2: (" + x2 + ", " + y2 + ")");
        Console.WriteLine("Tip: " + typeof(T));
    }
}

class Program
{
    static void Main()
    {
        Line<float> linija1 = new Line<float>(1.2f, 3.4f, 5.6f, 7.8f);
        Line<long> linija2 = new Line<long>(10, 20, 30, 40);

        linija1.Prikazi();
        Console.WriteLine();
        linija2.Prikazi();
    }
}
```

## 16. Generička klasa sa tipovima T1 i T2
```csharp
using System;

class Klasa<T1, T2>
{
    private T1 prvi;
    private T2 drugi;

    public Klasa(T1 a, T2 b)
    {
        prvi = a;
        drugi = b;
    }

    public void Prikazi()
    {
        Console.WriteLine("Prvi: " + prvi + " (tip: " + typeof(T1) + ")");
        Console.WriteLine("Drugi: " + drugi + " (tip: " + typeof(T2) + ")");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Klasa<int, string> o1 = new Klasa<int, string>(10, "Hello");
        Klasa<char, bool> o2 = new Klasa<char, bool>('A', true);
        Klasa<float, short> o3 = new Klasa<float, short>(3.5f, 12);
        Klasa<double, double> o4 = new Klasa<double, double>(2.5, 7.9);

        o1.Prikazi();
        o2.Prikazi();
        o3.Prikazi();
        o4.Prikazi();
    }
}
```

## 17. Četvorougao (generički)
```csharp
using System;

class Cetvorougao<T>
{
    private T a;
    private T b;
    private T h;

    public Cetvorougao() { }

    public Cetvorougao(T a, T b, T h)
    {
        this.a = a;
        this.b = b;
        this.h = h;
    }

    public T A
    {
        get { return a; }
        set { a = value; }
    }

    public T B
    {
        get { return b; }
        set { b = value; }
    }

    public T H
    {
        get { return h; }
        set { h = value; }
    }

    // Površina: a * h
    public double Povrsina()
    {
        double aa = Convert.ToDouble(a);
        double hh = Convert.ToDouble(h);
        return aa * hh;
    }
}

class Program
{
    static void Main()
    {
        Cetvorougao<short> c1 = new Cetvorougao<short>(5, 7, 3);
        Cetvorougao<float> c2 = new Cetvorougao<float>(4.5f, 6.2f, 2.0f);

        Console.WriteLine("Površina 1: " + c1.Povrsina());
        Console.WriteLine("Površina 2: " + c2.Povrsina());
    }
}
```

## 18. Generička klasa sa metodom koja vraća podatak
```csharp
using System;

class MojTip<T>
{
    private T podatak;

    public MojTip(T x)
    {
        podatak = x;
    }

    public T VratiVrednost()
    {
        return podatak;
    }

    public void Print<U>(U x)
    {
        Console.WriteLine("Print: " + x);
    }
}

class Program
{
    static void Main()
    {
        MojTip<int> o1 = new MojTip<int>(42);
        MojTip<string> o2 = new MojTip<string>("Hello");

        // ispis vrednosti podataka objekata
        Console.WriteLine(o1.VratiVrednost());
        Console.WriteLine(o2.VratiVrednost());

        // generička metoda za tipove string i float
        o1.Print<string>("Test");
        o2.Print<float>(3.14f);
    }
}
```

## 19. Generička osnovna i izvedena klasa
```csharp
using System;

class Osnovna<T>
{
    private T podatak1;

    public Osnovna(T x)
    {
        podatak1 = x;
    }

    public T Podatak1
    {
        get { return podatak1; }
        set { podatak1 = value; }
    }
}

class Izvedena<T> : Osnovna<T>
{
    private T podatak2;

    public Izvedena(T prvi, T drugi) : base(prvi)
    {
        podatak2 = drugi;
    }

    public T Podatak2
    {
        get { return podatak2; }
        set { podatak2 = value; }
    }
}

class Program
{
    static void Main()
    {
        Izvedena<int> obj = new Izvedena<int>(10, 20);

        Console.WriteLine("Podatak 1: " + obj.Podatak1);
        Console.WriteLine("Podatak 2: " + obj.Podatak2);
    }
}
```

## 20. Generički delegati sa različitim rezultatima
```csharp
using System;

class Program
{
    // generički delegat
    delegate TRez MojDelegat<TRez, TArg>(TArg x);

    // generička metoda 1 – vraća string
    static string Fun1<T>(T x)
    {
        return "Vrednost je: " + x;
    }

    // generička metoda 2 – vraća int
    static int Fun2<T>(T x)
    {
        return x.GetHashCode();   // jednostavno pretvaranje u int
    }

    static void Main()
    {
        // delegat 1: rezultat string, argument int
        MojDelegat<string, int> d1 = Fun1<int>;
        Console.WriteLine(d1(10));

        // delegat 2: rezultat int, argument double
        MojDelegat<int, double> d2 = Fun2<double>;
        Console.WriteLine(d2(3.14));
    }
}
```

## 21. Tipizirani delegat za realne operacije
```csharp
using System;

class Program
{
    // tipizirani delegat
    delegate double Operacija(double a, double b);

    static double Saberi(double a, double b)
    {
        return a + b;
    }

    static double Oduzmi(double a, double b)
    {
        return a - b;
    }

    static double Pomnozi(double a, double b)
    {
        return a * b;
    }

    static double Podeli(double a, double b)
    {
        return a / b;   // bez provere radi jednostavnosti
    }

    static void Main()
    {
        Operacija op;

        op = Saberi;
        Console.WriteLine("Sabiranje: " + op(10, 5));

        op = Oduzmi;
        Console.WriteLine("Oduzimanje: " + op(10, 5));

        op = Pomnozi;
        Console.WriteLine("Mnozenje: " + op(10, 5));

        op = Podeli;
        Console.WriteLine("Deljenje: " + op(10, 5));
    }
}
```

## 22. Delegat sa 2 funkcije
```csharp
using System;

class Program
{
    // generički delegat
    delegate TRez MojDelegat<TRez, TArg>(TArg x);

    // generička metoda 1: string rezultat, argument tipa T
    static string Fun1<T>(T x)
    {
        return "String funkcija: " + x;
    }

    // generička metoda 2: int rezultat, argument tipa T
    static int Fun2<T>(T x)
    {
        return x;
    }

    static void Main()
    {
        // delegat 1: rezultat string, argument int, pokazuje na Fun1
        MojDelegat<string, int> d1 = Fun1<int>;
        Console.WriteLine(d1(10));

        // delegat 2: rezultat int, argument double, pokazuje na Fun2
        MojDelegat<int, double> d2 = Fun2<double>;
        Console.WriteLine(d2(3.14));
    }
}

```

## 23. Negenerički interfejs sa generičkom metodom
```csharp
using System;

// negenerički interfejs
interface IInfo
{
    string GetInfo<T1, T2>(T1 x, T2 y);
}

// klasa koja implementira interfejs
class CInfo : IInfo
{
    public string GetInfo<T1, T2>(T1 x, T2 y)
    {
        return "Vrednost 1: " + x +
               " (tip: " + typeof(T1) + ")\n" +
               "Vrednost 2: " + y +
               " (tip: " + typeof(T2) + ")";
    }
}

class Program
{
    static void Main()
    {
        CInfo obj = new CInfo();

        // kombinacija int i double
        Console.WriteLine(obj.GetInfo<int, double>(5, 3.14));
        Console.WriteLine();

        // kombinacija char i float
        Console.WriteLine(obj.GetInfo<char, float>('A', 2.5f));
    }
}
```

## 24. Poređenje osoba preko delegata
```csharp
using System;

class Osoba
{
    public string Ime, Prezime;
    public Osoba(string ime, string prezime) { Ime = ime; Prezime = prezime; }
}

delegate int Poredi(Osoba a, Osoba b);

class Program
{
    static int PoImenu(Osoba a, Osoba b) => string.Compare(a.Ime, b.Ime, true);
    static int PoPrezimenu(Osoba a, Osoba b) => string.Compare(a.Prezime, b.Prezime, true);

    static void Main()
    {
        var o1 = new Osoba("Ana", "Anic");
        var o2 = new Osoba("Petar", "Petrovic");

        Poredi d = PoImenu;
        Console.WriteLine(d(o1, o2) < 0 ? "Prvo ime je pre" : "Drugo ime je pre");
        d = PoPrezimenu;
        Console.WriteLine(d(o1, o2) < 0 ? "Prezime 1 pre" : "Prezime 2 pre");
    }
}
```

## 25. Delegat u Windows Forms (krug/sfera)
```csharp
// U Form1.cs
public partial class Form1 : Form
{
    private delegate double Op(double r);
    public Form1()
    {
        InitializeComponent();
        Op obim = r => 2 * Math.PI * r;
        Op povrsina = r => Math.PI * r * r;
        Op zapremina = r => 4.0 / 3.0 * Math.PI * Math.Pow(r, 3);

        double r = 2;
        MessageBox.Show($"O: {obim(r)}, P: {povrsina(r)}, V: {zapremina(r)}");
    }
}
```

## 26. Generička osnovna/izvedena klasa (KKA)
```csharp
using System;

class Base<T>
{
    public T A { get; set; }
    public Base(T a) { A = a; }
}

class Derived<T> : Base<T>
{
    public T B { get; set; }
    public Derived(T a, T b) : base(a) { B = b; }
}

class Program
{
    static void Main()
    {
        var x = new Derived<int>(1, 2);
        Console.WriteLine($"A={x.A}, B={x.B}");
    }
}
```

## 27. Niz1/Niz2 generičke klase
```csharp
using System;

class Niz1<T>
{
    protected T[] niz;
    public Niz1(T[] niz) { this.niz = niz; }
    public T VratiVrednost(int i) => niz[i];
    public void Ispis1() => Console.WriteLine(string.Join(" ", niz));
}

class Niz2<T, V> : Niz1<T>
{
    private V[] kljucevi;
    public Niz2(T[] niz, V[] kljucevi) : base(niz) { this.kljucevi = kljucevi; }
    public V VratiKljuc(int i) => kljucevi[i];
    public void Ispis2() => Console.WriteLine(string.Join(" ", kljucevi));
}

class Program
{
    static void Main()
    {
        char[] niz1 = { 'a', 'b', 'c' };
        double[] niz2 = { 1.1, 2.2, 3.3 };
        var obj = new Niz2<char, double>(niz1, niz2);
        obj.Ispis1();
        obj.Ispis2();
        int idx = int.Parse(Console.ReadLine());
        Console.WriteLine($"Vrednost: {obj.VratiVrednost(idx)}");
        Console.WriteLine($"Kljuc: {obj.VratiKljuc(idx)}");
    }
}
```

## 28–33. Prosleđivanje podataka između formi
- **Svojstva (roditelj → child)**: u child formi napraviti javno svojstvo, postaviti ga pre `ShowDialog`.
- **Konstruktor (roditelj → child)**: proslediti vrednost kroz konstruktor child forme.
- **Prosleđivanje objekta forme**: proslediti referencu na roditeljsku formu i pozvati njene metode/svojstva.
- **Delegat (roditelj → child)**: proslediti delegat koji child poziva kada treba podatak.
- **Svojstva (child → roditelj)**: roditelj čita javno svojstvo child forme posle zatvaranja.
- **Bez svojstava (child → roditelj)**: koristiti događaj ili povratnu vrednost iz `DialogResult` sa javnim poljima.

## 34/38/42. Forma za studente
- **Klasa Student**
```csharp
public class Student
{
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string BrojIndeksa { get; set; }
    public DateTime DatumRodjenja { get; set; }
    public string Smer { get; set; }
    public override string ToString() => $"{Ime} {Prezime} ({BrojIndeksa}), {Smer}, {DatumRodjenja:dd.MM.yyyy}";
}
```
- **FormStudenti**: `ListBox lst; Button btnDodaj, btnObrisi, btnObrisiSve;` Dugmad manipulišu listom studenata; `btnDodaj` otvara `FormUnosNovogStudenta` modalno i dodaje rezultat u listu.
- **FormUnosNovogStudenta**: sadrži `TextBox`e za ime, prezime, broj indeksa, `ComboBox` sa smerovima i `DateTimePicker` za datum; na `Snimi` formira `Student` objekat dostupan roditelju (kroz svojstvo).

## 35/40/44. MDI aplikacija sa menijem za otvaranje dokumenata
- Glavna forma je `IsMdiContainer = true`; meni sadrži stavke **Fajl** (Otvori Excel, sliku, Word, Exit) i **O programu** (čita txt i prikazuje u `MessageBox`).
- Stavke imaju `ShortcutKeys` kako je zadato; otvaranje fajlova koristi `OpenFileDialog` i prikazuje sadržaj u novom `Child` prozoru sa `RichTextBox` ili `PictureBox`.

## 36/39/43. MDI aplikacija – izbor ispisa i raspored prozora
- Stavka **Izbor ispisa** kreira child formu i popunjava `RichTextBox` vrednostima `Math.Sqrt(i)`, `i*i` ili `i*i*i` za `i=1..N` prema izabranoj podstavci.
- Stavka **Window** koristi `LayoutMdi(MdiLayout.Cascade/TileHorizontal/TileVertical)`.
- Stavka **O programu** prikazuje tekst iz datoteke u `MessageBox`.

## 37/41. Klasičan WordPad (KWA) primer
- Meni **File** (New/Open/Save/Save As/Close/Exit), **Edit** (Cut/Copy/Paste) i **Help**. Koristi `RichTextBox`, `OpenFileDialog`, `SaveFileDialog` i kontekstni meni sa istim komandama.
- Svaka stavka poziva odgovarajuću metodu `richTextBox1.Cut()`, `Copy()`, `Paste()`, `LoadFile()`, `SaveFile()` itd.

## 28–33 primer koda (delegat child→roditelj)
```csharp
// U roditeljskoj formi
public partial class FormParent : Form
{
    public FormParent()
    {
        InitializeComponent();
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
        var child = new FormChild();
        child.Prosledi = txt => MessageBox.Show($"Stiglo: {txt}");
        child.ShowDialog();
    }
}

// U child formi
public partial class FormChild : Form
{
    public Action<string> Prosledi { get; set; }
    private void btnSend_Click(object sender, EventArgs e) => Prosledi?.Invoke(textBox1.Text);
}
```
