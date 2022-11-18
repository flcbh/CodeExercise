using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeExcercise
{
    [TestClass]
    public class MyTestClass : IMyTestClass
    {
        [TestMethod]
        public void Test1()
        {
            var pets = new List<Pet>()
            {
                new Dog() { Firstname = "Jim", Lastname = "Rogers", NumberofVisits = 5, JoinedPractice = DateTime.Now},
                new Dog() { Firstname = "Tony", Lastname = "Smith", NumberofVisits = 10, JoinedPractice = new DateTime(1985, 7, 13)},
                new Cat() { Firstname = "Steve", Lastname = "Roberts", NumberofVisits = 20, JoinedPractice = new DateTime(2002, 5, 6), NumberOfLives = 9 }
            };

            new Pet().printReport(pets, "PetsReport.csv");
            var outPets = File.ReadAllLines("PetsReport.csv");

            Assert.AreEqual(4, outPets.Count());
        }
    }

    public class Pet : Owner
    {
        //Encapsulate fields and use properties.
        private int numberofVisits;
        private DateTime joinedPractice;

        public int NumberofVisits { get => numberofVisits; set => numberofVisits = value; }
        public DateTime JoinedPractice { get => joinedPractice; set => joinedPractice = value; }

        // NOTE: This method prints a pets reports in csv format.
        public void printReport(IEnumerable<Pet> pets, string filename)
        {
            List<string> entries = new List<string>();
            entries.Add("Owners name,Date Joined Practice,Number Of Visits,Number of Lives");
            foreach (var p in pets)
            {
                var entry = string.Join(" ", p.Firstname, p.Lastname) + p.JoinedPractice + "," + p.NumberofVisits;
                if (p is Cat)
                {
                    var cat = p as Cat;
                    entry += "," + cat.NumberOfLives;
                }

                entries.Add(entry);
            }
            File.WriteAllLines(filename, entries.ToArray());
        }
    }

    public class Dog : Pet
    {
        private double costPerVisit;

        public double CostPerVisit { get => costPerVisit; set => costPerVisit = value; }
    }

    public class Cat : Pet
    {
        private int? numberOfLives;
        private double costPerVisit;

        public int? NumberOfLives { get => numberOfLives; set => numberOfLives = value; }
        public double CostPerVisit { get => costPerVisit; set => costPerVisit = value; }
    }
    
    public class Owner
    {
        private string firstname;
        private string lastname;

        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
    }

}
