using MyApplication;
using System;

namespace MyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Zwierze piesio = new Pies("Bob");
            Zwierze kotek = new Kot("Murka");
            Zwierze wazik = new Waz("Szszsz");

            powiedzCos(kotek);
            powiedzCos(piesio);
            powiedzCos(wazik);

            Pracownik piekasz = new Piekarz();
            piekasz.Pracuj();

            A obiektA = new A();
            B obiektB = new B();
            C obiektC = new C();
        }

        public static void powiedzCos(Zwierze g)
        {
            g.DajGlos();
            Console.WriteLine("Typ obiektu: " + g.GetType().Name);
        }
    }

    class Zwierze
    {
        protected string nazwa;

        public Zwierze(string nazwa)
        {
            this.nazwa = nazwa;
        }

        public virtual void DajGlos()
        {
            Console.WriteLine($"{nazwa} wydaje jakiś dźwięk...");
        }
    }

    class Pies : Zwierze
    {
        public Pies(string nazwa) : base(nazwa) { }

        public override void DajGlos()
        {
            Console.WriteLine($"{nazwa} robi: Woof woof!");
        }
    }

    class Kot : Zwierze
    {
        public Kot(string nazwa) : base(nazwa) { }

        public override void DajGlos()
        {
            Console.WriteLine($"{nazwa} robi: Miau miau!");
        }
    }

    class Waz : Zwierze
    {
        public Waz(string nazwa) : base(nazwa) { }

        public override void DajGlos()
        {
            Console.WriteLine($"{nazwa} robi: Ssssss!");
        }
    }

    public abstract class Pracownik
    {
        public abstract void Pracuj();
    }

    class Piekarz : Pracownik
    {
        public override void Pracuj()
        {
            Console.WriteLine("Trwa pieczenie…");
        }
    }

    class A
    {
        public A()
        {
            Console.WriteLine("To jest konstruktor A");
        }
    }

    class B : A
    {
        public B() : base()
        {
            Console.WriteLine("To jest konstruktor B");
        }
    }

    class C : B
    {
        public C() : base()
        {
            Console.WriteLine("To jest konstruktor C");
        }
    }
}
