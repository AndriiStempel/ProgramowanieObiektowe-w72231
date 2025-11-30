using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace ComplexNumber
{
    public interface IModular
    {
        double Modular();
    }
    public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
    {
        private double re;
        private double im;

        public double Re
        {
            get {return re;}
            set {re = value;}
        }
        public double Im
        {
            get {return im;}
            set {im = value;}
        }
        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }
        public override string ToString()
        {
            if (im > 0)
                return $"{re} + {im}i";
            else
                return $"{re}  {im}i";
        }

        // Dodawanie
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.re + b.re, a.im + b.im);
        }

        //Odejmowanie
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.re - b.re, a.im - b.im);
        }

        //Mnozenie
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double real = a.re * b.re - a.im * b.im;
            double ujem = a.re * b.im + a.im * b.re;
            return new ComplexNumber(real, ujem);
        }

        // Operator unarny
        public static ComplexNumber operator -(ComplexNumber a)
        {
            return new ComplexNumber(a.re, -a.im);
        }


        public object Clone()
        {
            return new ComplexNumber(this.re, this.im);
        }
        public bool Equals(ComplexNumber? other)
        {
            if (other == null)
                return false;
            return this.re == other.re && this.im == other.im;
        }

        public override bool Equals(object? obj)
        {
            if (obj is ComplexNumber other)
                return Equals(other);
            return false;
        }

    
        public override int GetHashCode()
        {
            return HashCode.Combine(re, im);
        }

        // Operator ==
        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(b);
        }

        // Operator !=
        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !(a == b);
        }

        // Implementacja IModular
        public double Module()
        {
            return Math.Sqrt((float)re * re + im * im);
        }

        public double Modular()
        {
            throw new NotImplementedException();
        }

        class Program
        {
            public static void Main(string[] args)
            {
                ComplexNumber a = new ComplexNumber(5.7, -2.1);
                ComplexNumber b = new ComplexNumber(-3.4, 6.8);

                Console.WriteLine("=== OPERACJE NA LICZBACH ZESPOLONYCH ===\n");

                Console.WriteLine($"Liczba A: {a}");
                Console.WriteLine($"Liczba B: {b}\n");

                Console.WriteLine("Suma: " + (a + b));
                Console.WriteLine("Różnica: " + (a - b));
                Console.WriteLine("Iloczyn: " + (a * b));

                Console.WriteLine($"\nSprzężenie liczby A: {(-a)}");

                Console.WriteLine($"\nModuł liczby A: {a.Module()}");

                ComplexNumber clone = a.Clone() as ComplexNumber;
                Console.WriteLine($"\nKopia liczby A: {clone}");

                Console.WriteLine("Czy A = B? " + (a == b));
                Console.WriteLine("Czy A nie rowne B? " + (a != b));

                Console.WriteLine("\nCzy kopia jest równa A? " + clone.Equals(a));
     
            }
        }
    }
}

