using System;
using System.Globalization;

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
    public class Ellipse : Conic
    {
        private double _a, _b;

        public Ellipse() { }

        public Ellipse(double a, double b)
        {
            _a = a;
            _b = b;
        }

        public override void SetCoefficients()
        {
            _a = ReadDouble("Введіть a: ");
            _b = ReadDouble("Введіть b: ");
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine($"Еліпс: x²/{_a}² + y²/{_b}² = 1");
        }

        public override bool Contains(double x, double y)
        {
            double v = (x * x) / (_a * _a) + (y * y) / (_b * _b);
            return Math.Abs(v - 1) < 0.0001 || v < 1;
        }

        private double ReadDouble(string message)
        {
            double value;
            Console.Write(message);
            while (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                Console.Write("Невірне значення. Спробуйте ще раз: ");
            }
            return value;
        }
    }

    // =================== КРИВА ДРУГОГО ПОРЯДКУ ==================
    public class SecondOrderCurve : Conic
    {
        private double _a11, _a12, _a22, _b1, _b2, _c;

        public SecondOrderCurve() { }

        public SecondOrderCurve(double a11, double a12, double a22, double b1, double b2, double c)
        {
            _a11 = a11;
            _a12 = a12;
            _a22 = a22;
            _b1 = b1;
            _b2 = b2;
            _c = c;
        }

        // Основний метод SetCoefficients
        public override void SetCoefficients()
        {
            _a11 = ReadDouble("Введіть a11: ");
            _a12 = ReadDouble("Введіть a12: ");
            _a22 = ReadDouble("Введіть a22: ");
            _b1 = ReadDouble("Введіть b1: ");
            _b2 = ReadDouble("Введіть b2: ");
            _c = ReadDouble("Введіть c: ");
        }

        // Перевантажений метод
        public void SetCoefficients(double a11, double a12, double a22, double b1, double b2, double c)
        {
            _a11 = a11;
            _a12 = a12;
            _a22 = a22;
            _b1 = b1;
            _b2 = b2;
            _c = c;
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine("Крива другого порядку:");
            Console.WriteLine($"{_a11}x² + 2*{_a12}xy + {_a22}y² + {_b1}x + {_b2}y + {_c} = 0");
        }

        public override bool Contains(double x, double y)
        {
            double v = _a11 * x * x + 2 * _a12 * x * y + _a22 * y * y + _b1 * x + _b2 * y + _c;
            return Math.Abs(v) < 0.0001;
        }

        private double ReadDouble(string message)
        {
            double value;
            Console.Write(message);
            while (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                Console.Write("Невірне значення. Спробуйте ще раз: ");
            }
            return value;
        }
    }

    // ============================ MAIN =============================
    class Program
    {
        static void Main(string[] args)
        {
            Conic[] conics = new Conic[2];

            Console.WriteLine("Створення еліпса:");
            conics[0] = new Ellipse();
            conics[0].SetCoefficients();
            conics[0].PrintCoefficients();

            Console.Write("Введіть точку X: ");
            double x = ReadDouble();
            Console.Write("Введіть точку Y: ");
            double y = ReadDouble();

            if (conics[0].Contains(x, y))
                Console.WriteLine("Точка належить еліпсу.");
            else
                Console.WriteLine("Точка НЕ належить еліпсу.");

            Console.WriteLine("\nСтворення кривої другого порядку:");
            conics[1] = new SecondOrderCurve();
            conics[1].SetCoefficients();
            conics[1].PrintCoefficients();

            if (conics[1].Contains(x, y))
                Console.WriteLine("Точка належить кривій другого порядку.");
            else
                Console.WriteLine("Точка НЕ належить цій кривій.");

            // Демонстрація поліморфізму через масив Conic[]
            Console.WriteLine("\nДемонстрація поліморфізму:");
            foreach (var c in conics)
            {
                c.PrintCoefficients();
            }
        }

        private static double ReadDouble()
        {
            double value;
            while (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                Console.Write("Невірне значення. Спробуйте ще раз: ");
            }
            return value;
        }
    }
}
