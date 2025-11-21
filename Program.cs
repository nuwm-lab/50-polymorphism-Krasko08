using System;

namespace LabWork
{
    // =================== БАЗОВИЙ КЛАС: РІВНОСТОРОННІЙ ТРИКУТНИК ===================
    class EquilateralTriangle
    {
        public double Side;   // відома сторона
        public double Angle;  // усі кути по 60°
        public double Height;
        public double Area;
        public double PerimeterValue;

        // введення значення сторони та кута
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
            Console.WriteLine($"Висота = {Height:F2}");
            Console.WriteLine($"Площа = {Area:F2}");
            Console.WriteLine($"Периметр = {PerimeterValue:F2}");
        }
    }

    // =================== ПОХІДНИЙ КЛАС: ТРИКУТНИК ===================
    class Triangle : EquilateralTriangle
    {
        public double Angle1, Angle2, Angle3;
        public double SideA, SideB, SideC;

        // введення значення сторони і двох кутів
        public override void Init()
        {
            Console.WriteLine("Введіть довжину відомої сторони A:");
            SideA = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введіть кут при стороні A (Angle1):");
            Angle1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введіть другий прилеглий кут (Angle2):");
            Angle2 = Convert.ToDouble(Console.ReadLine());

            Angle3 = 180 - Angle1 - Angle2;
        }

        // обчислення інших сторін
        public override void Calculate()
        {
            double rad = Math.PI / 180;

            SideB = SideA * Math.Sin(Angle2 * rad) / Math.Sin(Angle3 * rad);
            SideC = SideA * Math.Sin(Angle1 * rad) / Math.Sin(Angle3 * rad);

            PerimeterValue = SideA + SideB + SideC;
        }

        // виведення результатів
        public override void Show()
        {
            Console.WriteLine("\n=== Довільний трикутник ===");
            Console.WriteLine($"Сторона A = {SideA:F2}");
            Console.WriteLine($"Сторона B = {SideB:F2}");
            Console.WriteLine($"Сторона C = {SideC:F2}");
            Console.WriteLine($"Кут A = {Angle1}°");
            Console.WriteLine($"Кут B = {Angle2}°");
            Console.WriteLine($"Кут C = {Angle3}°");
            Console.WriteLine($"Периметр = {PerimeterValue:F2}");
        }
    }

    // ============================= ПРОГРАМА =============================
    class Program
    {
        static void Main(string[] args)
        {
            int userSelect;
            EquilateralTriangle tri;

            do
            {
                Console.WriteLine("\nВиберіть фігуру:");
                Console.WriteLine("0 — Рівносторонній трикутник");
                Console.WriteLine("1 — Довільний трикутник");
                Console.WriteLine("Інше — Вихід");

                userSelect = Convert.ToInt32(Console.ReadLine());

                if (userSelect == 0)
                {
                    tri = new EquilateralTriangle();
                }
                else if (userSelect == 1)
                {
                    tri = new Triangle();
                }
                else
                {
                    return;
                }

                tri.Init();
                tri.Calculate();
                tri.Show();

            } while (true);
