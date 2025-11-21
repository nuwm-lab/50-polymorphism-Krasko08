using System;

namespace LabWork
{
    // Вхідна допоміжна логіка вводу
    static class InputHelper
    {
        public static double ReadDouble(string prompt, Func<double, bool> validator = null)
        {
            double value;
            while (true)
            {
                Console.WriteLine(prompt);
                var s = Console.ReadLine();
                if (double.TryParse(s, out value))
                {
                    if (validator == null || validator(value))
                        return value;
                    Console.WriteLine("Значення не пройшло перевірку. Спробуйте ще раз.");
                }
                else
                {
                    Console.WriteLine("Невірний ввід. Введіть число (наприклад 3.5).");
                }
            }
        }

        public static int ReadInt(string prompt)
        {
            int v;
            while (true)
            {
                Console.WriteLine(prompt);
                var s = Console.ReadLine();
                if (int.TryParse(s, out v)) return v;
                Console.WriteLine("Невірний ввід. Спробуйте ще раз.");
            }
        }
    }

    // Абстрактний базовий клас для кривих другого порядку / коників
    abstract class Conic
    {
        protected const double Epsilon = 1e-9;

        // Встановити коефіцієнти через вхід (розділення IO від математичної логіки частково забезпечено)
        public abstract void SetCoefficientsFromInput();

        // Показати коефіцієнти та основні характеристики
        public abstract void Show();

        // Перевіряє, чи належить точка кривій (в межах Epsilon)
        public abstract bool ContainsPoint(double x, double y);
    }

    // Еліпс у канонічній позиції: x^2/a^2 + y^2/b^2 = 1
    class Ellipse : Conic
    {
        public double A { get; private set; }
        public double B { get; private set; }

        public Ellipse() { }

        public Ellipse(double a, double b)
        {
            SetParameters(a, b);
        }

        public void SetParameters(double a, double b)
        {
            if (a <= 0 || b <= 0)
                throw new ArgumentException("Параметри еліпсу повинні бути додатними (a>0, b>0).");
            A = a;
            B = b;
        }

        public override void SetCoefficientsFromInput()
        {
            double a = InputHelper.ReadDouble("Введіть a (велика піввісь > 0):", v => v > 0);
            double b = InputHelper.ReadDouble("Введіть b (мала піввісь > 0):", v => v > 0);
            SetParameters(a, b);
        }

        public override void Show()
        {
            Console.WriteLine("\n--- Еліпс (канонічна форма) ---");
            Console.WriteLine($"a = {A}");
            Console.WriteLine($"b = {B}");
            Console.WriteLine($"Рівняння: x^2/{A}^2 + y^2/{B}^2 = 1 (в канонічній позиції)");
            Console.WriteLine($"Приблизний периметр (рамануджан) = {PerimeterApprox():F6}");
        }

        // Перевіряємо приналежність точки до еліпса
        public override bool ContainsPoint(double x, double y)
        {
            if (A == 0 || B == 0) return false;
            double val = (x * x) / (A * A) + (y * y) / (B * B) - 1.0;
            return Math.Abs(val) <= Epsilon;
        }

        // Приблизна довжина кола еліпса — формула Ramanujan (перша апрокс.)
        public double PerimeterApprox()
        {
            double a = A, b = B;
            return Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b)));
        }
    }

    // Загальна крива другого порядку: a11 x^2 + 2 a12 x y + a22 y^2 + b1 x + b2 y + c = 0
    class GeneralConic : Conic
    {
        public double A11 { get; private set; }
        public double A12 { get; private set; }
        public double A22 { get; private set; }
        public double B1 { get; private set; }
        public double B2 { get; private set; }
        public double C { get; private set; }

        public GeneralConic() { }

        public GeneralConic(double a11, double a12, double a22, double b1, double b2, double c)
        {
            A11 = a11; A12 = a12; A22 = a22; B1 = b1; B2 = b2; C = c;
        }

        public override void SetCoefficientsFromInput()
        {
            A11 = InputHelper.ReadDouble("a11:");
            A12 = InputHelper.ReadDouble("a12 (пів-коефіцієнт при xy):");
            A22 = InputHelper.ReadDouble("a22:");
            B1 = InputHelper.ReadDouble("b1 (коефіцієнт при x):");
            B2 = InputHelper.ReadDouble("b2 (коефіцієнт при y):");
            C = InputHelper.ReadDouble("c (вільний член):");
        }

        public override void Show()
        {
            Console.WriteLine("\n--- Загальна крива другого порядку ---");
            Console.WriteLine($"a11 = {A11}");
            Console.WriteLine($"a12 = {A12}");
            Console.WriteLine($"a22 = {A22}");
            Console.WriteLine($"b1 = {B1}");
            Console.WriteLine($"b2 = {B2}");
            Console.WriteLine($"c = {C}");
            Console.WriteLine("Форма: a11 x^2 + 2 a12 x y + a22 y^2 + b1 x + b2 y + c = 0");
        }

        public override bool ContainsPoint(double x, double y)
        {
            double val = A11 * x * x + 2.0 * A12 * x * y + A22 * y * y + B1 * x + B2 * y + C;
            return Math.Abs(val) <= Epsilon;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Програма: Еліпс та загальна крива другого порядку (Conic)");

            while (true)
            {
                Console.WriteLine("\nОберіть тип об'єкта:");
                Console.WriteLine("0 - Еліпс");
                Console.WriteLine("1 - Загальна крива другого порядку");
                Console.WriteLine("Інше - Вихід");

                int sel = InputHelper.ReadInt("Ваш вибір:");
                Conic conic;

                if (sel == 0)
                {
                    conic = new Ellipse();
                }
                else if (sel == 1)
                {
                    conic = new GeneralConic();
                }
                else
                {
                    return;
                }

                // Введення коефіцієнтів
                conic.SetCoefficientsFromInput();

                // Показати
                conic.Show();

                // Перевірка точки на належність
                Console.WriteLine("\nПеревірити належність точки кривій? (1 - так, 0 - ні)");
                int check = InputHelper.ReadInt("Ваш вибір:");
                if (check == 1)
                {
                    double x = InputHelper.ReadDouble("Введіть x:");
                    double y = InputHelper.ReadDouble("Введіть y:");
                    bool belongs = conic.ContainsPoint(x, y);
                    Console.WriteLine(belongs ? "Точка належить кривій (в межах точності)." : "Точка НЕ належить кривій.");
                }

                // Повернутися в цикл
            }
        }
    }
}
