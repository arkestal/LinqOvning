using System;
using System.Linq;
using System.Threading;

namespace ÖvningLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Person.AddPeopleToList(Person.People);
            Menu();
        }

        private static void Menu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.Write("Välkommen till namn-verkstan!" +
                    "\n\t[1] Förskriva frågor." +
                    "\n\t[2] Skriv in ett namn för att se om namnet matchar andra i listan." +
                    "\n\t[3] Skriv in ett datum för att se om daumet matchar andra i listan." +
                    "\n\t[4] Kolla hur många namn som börjar på varje bokstav." +
                    "\n\t[5] Olika val för namnsdag." +
                    "\n\t[6] Avsluta" +
                    "\n\tVälj: ");
                ConsoleKey key = Console.ReadKey().Key;
                Console.Clear();
                switch (key)
                {
                    case ConsoleKey.D1:
                        PreWrittenQuerys();
                        break;
                    case ConsoleKey.D2:
                        NameSearch();
                        break;
                    case ConsoleKey.D3:
                        DateSearch();
                        break;
                    case ConsoleKey.D4:
                        LetterSearch();
                        break;
                    case ConsoleKey.D5:
                        NameDayChoices();
                        break;
                    case ConsoleKey.D6:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltig inmatning!");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }

        private static void NameDayChoices()
        {
            bool isRunning4 = true;
            while (isRunning4)
            {
                Console.Clear();
                Console.Write("Kolla hur många som har namnsdag" +
                    "\n\t[1] Varje månad" +
                    "\n\t[2] Varje kvartal" +
                    "\n\t[3] De fem dagar som flest har namnsdag" +
                    "\n\t[4] Återgå till huvudmenyn" +
                    "\n\tVälj: ");
                ConsoleKey key4 = Console.ReadKey().Key;
                Console.Clear();
                switch (key4)
                {
                    case ConsoleKey.D1:
                        var q7 = Person.People
                            .OrderBy(p => p.NameDay.Month)
                            .GroupBy(p => p.NameDay.Month);

                        foreach (var month in q7)
                        {
                            int personCounter = 0;
                            Console.Write($"{month.Key}: ");
                            foreach (var person in month)
                            {
                                personCounter++;
                            }
                            Console.WriteLine($"{personCounter} namnsdagar.");
                        }
                        Console.Write("Tryck [ENTER] för att återgå till menyn: ");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D2:
                        var q8 = Person.People
                            .OrderBy(p => p.NameDay.Month)
                            .GroupBy(p => (p.NameDay.Month - 1) / 3);
                        int quarterCount = 0;
                        foreach (var item in q8)
                        {
                            int personCounter2 = 0;
                            quarterCount++;
                            Console.Write($"Kvartal {quarterCount}: ");
                            foreach (var person in item)
                            {
                                personCounter2++;
                            }
                            Console.WriteLine($"{personCounter2} namnsdagar.");
                        }
                        Console.Write($"\nTryck [ENTER] för att återgå till menyn: ");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D3:
                        var q9 = Person.People
                            .GroupBy(p => p.NameDay)
                            .OrderByDescending(p => p.Count())
                            .Take(5);
                        Console.WriteLine("De fem vanligaste namnsdagarna:\n");
                        foreach (var item in q9)
                        {
                            int personCounter3 = 0;
                            foreach (var person in item)
                            {
                                personCounter3++;
                            }
                            Console.WriteLine($"\t{item.Key.Day}/{item.Key.Month} med {personCounter3} namnsdagar.");
                        }
                        Console.Write($"\nTryck [ENTER] för att återgå till menyn: ");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D4:
                        isRunning4 = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltig inmatning!");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }

        private static void LetterSearch()
        {
            int counter = 0;
            Console.Write("Skriv in en bokstav för att se hur många namn som börjar på den bokstaven: ");
            ConsoleKey key3 = Console.ReadKey().Key;
            Console.WriteLine("\n");
            var q6 = Person.People
                .GroupBy(p => p.Name);
            foreach (var item in q6)
            {
                if (item.Key.StartsWith((char)key3))
                {
                    Console.WriteLine($"\t{item.Key}");
                    counter++;
                }
            }
            Console.Write($"\nDet finns {counter} namn som börjar med bokstaven '{key3}'." +
                $"\nTryck [ENTER] för att återgå till menyn.");
            Console.ReadLine();
        }

        private static void DateSearch()
        {
            int monthChoice = 0;
            int dayChoice = 0;
            bool isRunning3 = true;
            while (isRunning3)
            {
                Console.Clear();
                Console.Write("Skriv in en månad med siffror (1-12): ");
                try
                {
                    monthChoice = int.Parse(Console.ReadLine());
                    if (monthChoice < 1 || monthChoice > 12)
                    {
                        Console.Clear();
                        Console.WriteLine("Ogiltig inmatning!");
                        continue;
                    }
                    Console.Write("Skriv in datumet: ");
                    dayChoice = int.Parse(Console.ReadLine());
                    if (dayChoice < 1 || dayChoice > 31)
                    {
                        Console.Clear();
                        Console.WriteLine("Ogiltig inmatning!");
                        continue;
                    }
                    isRunning3 = false;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltig inmatning!");
                }
            }
            var q5 = Person.People
                .Where(p => p.NameDay.Month == monthChoice && p.NameDay.Day == dayChoice);
            Console.Clear();
            Console.WriteLine($"Följande personer har namnsdag den {dayChoice}/{monthChoice}.");
            foreach (var item in q5)
            {
                Console.WriteLine($"\t{item.Name}");
            }
            Console.Write("\nTryck [ENTER] för att återgå till menyn: ");
            Console.ReadLine();
        }

        private static void NameSearch()
        {
            Console.Write("Sök efter ett namn: ");
            string nameSearch = Console.ReadLine();
            var q4 = Person.People
                .Where(p => p.Name.ToLower().Contains(nameSearch));
            foreach (var item in q4)
            {
                Console.WriteLine($"{item.Name}  \tMånad: {item.NameDay.Month} Dag: {item.NameDay.Day}");
            }
            if (q4.Count() == 0)
            {
                Console.WriteLine("Ingen med detta namn finns i listan.");
            }
            Console.Write("\nTryck [ENTER] för att återgå till menyn: ");
            Console.ReadLine();
        }

        private static void PreWrittenQuerys()
        {
            bool isRunning2 = true;
            while (isRunning2)
            {
                Console.Clear();
                Console.Write("[1] Lista alla namn som börjar på 'And'." +
                    "\n[2] Lista alla som har namnsdag 23:e juli." +
                    "\n[3] Lista alla som har namn som börjar på 'P' och har namnsdag i april." +
                    "\n[4] Lista alla namn vars hund är långhårig." +
                    "\n[5] Återgå till huvudmenyn." +
                    "\nVälj: ");
                ConsoleKey key2 = Console.ReadKey().Key;
                Console.Clear();
                switch (key2)
                {
                    case ConsoleKey.D1:
                        var q1 = Person.People
                            .Where(p => p.Name.ToLower().StartsWith("and"));
                        foreach (var item in q1)
                        {
                            Console.WriteLine(item.Name);
                        }
                        Console.Write("\nTryck [ENTER] för att återgå till menyn: ");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D2:
                        var q2 = Person.People
                            .Where(p => p.NameDay.Day == 23 && p.NameDay.Month == 7);
                        Console.WriteLine("Dessa har namnsdag den 23:e juli:\n");
                        foreach (var item in q2)
                        {
                            Console.WriteLine(item.Name);
                        }
                        Console.Write("\nTryck [ENTER] för att återgå till menyn: ");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D3:
                        var q3 = Person.People
                            .Where(p => p.Name.StartsWith("P") && p.NameDay.Month == 4);
                        Console.WriteLine("Dessa börjar sitt namn med 'P' och har namnsdag i april:\n");
                        foreach (var item in q3)
                        {
                            Console.WriteLine($"{item.Name} har namnsdag den {item.NameDay.Day}");
                        }
                        Console.Write("\nTryck [ENTER] för att återgå till menyn: ");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D4:
                        Console.WriteLine("Ingen äger hund!");
                        Thread.Sleep(1500);
                        break;
                    case ConsoleKey.D5:
                        isRunning2 = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltig inmatning!");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }
    }
}
