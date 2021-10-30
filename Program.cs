using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * Модель компьютера характеризуется кодом и названием марки компьютера, типом процессора, частотой работы процессора,
 * объемом оперативной памяти, объемом жесткого диска, объемом памяти видеокарты, стоимостью компьютера в условных единицах
 * и количеством экземпляров, имеющихся в наличии. Создать список, содержащий 6-10 записей с различным набором значений характеристик.
Определить:
- все компьютеры с указанным процессором. Название процессора запросить у пользователя;
- все компьютеры с объемом ОЗУ не ниже, чем указано. Объем ОЗУ запросить у пользователя;
- вывести весь список, отсортированный по увеличению стоимости;
- вывести весь список, сгруппированный по типу процессора;
- найти самый дорогой и самый бюджетный компьютер;
- есть ли хотя бы один компьютер в количестве не менее 30 штук?
 */

namespace Zadanie_19
{
    class Computer
    {
        public int CompId { get; set; }
        public string CompMark { get; set; }
        public string CompProcessor { get; set; }
        public int CompProcessorFreq { get; set; }
        public int CompRAMCapacity { get; set; }
        public int CompHDDCapacity { get; set; }
        public int CompVCardCapacity { get; set; }
        public int CompPrice { get; set; }
        public int CompQuantity { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задание 19. Использование LINQ");
            Console.WriteLine("------------------------------");
            List<Computer> listComputers = new List<Computer>()
            {
                new Computer(){CompId=1,CompMark="Dell",CompProcessor="Intel",CompProcessorFreq=3000,CompRAMCapacity=16,CompHDDCapacity=500,CompVCardCapacity=8,CompPrice=500,CompQuantity=21},
                new Computer(){CompId=2,CompMark="HP",CompProcessor="Intel",CompProcessorFreq=3500,CompRAMCapacity=16,CompHDDCapacity=1000,CompVCardCapacity=6,CompPrice=1000,CompQuantity=8},
                new Computer(){CompId=3,CompMark="Acer",CompProcessor="AMD",CompProcessorFreq=3800,CompRAMCapacity=8,CompHDDCapacity=500,CompVCardCapacity=2,CompPrice=400,CompQuantity=45},
                new Computer(){CompId=4,CompMark="Asus",CompProcessor="Intel",CompProcessorFreq=4000,CompRAMCapacity=32,CompHDDCapacity=1000,CompVCardCapacity=4,CompPrice=1500,CompQuantity=36},
                new Computer(){CompId=5,CompMark="MSI",CompProcessor="AMD",CompProcessorFreq=2800,CompRAMCapacity=32,CompHDDCapacity=1500,CompVCardCapacity=6,CompPrice=1300,CompQuantity=16},
                new Computer(){CompId=6,CompMark="Apple",CompProcessor="Intel",CompProcessorFreq=4200,CompRAMCapacity=64,CompHDDCapacity=2000,CompVCardCapacity=4,CompPrice=2000,CompQuantity=3}
            };

            //все компьютеры с указанным процессором
            Console.Write("Какой процессор Вас интересует: ");
            List<string> listProc = listComputers.Select(c => c.CompProcessor).Distinct().ToList();
            foreach (string p in listProc)
                Console.Write("\t" + p);
            Console.WriteLine();
            string userProc = Console.ReadLine();

            List<Computer> listComp = (from c in listComputers
                                       where (c.CompProcessor == userProc)
                                       select c).ToList();
            CompPrint(listComp);

            // все компьютеры и объемом ОЗУ не ниже, чем указано
            Console.Write("Какой минимальный объем ОЗУ (Гб) Вас интересует: ");
            List<int> listRAM = listComputers.OrderBy(c => c.CompRAMCapacity).Select(c => c.CompRAMCapacity).Distinct().ToList();
            foreach (int ram in listRAM)
                Console.Write("\t" + ram);
            Console.WriteLine();
            int userRAM = Convert.ToInt32(Console.ReadLine());

            listComp = listComputers.Where(c => c.CompRAMCapacity >= userRAM).ToList();
            CompPrint(listComp);
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();

            // все компьютеры по стоимости
            Console.WriteLine("Компьютеры на складе (по увеличению стоимости)");
            listComp = listComputers.OrderBy(c => c.CompPrice).ToList();
            CompPrint(listComp);

            //группировка по типу процессора
            Console.WriteLine("Компьютеры на складе (по типу процессора)");
            var groupComp = listComputers.GroupBy(c=>c.CompProcessor).ToList();
            foreach (IGrouping<string,Computer> g in groupComp)
            {
                Console.WriteLine(g.Key);
                foreach (var c in g)
                    Console.WriteLine($"{c.CompId}\t{c.CompMark}\t{c.CompProcessor}\t{c.CompProcessorFreq}\t{c.CompRAMCapacity}\t{c.CompHDDCapacity}\t{c.CompVCardCapacity}\t{c.CompPrice}\t{c.CompQuantity}");
                Console.WriteLine();
            }

            // самый дорогой и самый бюджетный компьютер
            Console.WriteLine("Самый дорогой вариант");
            int maxPrice = listComputers.Max(c => c.CompPrice);
            listComp = listComputers
                .Where(c=>c.CompPrice==maxPrice)
                .ToList();
            CompPrint(listComp);

            Console.WriteLine("Самый бюджетный вариант");
            int minPrice = listComputers.Min(c => c.CompPrice);
            listComp = listComputers
                .Where(c => c.CompPrice == minPrice)
                .ToList();
            CompPrint(listComp);

            //хотя бы один компьютер в количестве не менее 30 штук
            Console.WriteLine("Самый доступный вариант (на складе 30 шт. и более)");
            listComp = listComputers.Where(c => c.CompQuantity >= 30).ToList();
            CompPrint(listComp);

            Console.ReadKey();
        }

        public static void CompPrint (List<Computer> listComp)
        {
            Console.WriteLine("Доступно:");
            Console.WriteLine("Код\tМарка\tПроц.\tЧастота\tRAM\tHDD\tVRAM\tЦена\tКол-во");
            foreach (Computer c in listComp)
                Console.WriteLine($"{c.CompId}\t{c.CompMark}\t{c.CompProcessor}\t{c.CompProcessorFreq}\t{c.CompRAMCapacity}\t{c.CompHDDCapacity}\t{c.CompVCardCapacity}\t{c.CompPrice}\t{c.CompQuantity}");
            Console.WriteLine();
            Console.WriteLine();
        }

    }
}
