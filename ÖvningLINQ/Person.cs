using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ÖvningLINQ
{
    class Person
    {
        public static List<Person> People = new List<Person>();
        public string Name { get; set; }
        public DateTime NameDay { get; set; }

        public static void AddPeopleToList(List<Person> people)
        {
            const string filePath = @"C:\Users\Marcus A\source\repos\ÖvningLINQ\ÖvningLINQ\bin\Debug\netcoreapp3.1\names.csv";

            foreach (string person in File.ReadLines(filePath, Encoding.UTF7))
            {
                string[] personData = person.Split(';');

                if (PersonNotInList(personData[0], people))
                {
                    Person p = new Person
                    {
                        Name = personData[0],
                        NameDay = DateTime.Parse(personData[1])
                    };
                    people.Add(p);
                }
            }
        }

        private static bool PersonNotInList(string name, List<Person> people)
        {
            bool nameFound = false;

            foreach (Person person in people)
            {
                nameFound = person.Name.ToLower() == name.ToLower();
                if (nameFound)
                {
                    break;
                }
            }
            return !nameFound;
        }
        enum MonthCount
        {
            Januari = 1,
            Februari,
            Mars,
            April,
            Maj,
            Juni,
            Juli,
            Augusti,
            September,
            Oktober,
            November,
            December
        }
    }
}
