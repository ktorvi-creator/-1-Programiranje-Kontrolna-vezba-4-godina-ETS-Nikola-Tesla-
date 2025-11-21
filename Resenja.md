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
    static T Saberi<T>(dynamic a, dynamic b) => (T)(a + b);

    static void Main()
    {
        Console.WriteLine(Saberi<int>(2, 3));
        Console.WriteLine(Saberi<double>(2.5, 1.2));
    }
}
```

## 11. MessageBox sa dugmadima i helpom (Windows Forms)
```csharp
using System.Windows.Forms;

MessageBox.Show("Tekst poruke", "Naslov", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button2, 0, true);
// Pretpostavlja se da je HelpProvider podešen na formi za prikaz fajla pomoći.
```

## 12. Generička klasa sa jednim tipom + Student
```csharp
using System;

class Box<T>
{
    private T data;
    public Box(T value) => data = value;
    public void Print() => Console.WriteLine($"{data} ({typeof(T).Name})");
}

class Student
{
    private int indeks, godina;
    private string ime;
    public Student(int indeks, int godina, string ime)
    { this.indeks = indeks; this.godina = godina; this.ime = ime; }
    public override string ToString() => $"{ime}, {indeks}/{godina}";
}

class Program
{
    static void Main()
    {
        new Box<int>(5).Print();
        new Box<double>(3.4).Print();
        new Box<string>("tekst").Print();
        new Box<Student>(new Student(123, 4, "Ana")).Print();
    }
}
```

## 13. Generička klasa sa dva tipa
```csharp
using System;

class Pair<T, U>
{
    public T First { get; }
    public U Second { get; }
    public Pair(T first, U second) { First = first; Second = second; }
    public void Print() => Console.WriteLine($"{First} ({typeof(T).Name}), {Second} ({typeof(U).Name})");
}

class Program
{
    static void Main()
    {
        new Pair<char, char>('a', 'b').Print();
    }
}
```

## 14. Klasa Point<T>
```csharp
using System;

class Point<T>
{
    public T X { get; set; }
    public T Y { get; set; }
    public Point(T x, T y) { X = x; Y = y; }
    public void Print() => Console.WriteLine($"({X}, {Y})");
}

class Program
{
    static void Main()
    {
        var p1 = new Point<int>(1, 2);
        var p2 = new Point<double>(1.5, 3.2);
        p1.Print();
        p2.Print();
    }
}
```

## 15. Klasa Line<T>
```csharp
using System;

class Line<T>
{
    public T X1 { get; set; }
    public T Y1 { get; set; }
    public T X2 { get; set; }
    public T Y2 { get; set; }
    public Line(T x1, T y1, T x2, T y2)
    { X1 = x1; Y1 = y1; X2 = x2; Y2 = y2; }
    public void Print() => Console.WriteLine($"({X1},{Y1})-({X2},{Y2})");
}

class Program
{
    static void Main()
    {
        new Line<float>(0, 0, 1, 1).Print();
        new Line<long>(-1, -2, 3, 4).Print();
    }
}
```

## 16. Generička klasa sa tipovima T1 i T2
```csharp
using System;

class Duo<T1, T2>
{
    public T1 A { get; set; }
    public T2 B { get; set; }
    public Duo(T1 a, T2 b) { A = a; B = b; }
    public void Print() => Console.WriteLine($"{A} ({typeof(T1).Name}), {B} ({typeof(T2).Name})");
}

class Program
{
    static void Main()
    {
        new Duo<int, string>(1, "jedan").Print();
        new Duo<char, bool>('c', true).Print();
        new Duo<float, short>(1.1f, 2).Print();
        new Duo<double, double>(2.3, 4.5).Print();
    }
}
```

## 17. Četvorougao (generički)
```csharp
using System;

class Cetvorougao<T>
{
    public T A { get; set; }
    public T B { get; set; }
    public T H { get; set; }
    public Cetvorougao() { }
    public Cetvorougao(T a, T b, T h) { A = a; B = b; H = h; }
    public double Povrsina() => Convert.ToDouble(A) * Convert.ToDouble(H) / 2 + Convert.ToDouble(B) * Convert.ToDouble(H) / 2;
}

class Program
{
    static void Main()
    {
        var c1 = new Cetvorougao<short>(3, 4, 2);
        var c2 = new Cetvorougao<float>(2.5f, 3.5f, 1.5f);
        Console.WriteLine(c1.Povrsina());
        Console.WriteLine(c2.Povrsina());
    }
}
```

## 18. Generička klasa sa metodom koja vraća podatak
```csharp
using System;

class Holder<T>
{
    private T data;
    public Holder(T value) { data = value; }
    public T GetValue() => data;
    public void PrintOther<U>(U value) => Console.WriteLine(value);
}

class Program
{
    static void Main()
    {
        var h1 = new Holder<int>(42);
        var h2 = new Holder<string>("tekst");
        Console.WriteLine(h1.GetValue());
        Console.WriteLine(h2.GetValue());
        h1.PrintOther("drugo");
        h2.PrintOther(3.14f);
    }
}
```

## 19. Generička osnovna i izvedena klasa
```csharp
using System;

class Base<T>
{
    public T Data { get; set; }
    public Base(T data) { Data = data; }
}

class Derived<T> : Base<T>
{
    public T Extra { get; set; }
    public Derived(T data, T extra) : base(data) { Extra = extra; }
}

class Program
{
    static void Main()
    {
        var obj = new Derived<int>(1, 2);
        Console.WriteLine($"Data={obj.Data}, Extra={obj.Extra}");
    }
}
```

## 20. Generički delegati sa različitim rezultatima
```csharp
using System;

delegate TRez Del<TArg, TRez>(TArg arg);

class Program
{
    static string F1<T>(T x) => $"Prvi: {x}";
    static int F2<T>(T x) => x.GetHashCode();

    static void Main()
    {
        Del<int, string> d1 = F1;
        Del<double, int> d2 = F2;
        Console.WriteLine(d1(5));
        Console.WriteLine(d2(3.14));
    }
}
```

## 21. Tipizirani delegat za realne operacije
```csharp
using System;

delegate double RealOp(double a, double b);
```

## 22. Delegat sa četiri funkcije
```csharp
using System;

delegate double Kalk(double a, double b);

class Program
{
    static double Saberi(double a, double b) => a + b;
    static double Oduzmi(double a, double b) => a - b;
    static double Pomnozi(double a, double b) => a * b;
    static double Podeli(double a, double b) => a / b;

    static void Main()
    {
        Kalk d = Saberi;
        Console.WriteLine(d(2, 3));
        d = Oduzmi; Console.WriteLine(d(5, 1));
        d = Pomnozi; Console.WriteLine(d(3, 4));
        d = Podeli; Console.WriteLine(d(8, 2));
    }
}
```

## 23. Negenerički interfejs sa generičkom metodom
```csharp
using System;

interface IInfo
{
    string GetInfo<T1, T2>(T1 a, T2 b);
}

class CInfo : IInfo
{
    public string GetInfo<T1, T2>(T1 a, T2 b) => $"Vrednosti: {a}, {b}; Tipovi: {typeof(T1).Name}, {typeof(T2).Name}";
}

class Program
{
    static void Main()
    {
        IInfo info = new CInfo();
        Console.WriteLine(info.GetInfo(3, 2.5));
        Console.WriteLine(info.GetInfo('c', 1.1f));
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
