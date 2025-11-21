using System;

namespace LabWork
{
    // ======================= ІНТЕРФЕЙС =======================
    public interface IPrintable
    {
        void PrintCoefficients();
        bool Contains(double x, double y); 
    }

    // =================== АБСТРАКТНИЙ КЛАС ====================
    public abstract class Conic : IPrintable
    {
        public abstract void SetCoefficients();
        public abstract void PrintCoefficients();
        public abstract bool Contains(double x, double y);
    }

    // ======================== КЛАС ЕЛІПС ======================
    // Еліпс:  x^2/a^2 + y^2/b^2 = 1
    public class Ellipse : Conic
    {
        private double a, b;

        public override void SetCoefficients()
        {
            Console.Write("Введіть a: ");
            a = double.Parse(Console.ReadLine());

            Console.Write("Введіть b: ");
            b = double.Parse(Console.ReadLine());
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine($"Еліпс: x^2/{a * a} + y^2/{b * b} = 1");
        }

        public override bool Contains(double x, double y)
        {
            double v = (x * x) / (a * a) + (y * y) / (b * b);
            return Math.Abs(v - 1) < 0.0001 || v < 1; // точка на або в середині еліпса
        }
    }

    // =================== КРИВА ДРУГОГО ПОРЯДКУ ==================
    // a11x² + 2a12xy + a22y² + b1x + b2y + c = 0
    public class SecondOrderCurve : Conic
    {
        private double a11, a12, a22, b1, b2, c;

        public override void SetCoefficients()
        {
            Console.Write("Введіть a11: ");
            a11 = double.Parse(Console.ReadLine());
            Console.Write("Введіть a12: ");
            a12 = double.Parse(Console.ReadLine());
            Console.Write("Введіть a22: ");
            a22 = double.Parse(Console.ReadLine());
            Console.Write("Введіть b1: ");
            b1 = double.Parse(Console.ReadLine());
            Console.Write("Введіть b2: ");
            b2 = double.Parse(Console.ReadLine());
            Console.Write("Введіть c: ");
            c = double.Parse(Console.ReadLine());
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine($"Крива другого порядку:");
            Console.WriteLine($"{a11}x² + 2*{a12}xy + {a22}y² + {b1}x + {b2}y + {c} = 0");
        }

        public override bool Contains(double x, double y)
        {
            double v = a11 * x * x + 2 * a12 * x * y + a22 * y * y + b1 * x + b2 * y + c;
            return Math.Abs(v) < 0.0001;
        }
    }

    // ============================ MAIN =============================
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Створення еліпса:");
            Ellipse ellipse = new Ellipse();
            ellipse.SetCoefficients();
            ellipse.PrintCoefficients();

            Console.Write("Введіть точку X: ");
            double x = double.Parse(Console.ReadLine());
            Console.Write("Введіть точку Y: ");
            double y = double.Parse(Console.ReadLine());

            if (ellipse.Contains(x, y))
                Console.WriteLine("Точка належить еліпсу.");
            else
                Console.WriteLine("Точка НЕ належить еліпсу.");

            Console.WriteLine("\nСтворення кривої другого порядку:");
            SecondOrderCurve curve = new SecondOrderCurve();
            curve.SetCoefficients();
            curve.PrintCoefficients();

            if (curve.Contains(x, y))
                Console.WriteLine("Точка належить кривій другого порядку.");
            else
                Console.WriteLine("Точка НЕ належить цій кривій.");
        }
    }
}
