using System;
using System.Linq;

namespace Lab5_AllTasks
{
    // ========================================================================
    // КЛАСИ ДЛЯ ЗАВДАННЯ 1 ТА 2 (Ієрархія видань + Конструктори/Деструктори)
    // ========================================================================
    
    public abstract class PrintedPublication
    {
        public string Title { get; set; }
        public int Year { get; set; }

        // Конструктори (мінімум 3)
        protected PrintedPublication() { Title = "Без назви"; Year = 0; Console.WriteLine("[+] Створено базове видання (0 парам)"); }
        protected PrintedPublication(string title) { Title = title; Year = 0; Console.WriteLine($"[+] Створено базове видання: {Title} (1 парам)"); }
        protected PrintedPublication(string title, int year) { Title = title; Year = year; Console.WriteLine($"[+] Створено базове видання: {Title}, {Year} (2 парам)"); }

        // Деструктор
        ~PrintedPublication() { Console.WriteLine($"[-] Знищено базове видання: {Title}"); }

        // Абстрактний метод
        public abstract void Show();
    }

    public class Book : PrintedPublication
    {
        public string Author { get; set; }

        public Book() : base() { Author = "Невідомий"; Console.WriteLine("  -> Створено Книгу (0 парам)"); }
        public Book(string title, string author) : base(title) { Author = author; Console.WriteLine($"  -> Створено Книгу (2 парам): {Author}"); }
        public Book(string title, int year, string author) : base(title, year) { Author = author; Console.WriteLine($"  -> Створено Книгу (3 парам): {Author}"); }

        ~Book() { Console.WriteLine($"  [-] Знищено Книгу: {Title}"); }

        public override void Show() => Console.WriteLine($"[Книга] \"{Title}\", Автор: {Author}, Рік: {Year}");
    }

    public class Textbook : Book
    {
        public string Subject { get; set; }

        public Textbook() : base() { Subject = "Загальний"; Console.WriteLine("    -> Створено Підручник (0 парам)"); }
        public Textbook(string title, string author, string subject) : base(title, author) { Subject = subject; Console.WriteLine($"    -> Створено Підручник (3 парам): {Subject}"); }
        public Textbook(string title, int year, string author, string subject) : base(title, year, author) { Subject = subject; Console.WriteLine($"    -> Створено Підручник (4 парам): {Subject}"); }

        ~Textbook() { Console.WriteLine($"    [-] Знищено Підручник: {Title}"); }

        public override void Show() => Console.WriteLine($"[Підручник] \"{Title}\" з предмета {Subject}, Автор: {Author}, Рік: {Year}");
    }

    public class Magazine : PrintedPublication
    {
        public int IssueNumber { get; set; }

        public Magazine() : base() { IssueNumber = 1; Console.WriteLine("  -> Створено Журнал (0 парам)"); }
        public Magazine(string title, int issue) : base(title) { IssueNumber = issue; Console.WriteLine($"  -> Створено Журнал (2 парам): №{IssueNumber}"); }
        public Magazine(string title, int year, int issue) : base(title, year) { IssueNumber = issue; Console.WriteLine($"  -> Створено Журнал (3 парам): №{IssueNumber}"); }

        ~Magazine() { Console.WriteLine($"  [-] Знищено Журнал: {Title}"); }

        public override void Show() => Console.WriteLine($"[Журнал] \"{Title}\" №{IssueNumber}, Рік: {Year}");
    }


    // ========================================================================
    // КЛАСИ ДЛЯ ЗАВДАННЯ 3 (Абстрактна Фігура та її нащадки)
    // ========================================================================
    
    public abstract class Figure
    {
        public string Name { get; set; }
        public Figure(string name) { Name = name; }

        public abstract double GetArea();
        public abstract double GetPerimeter();

        public virtual void ShowInfo()
        {
            Console.WriteLine($"--- {Name} ---");
            Console.WriteLine($"Периметр: {GetPerimeter():F2} | Площа: {GetArea():F2}");
        }
    }

    public class Rectangle : Figure
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(double w, double h) : base("Прямокутник") { Width = w; Height = h; }
        public override double GetPerimeter() => 2 * (Width + Height);
        public override double GetArea() => Width * Height;
        public override void ShowInfo() { base.ShowInfo(); Console.WriteLine($"Сторони: {Width} та {Height}\n"); }
    }

    public class Circle : Figure
    {
        public double Radius { get; set; }
        public Circle(double r) : base("Коло") { Radius = r; }
        public override double GetPerimeter() => 2 * Math.PI * Radius;
        public override double GetArea() => Math.PI * Math.Pow(Radius, 2);
        public override void ShowInfo() { base.ShowInfo(); Console.WriteLine($"Радіус: {Radius}\n"); }
    }

    public class Triangle : Figure
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public Triangle(double a, double b, double c) : base("Трикутник") { A = a; B = b; C = c; }
        public override double GetPerimeter() => A + B + C;
        public override double GetArea()
        {
            double p = GetPerimeter() / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }
        public override void ShowInfo() { base.ShowInfo(); Console.WriteLine($"Сторони: {A}, {B}, {C}\n"); }
    }


    // ========================================================================
    // КЛАС ДЛЯ ЗАВДАННЯ 4 (Запечатаний частковий клас Point)
    // ========================================================================
    
    // ЧАСТИНА 1: Оголошення полів та методів
    public sealed partial class Point
    {
        private int x, y, c;
        public Point() { x = 0; y = 0; c = 0; }
        public Point(int x, int y, int c) { this.x = x; this.y = y; this.c = c; }
        
        // Спеціальний частковий метод-заглушка (оголошення)
        partial void PrintInternal();

        // Публічний метод, який викликає заглушку
        public void Print() => PrintInternal();
    }

    // ЧАСТИНА 2: Реалізація методів та операторів
    public sealed partial class Point
    {
        // Реалізація часткового методу
        partial void PrintInternal()
        {
            Console.WriteLine($"Точка [X={x}, Y={y}] | Колір: {c}");
        }

        public static Point operator ++(Point p) { p.x++; p.y++; return p; }
        public static bool operator true(Point p) => p.x == p.y;
        public static bool operator false(Point p) => p.x != p.y;
    }


    // ========================================================================
    // ГОЛОВНА ПРОГРАМА З МЕНЮ
    // ========================================================================
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("                 ЛАБОРАТОРНА №5                   ");
                Console.WriteLine("==================================================");
                Console.WriteLine(" 1 - Завдання 1 (Ієрархія видань: сортування та вивід)");
                Console.WriteLine(" 2 - Завдання 2 (Видання: конструктори та деструктори)");
                Console.WriteLine(" 3 - Завдання 3 (Абстрактна фігура: площа і периметр)");
                Console.WriteLine(" 4 - Завдання 4 (Point: частковий та запечатаний клас)");
                Console.WriteLine("==================================================");
                Console.WriteLine("[Натисніть ENTER без вводу для виходу]");
                Console.Write("Оберіть завдання: ");

                string choice = Console.ReadLine();

                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Програму завершено.");
                    break;
                }

                Console.WriteLine();

                switch (choice)
                {
                    case "1": RunTask1(); break;
                    case "2": RunTask2(); break;
                    case "3": RunTask3(); break;
                    case "4": RunTask4(); break;
                    default: Console.WriteLine("Помилка! Такого пункту немає."); break;
                }

                Console.WriteLine("\n[Натисніть будь-яку клавішу для повернення в меню...]");
                Console.ReadKey();
            }
        }

        static void RunTask1()
        {
            Console.WriteLine("--- ВИКОНАННЯ ЗАВДАННЯ 1 (Сортування видань) ---");
            PrintedPublication[] library = new PrintedPublication[]
            {
                new Book("1984", 2021, "Джордж Орвелл"),
                new Magazine("Forbes Україна", 2024, 4),
                new Textbook("Алгебра", 2020, "О.С. Істер", "Математика"),
                new Book("Кобзар", 2015, "Тарас Шевченко"),
                new Magazine("National Geographic", 2023, 11)
            };

            Console.WriteLine("\n=== ВІДСОРТОВАНИЙ МАСИВ ЗА РОКОМ ВИДАННЯ ===");
            var sortedLibrary = library.OrderBy(p => p.Year).ToArray();
            
            foreach (var pub in sortedLibrary)
            {
                pub.Show(); 
            }
        }

        static void RunTask2()
        {
            Console.WriteLine("--- ВИКОНАННЯ ЗАВДАННЯ 2 (Конструктори та Деструктори) ---\n");
            
            PrintedPublication[] library = new PrintedPublication[]
            {
                new Book(),                                          
                new Magazine("National Geographic", 11),                       
                new Textbook("Історія України", 2019, "В. Власов", "Історія")  
            };

            Console.WriteLine("\n=== ЗНИЩУЄМО ОБ'ЄКТИ ТА КЛИЧЕМО GARBAGE COLLECTOR ===");
            library = null;
            GC.Collect();
            GC.WaitForPendingFinalizers(); 
            // Ви побачите як в консолі спрацюють деструктори (методи з ~)
        }

        static void RunTask3()
        {
            Console.WriteLine("--- ВИКОНАННЯ ЗАВДАННЯ 3 (Фігури) ---\n");
            Figure[] figures = new Figure[]
            {
                new Rectangle(5, 10),
                new Circle(7.5),
                new Triangle(3, 4, 5)
            };

            foreach (Figure fig in figures)
            {
                fig.ShowInfo();
            }
        }

        static void RunTask4()
        {
            Console.WriteLine("--- ВИКОНАННЯ ЗАВДАННЯ 4 (Частковий клас Point) ---\n");
            
            // Створюємо точку (Код конструктора в ЧАСТИНІ 1)
            Point p = new Point(5, 5, 1);
            
            // Виводимо (Код реалізації Print знаходиться в ЧАСТИНІ 2)
            Console.Write("Початкова точка: ");
            p.Print();

            p++;
            Console.Write("Після p++: ");
            p.Print();

            Console.WriteLine($"Чи рівні X та Y? -> {(p ? "Так" : "Ні")}");
        }
    }
}