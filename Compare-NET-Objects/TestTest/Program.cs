using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjectsTests.TestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTest
{
    class Program
    {

        static void Main(string[] args)
        {
            CompareObjects compareObjects = new CompareObjects();

            var jane = new Person { Name = "Jane", DateCreated = DateTime.Now };
            var mary = new Person { Name = "Mary", DateCreated = DateTime.Now };
            var jack = new Person { Name = "Jack", DateCreated = DateTime.Now };

            var nameList1 = new List<Person>() { jane, mary, jack };
            var nameList2 = new List<Person>() { jane, jack, mary };

            var class1 = new ListClass<Person>(nameList1);
            var class2 = new ListClass<Person>(nameList2);

            compareObjects.IgnoreCollectionOrder = true;
            compareObjects.Compare(class1, class2);
            Console.WriteLine(compareObjects.Differences.Count);
            Console.WriteLine(compareObjects.DifferencesString);
            Console.ReadLine();
        }

    }
}
