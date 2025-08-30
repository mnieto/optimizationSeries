using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVExporter
{

    [MemoryDiagnoser(false)]
    [SimpleJob]
    public class ExportComparation
    {
        private List<Person> data = new List<Person>();

        [GlobalSetup]
        public void Setup()
        {
            data = new List<Person>
            {
                new Person { Name = "Alice", Age = 30, Email = "alice@gmail.com" },
                new Person { Name = "Bob", Age = 25, Email = "bob@hotmail.com" },
                new Person { Name = "Charlie", Age = 35, Email = "charlie@yahoo.com" },
                new Person { Name = "Diana", Age = 28, Email = "dhunter@gmail.com" },
                new Person { Name = "Eve", Age = 22, Email = "evefirst@hotmail.com" },
                new Person { Name = "Frank", Age = 40, Email = "anafrank@gmail.com" }
            };
        }

        [Benchmark]
        public void ExportWithSourceGenerator()
        {
            string exported = PersonExporter.ExportToCsv(data);
        }

        [Benchmark]
        public void ExportWithReflexion()
        {
            string exported = ExporterWitReflection.ExportToCsv(data);
        }

    }
}
