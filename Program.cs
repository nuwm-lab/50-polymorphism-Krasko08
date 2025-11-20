using System;

namespace LabWork
{
    // =================== БАЗОВИЙ КЛАС: РІВНОСТОРОННІЙ ТРИКУТНИК ===================
    class EquilateralTriangle
    {
        public double Side;     // довжина сторони
        public double Angle;    // всі кути рівні = 60°

        public double PerimeterValue;
        public double Height;
        public double Area;

        // введення значення сторони
        public virtual void Init()
        {
            Console.WriteLine("Введіть довжину сторони рівностороннього трикутника:");
            Side = Convert.ToDouble(Console.ReadLine());
            Angle = 60;
        }

        // обчислення інших характеристик
        public virtual void Calculate()
        {
            Height = Side * Math.Sqrt(3) / 2;
            Area = (Side * Side * Math.Sqrt(3)) / 4;
            PerimeterValue = 3 * Side;
        }

        // виведення результатів
        public virtual void Show()
        {
            Console.WriteLine("\n=== Рівносторонній трикутник ===");
            Console.WriteLine($"Сторона a = {Side}");
            Console.WriteLine($"Кути = {Angle}°");
            Console.WriteLine($"Висота = {Height:F3}");
            Console.WriteLine($"Площа = {Area:F3}");
            Console.WriteLine($"Периметр = {PerimeterValue:F3}\n");
        }
    }


    // =================== ПОХІДНИЙ КЛАС: ДОВІЛЬНИЙ ТРИКУТНИК ===================
    class Triangle : EquilateralTriangle
    {
        public double Angle1, Angle2, Angle3;
        public double SideA, SideB, SideC;

        // введення значень
        public override void Init()
        {
            Console.WriteLine("Введіть довжину сторони (Side A):");
            SideA = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введіть два прилеглі кути (Angle1 та Angle2):");
            Angle1 = Convert.ToDouble(Console.ReadLine());
            Angle2 = Convert.ToDouble(Console.ReadLine());

            Angle3 = 180 - Angle1 - Angle2;
        }

        // обчислення інших сторін за теоремою синусів
        public override void Calculate()
        {
            // за теоремою синусів
            double rad = Math.PI / 180.0;

            SideB = SideA * Math.Sin(Angle2 * rad) / Math.Sin(Angle3 * rad);
            SideC = SideA * Math.Sin(Angle1 * rad) / Math.Sin(Angle3 * rad);

            PerimeterValue = SideA + SideB + SideC;
        }

        public override void Show()
        {
            Console.WriteLine("\n=== Довільний трикутник ===");
            Console.WriteLine($"Сторона A = {SideA:F3}");
            Console.WriteLine($"Сторона B = {SideB:F3}");
            Console.WriteLine($"Сторона C = {SideC:F3}");
            Console.WriteLine($"Кут A = {Angle1}°");
            Console.WriteLine($"Кут B = {Angle2}°");
            Console.WriteLine($"Кут C = {Angle3}°");
            Console.WriteLine($"Периметр = {PerimeterValue:F3}\n");
        }
    }


    // ================================ ПРОГРАМА ===================================
    class Program
    {
        static void Main(string[] args)
        {
            int userSelect;

            do
            {
                Console.WriteLine("Виберіть тип фігури:");
                Console.WriteLine("0 — Рівносторонній трикутник");
                Console.WriteLine("1 — Трикутник (1 сторона + 2 кути)");
                Console.WriteLine("Інше — Вихід");

                userSelect = Convert.ToInt32(Console.ReadLine());
                EquilateralTriangle triangle;

                if (userSelect == 0)
                {
                    triangle = new EquilateralTriangle();
                }
                else if (userSelect == 1)
                {
                    triangle = new Triangle();
                }
                else
                {
                    return;
                }

                triangle.Init();
                triangle.Calculate();
                triangle.Show();

            } while (true);
        }
    }
}
