namespace CSVExporter;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        List<Person> people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30, Email = "alice@gmail.com" },
            new Person { Name = "Bob", Age = 25, Email = "bob@hotmail.com" }
        };

        PersonExporter.ExportToCsv(people, "peoples.tsv");
    }
}
